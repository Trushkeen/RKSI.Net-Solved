using System;

namespace Lab7
{
    public enum CalculatorOperation { Add = '+', Sub = '-', Mul = '*', Div = '/' };

    public interface ICalculator
    {
        /// <summary>
        /// Значение слева от операции
        /// </summary>
        double? LeftValue { get; }

        /// <summary>
        /// Значение справа от операции
        /// </summary>
        double? RightValue { get; }

        /// <summary>
        /// Текущая операция
        /// </summary>
        CalculatorOperation? Operation { get; }

        /// <summary>
        /// Последний вычисленный результат
        /// </summary>
        double? Result { get; }

        /// <summary>
        /// Добавляет точку
        /// </summary>
        void AddPoint(double digit, int nextAfterPoint);

        /// <summary>
        /// Добавить цифру.
        /// Добавляет цифру справа к правому значению
        /// </summary>
        /// <param name="digit">Цифра 0-9</param>
        void AddDigit(int digit);

        event CalculatorEvent OnDidChangeLeft;
        event CalculatorEvent OnDidChangeRight;
        event CalculatorEvent OnPointAdded;

        /// <summary>
        /// Добавить следующую арифметическую операцию.
        /// Изменяет значение Operation. 
        /// Если операция уже задана, то вычисляет результат
        /// и переносит его в левое значение.
        /// Если операция не задана, то переносит правое значение влево.
        /// </summary>
        /// <param name="op">Новая опреация</param>
        void AddOperation(CalculatorOperation op);

        event CalculatorEvent OnDidChangeOperation;

        /// <summary>
        /// Производит вычисление текущего выражения.
        /// Если это невозможно, вызывает событие UnableToCompute.
        /// Результат записывается в левое значение.
        /// </summary>
        void Compute();

        event CalculatorEvent OnDidCompute;
        event CalculatorEvent OnUnableToCompute;

        /// <summary>
        /// Очищает внутренниее сосотояние калькулятора.
        /// </summary>
        void Clear();

        void Calculator_OnUnableToCompute(ICalculator sender, CalculatorEventArgs eventArgs);
    }
}
