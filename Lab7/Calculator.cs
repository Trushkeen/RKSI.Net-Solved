using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
        public event CalculatorEvent OnPointAdded;

        public void AddPoint(double digit, int nextAfterPoint)
        {
            for (int i = 0; i < nextAfterPoint; ++i)
            {
                digit = digit * 0.1;
            }
            RightValue += digit;
            OnPointAdded(this, new CalculatorEventArgs("Добавлена точка", null, RightValue, null));
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
            if (RightValue == null)
            {
                RightValue = 0;
                OnDidChangeRight(this,
                    new CalculatorEventArgs("В правое помещен 0", null, null, null));
            }
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
                    new CalculatorEventArgs("Левое значение приняло результат", Result, null, null));
                RightValue = null;
                OnDidChangeLeft(this,
                    new CalculatorEventArgs("Правое значение обнулено", null, null, null));
                Operation = op;
                OnDidChangeOperation(this,
                    new CalculatorEventArgs("Изменился оператор", null, null, Operation));
            }
        }


        public void Clear()
        {
            LeftValue = null;
            RightValue = null;
            Result = null;
            Operation = null;
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

        public void Calculator_OnUnableToCompute(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            MessageBox.Show("Не получилось посчитать", "Ну вот!");
        }
    }
}
