using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7
{
    public class CalculatorForm : Form
    {
        private Button[,] buttons = null;
        private TextBox output = null;
        private ICalculator calc;

        private string[,] symbols = {
            { "7", "8", "9", "/" },
            { "4", "5", "6", "*" },
            { "1", "2", "3", "-" },
            { "0", ",", "=", "+"}
        };

        public CalculatorForm(ICalculator calcl)
        {
            calc = new Calculator();
            Logger lgr = new Logger("log.txt");
            calc.OnDidChangeRight += lgr.Calculator_OnDidChangeRight;
            calc.OnDidChangeLeft += lgr.Calculator_OnDidChangeLeft;
            calc.OnDidChangeOperation += lgr.Calculator_OnDidChangeOperation;
            calc.OnDidCompute += lgr.Calculator_OnDidCompute;
            calc.OnUnableToCompute += calc.Calculator_OnUnableToCompute;
            calc.OnPointAdded += lgr.Calculator_OnPointAdded;
            //
            int offset = 8;
            Point origin = new Point(offset, offset);
            ClientSize = new Size(480, 640);
            Text = "Calculator";
            AutoScaleMode = AutoScaleMode.Font;

            Size buttonSize = new Size(
                (ClientSize.Width - origin.X) / symbols.GetLength(1) - offset,
                (ClientSize.Height - origin.Y) / (symbols.GetLength(0) + 1) - offset
            );

            Font font = null;
            using (Graphics g = this.CreateGraphics())
            {
                font = new Font(FontFamily.GenericMonospace, buttonSize.Height / 2 / g.DpiY * 72);
            }

            int tabIndex = 0;
            SuspendLayout();

            //
            output = new TextBox();
            output.TabIndex = tabIndex++;
            output.Name = "output";
            output.TextAlign = HorizontalAlignment.Right;
            output.Font = font;
            output.ReadOnly = true;

            output.Location = origin;
            output.AutoSize = false;
            output.Size = new Size(ClientSize.Width - origin.X * 2, buttonSize.Height);

            origin = new Point(origin.X, origin.Y + output.Size.Height + offset);

            //
            buttons = new Button[symbols.GetLength(0), symbols.GetLength(1)];
            for (int i = 0; i < symbols.GetLength(0); ++i)
            {
                for (int j = 0; j < symbols.GetLength(1); ++j)
                {
                    Button b = new Button();
                    b.Name = "Button_" + symbols[i, j];
                    b.Text = symbols[i, j];
                    b.Font = font;
                    b.TabIndex = tabIndex++;

                    b.Location = new Point(
                        origin.X + (buttonSize.Width + offset) * j,
                        origin.Y + (buttonSize.Height + offset) * i);
                    b.Size = buttonSize;

                    b.Click += buttonOnClick;

                    buttons[i, j] = b;
                }
            }

            // 
            foreach (Button b in buttons)
                Controls.Add(b);
            Controls.Add(output);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            ResumeLayout(false);
            PerformLayout();
        }

        private void buttonOnClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            output.AppendText(btn.Text);
            if (btn.Text == "=")
            {
                try
                {
                    Parse(calc, output.Text);
                    output.Text = calc.Result.Value.ToString();
                    calc.Clear();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Не посчиталось", "Ну вот опять!");
                }
            }
        }

        static void Parse(ICalculator calc, string expression)
        {
            int numsAfterPoint = 0;
            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                {
                    if (calc.RightValue == null) numsAfterPoint = 0;
                    if (numsAfterPoint > 0)
                    {
                        calc.AddPoint(c - '0', numsAfterPoint);
                        numsAfterPoint++;
                    }
                    else
                        calc?.AddDigit(c - '0');
                }
                else if (c == ',')
                {
                    numsAfterPoint++;
                }
                else if (c == '=')
                {
                    calc.Compute();
                }
                else
                {
                    object op = Enum.ToObject(typeof(CalculatorOperation), c);

                    if (Enum.IsDefined(typeof(CalculatorOperation), op))
                    {
                        calc?.AddOperation((CalculatorOperation)op);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid character: " + c);
                    }
                }
            }
        }
    }
}
