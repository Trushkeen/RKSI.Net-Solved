﻿using System;

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

        public void AddDigit(int digit)
        {
            if (RightValue == null) RightValue = digit;
            else RightValue = RightValue * 10 + digit;
            OnDidChangeRight(this, new CalculatorEventArgs("Изменилось правое значение", null, RightValue, null));
        }

        public void AddOperation(CalculatorOperation op)
        {
            Operation = op;
            OnDidChangeOperation(this,
                new CalculatorEventArgs("Изменился оператор", null, null, Operation));
            LeftValue = RightValue;
            OnDidChangeRight(this,
                new CalculatorEventArgs("Изменилось правое значение", null, RightValue, null));
            RightValue = null;
            OnDidChangeLeft(this,
                new CalculatorEventArgs("Изменилось левое значение", LeftValue, null, null));
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
    }
}