using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Calculator : ICalculator
    {
        public double? LeftValue { get; private set; }
        public double? RightValue { get; private set; }
        public CalculatorOperation? Operation { get; private set; }
        public double? Result { get; private set; }

        public event CalculatorEvent OnDidChangeLeft;
        public event CalculatorEvent OnDidChangeRight;
        public event CalculatorEvent OnDidChangeOperation;
        public event CalculatorEvent OnDidCompute;
        public event CalculatorEvent OnUnableToCompute;
        public event CalculatorEvent OnClear;

        public Calculator()
        {
            OnDidChangeRight += Calculator_OnDidChangeRight;
            OnDidChangeLeft += Calculator_OnDidChangeLeft;
            OnDidChangeOperation += Calculator_OnDidChangeOperation;
            OnDidCompute += Calculator_OnDidCompute;
            OnUnableToCompute += Calculator_OnUnableToCompute;
            OnClear += Calculator_OnClear;
        }

        private void Calculator_OnClear(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            Logger.WriteLog("log.txt", eventArgs.Message);
        }

        public void AddDigit(int digit)
        {
            if (RightValue == null)
            {
                RightValue = digit;
                OnDidChangeRight(this, new CalculatorEventArgs("Добавлена первая цифра в правое значение", null, RightValue, null));
            }
            else
            {
                RightValue = RightValue * 10 + digit;
                OnDidChangeRight(this, new CalculatorEventArgs("Добавлена следующая цифра в правое значение", null, RightValue, null));
            }
        }
        /// Добавить следующую арифметическую операцию.
        /// Изменяет значение Operation. 
        /// Если операция уже задана, то вычисляет результат
        /// и переносит его в левое значение.
        /// Если операция не задана, то переносит правое значение влево.
        public void AddOperation(CalculatorOperation op)
        {
            if (Operation == null)
            {
                LeftValue = RightValue;
                OnDidChangeRight(this,
                    new CalculatorEventArgs("Левое значение приняло правое", null, RightValue, null));
                RightValue = null;
                OnDidChangeLeft(this,
                    new CalculatorEventArgs("Правое значение обнулено", null, null, null));
                Operation = op;
                OnDidChangeOperation(this,
                    new CalculatorEventArgs("Изменился оператор", null, null, Operation));
            }
            else if (Operation != null)
            {
                Compute();
                OnDidCompute(this,
                    new ComputeEventArgs(LeftValue.Value, RightValue.Value, Operation.Value, Result.Value));
                LeftValue = Result;
                OnDidChangeRight(this,
                    new CalculatorEventArgs("Левое значение приняло результат", null, RightValue, null));
                RightValue = null;
                OnDidChangeLeft(this,
                    new CalculatorEventArgs("Правое значение обнулено", null, null, null));
                Operation = op;
                OnDidChangeOperation(this,
                    new CalculatorEventArgs("Изменился оператор", null, null, Operation));
            }
        }

        private void Calculator_OnDidChangeOperation(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            Logger.WriteLog("log.txt", eventArgs.Message + " " + eventArgs.Operation.Value);
        }

        private void Calculator_OnDidChangeRight(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            Logger.WriteLog("log.txt", eventArgs.Message + " " + eventArgs.RightValue);
        }

        private void Calculator_OnDidChangeLeft(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            Logger.WriteLog("log.txt", eventArgs.Message + " " + eventArgs.LeftValue);
        }

        private void Calculator_OnDidCompute(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            var e = eventArgs as ComputeEventArgs;
            Logger.WriteLog("log.txt", $"Посчитано {e.LeftValue} {(char)(CalculatorOperation)e.Operation} {e.RightValue}={e.Result}");
        }

        public void Clear()
        {
            LeftValue = null;
            RightValue = null;
            Result = null;
            Operation = null;
            OnClear(this, new CalculatorEventArgs("Очистка переменных", null, null, null));
        }

        public void Compute()
        {
            if (LeftValue != null && RightValue != null)
            {
                switch (Operation)
                {
                    case CalculatorOperation.Add:
                        Result = LeftValue + RightValue;
                        OnDidCompute(this, new ComputeEventArgs(LeftValue.Value, RightValue.Value, Operation.Value, Result.Value));
                        break;
                    case CalculatorOperation.Sub:
                        Result = LeftValue - RightValue;
                        OnDidCompute(this, new ComputeEventArgs(LeftValue.Value, RightValue.Value, Operation.Value, Result.Value));
                        break;
                    case CalculatorOperation.Mul:
                        Result = LeftValue * RightValue;
                        OnDidCompute(this, new ComputeEventArgs(LeftValue.Value, RightValue.Value, Operation.Value, Result.Value));
                        break;
                    case CalculatorOperation.Div:
                        if (RightValue != 0)
                        {
                            Result = LeftValue / RightValue;
                            OnDidCompute(this, new ComputeEventArgs(LeftValue.Value, RightValue.Value, Operation.Value, Result.Value));
                        }
                        else
                        {
                            OnUnableToCompute(this,
                                new ErrorEventArgs("Ошибка деления!", LeftValue, RightValue, Operation));
                        }
                        break;
                    default:
                        Clear();
                        break;
                }
            }
        }

        private void Calculator_OnUnableToCompute(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            Logger.WriteLog("log.txt", eventArgs.Message);
        }
    }
}
