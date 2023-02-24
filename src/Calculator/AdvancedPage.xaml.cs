using System.Data;

namespace Calculator;

public partial class AdvancedPage : ContentPage
{
	public AdvancedPage()
	{
		InitializeComponent();
        OnClear(this, null);
    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";



    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;

        this.CurrentCalculation.Text += pressed;

    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);
        OnCalculate(this, null);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        if (pressed == "÷")
        {
            decimalFormat = "N2";
        }
        mathOperator = pressed;

        this.CurrentCalculation.Text += pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
        CurrentCalculation.Text = "";
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            if (secondNumber == 0)
                LockNumberValue(resultText.Text);

            double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            //this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = 1;
            currentEntry = string.Empty;
        }
    }

    void OnCalculateResult(object sender, EventArgs e)
    {
        string express = CurrentCalculation.Text;
        if (express.Contains("("))
        {
            try
            {
                express = express.Replace("×", "*").Replace("÷", "/").Replace("Mod", "%");
                string res = new DataTable().Compute(express, null).ToString();
                resultText.Text = res;
                CurrentCalculation.Text = "";
            }
            catch (Exception ex)
            {
                resultText.Text = "Invalid";
                CurrentCalculation.Text = "";
            }
            
        }
        else
        {
            OnCalculate(sender, e);
            CurrentCalculation.Text = "";
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        //if (currentState == 1)
        //{
        //    secondNumber = -1;
        //    mathOperator = "×";
        //    currentState = 2;
        //    OnCalculate(this, null);
        //}
        double number;
        double.TryParse(resultText.Text, out number);
        resultText.Text = (number * (-1)).ToTrimmedString(decimalFormat);
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnSelectRootOperator(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        double number;
        decimalFormat = "N2";
        double.TryParse(resultText.Text, out number);
        if (number >= 0)
        {
            try
            {
                CurrentCalculation.Text = CurrentCalculation.Text.Remove(CurrentCalculation.Text.Length - resultText.Text.Length) + (pressed + resultText.Text);
                resultText.Text = Math.Sqrt(number).ToTrimmedString(decimalFormat);
            }
            catch (Exception ex)
            {
                resultText.Text = Math.Sqrt(number).ToTrimmedString(decimalFormat);
            }

        }
        else
        {
            resultText.Text = "Invalid";
        }
    }
}
