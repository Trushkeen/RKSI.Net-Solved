using System;
using System.Text;

namespace Lab7
{
    public delegate void CalculatorEvent(ICalculator sender, CalculatorEventArgs eventArgs);

    public class CalculatorEventArgs
    {
        public readonly double? LeftValue;
        public readonly double? RightValue;
        public readonly CalculatorOperation? Operation;

        public readonly string Message;

        public CalculatorEventArgs(string message, double? leftVal, double? rightVal, CalculatorOperation? op)
        {
            Message = message;
            LeftValue = leftVal;
            RightValue = rightVal;
            Operation = op;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (Message != null)
            {
                str.Append(Message).Append(": ");
            }
            str.Append(String.Join(" ", new object[] { LeftValue, (char)Operation, RightValue}));
            return str.ToString();
        }
    }

    public class ComputeEventArgs : CalculatorEventArgs
    {
        public readonly double Result;

        public ComputeEventArgs(double leftVal, double rightVal, CalculatorOperation op, double result) :
            base(null, leftVal, rightVal, op)
        {
            Result = result;
        }

        public override string ToString()
        {
            return base.ToString() + " = " + Result;
        }
    }

    public class ErrorEventArgs : CalculatorEventArgs
    {
        public ErrorEventArgs(string message, double? leftVal, double? rightVal, CalculatorOperation? op):
            base(message,leftVal,rightVal,op)
        {}

        public override string ToString()
        {
            return "Error: " + Message;
        }
    }
}
