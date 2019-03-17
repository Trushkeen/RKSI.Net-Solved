using System;
using System.IO;
using System.Text;

namespace Lab6
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

        public void AddOperation(CalculatorOperation op)
        {
            Operation = op;
            OnDidChangeOperation(this,
                new CalculatorEventArgs("Изменился оператор", null, null, Operation));
            LeftValue = RightValue;
            OnDidChangeRight(this,
                new CalculatorEventArgs("Левое значение приняло правое", null, RightValue, null));
            RightValue = null;
            OnDidChangeLeft(this,
                new CalculatorEventArgs("Правое значение обнулено", null, null, null));
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
