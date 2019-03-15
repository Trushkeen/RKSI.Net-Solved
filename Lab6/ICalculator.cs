using System;

namespace Lab6
{
    public enum CalculatorOperation { Add = '+', Sub = '-', Mul = '*', Div = '/' };

    public interface ICalculator
    {
        double? LeftValue { get; }

        double? RightValue { get; }

        CalculatorOperation? Operation { get; }

        double? Result { get; }

        /// <summary>
        /// Adds the digit.
        /// </summary>
        /// <param name="digit">Digit.</param>
        void AddDigit(int digit);

        event CalculatorEvent OnDidChangeLeft;
        event CalculatorEvent OnDidChangeRight;
        event CalculatorEvent OnClear;

        /// <summary>
        /// Adds the operation.
        /// </summary>
        /// <param name="op">Op.</param>
        void AddOperation(CalculatorOperation op);

        event CalculatorEvent OnDidChangeOperation;

        /// <summary>
        /// Compute this instance.
        /// </summary>
        void Compute();

        event CalculatorEvent OnDidCompute;
        event CalculatorEvent OnUnableToCompute;

        /// <summary>
        /// Clear this instance.
        /// </summary>
        void Clear();
    }
}
