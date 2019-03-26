using System.IO;
using System.Text;

namespace Lab6
{
    class Logger
    {
        string Path { get; }
        public Logger(string path)
        {
            Path = path;
        }
        public void WriteLog(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Default))
            {
                sw.WriteLine(" " + text);
            }
        }

        public void Calculator_OnDidChangeOperation(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            WriteLog(eventArgs.Message + " " + eventArgs.Operation.Value);
        }

        public void Calculator_OnDidChangeRight(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            WriteLog(eventArgs.Message + " " + eventArgs.RightValue);
        }

        public void Calculator_OnDidChangeLeft(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            WriteLog(eventArgs.Message + " " + eventArgs.LeftValue);
        }

        public void Calculator_OnDidCompute(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            var e = eventArgs as ComputeEventArgs;
            WriteLog($"Посчитано {e.LeftValue} {(char)(CalculatorOperation)e.Operation} {e.RightValue}={e.Result}");
        }

        public void Calculator_OnUnableToCompute(ICalculator sender, CalculatorEventArgs eventArgs)
        {
            WriteLog(eventArgs.Message);
        }
    }
}
