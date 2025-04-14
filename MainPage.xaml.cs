using System;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private double currentNumber = 0;
        private double previousNumber = 0;
        private string currentOperator = "";

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNumberButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Text;

		  if (digit == "." && ((Label)Content.FindByName("resultLabel")).Text.Contains("."))
		  {
		   	return; // Ignore additional decimal points
		  }

            if (currentNumber == 0 && digit != "0" && digit != ".")
            {
                // Handle leading zeros
                ((Label)Content.FindByName("resultLabel")).Text = digit;
                currentNumber = double.Parse(digit);
            }
            else
            {
                ((Label)Content.FindByName("resultLabel")).Text += digit;
                if (!double.TryParse(((Label)Content.FindByName("resultLabel")).Text, out currentNumber))
                {
                    currentNumber = 0; // Handle potential parsing errors
                }
            }
        }

        private void OnOperatorButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operatorClicked = button.Text;

            if (currentNumber != 0)
            {
                if (previousNumber == 0)
                {
                    previousNumber = currentNumber;
                }
                else
                {
                    Calculate();
                }
                currentOperator = operatorClicked;
                ((Label)Content.FindByName("resultLabel")).Text = operatorClicked;
                currentNumber = 0;
            }
        }

        private void OnEqualsButtonClicked(object sender, EventArgs e)
        {
            Calculate();
            currentOperator = "";
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            currentNumber = 0;
            previousNumber = 0;
            currentOperator = "";
            ((Label)Content.FindByName("resultLabel")).Text = "0";
        }

        private void Calculate()
        {
            if (currentOperator == "+")
                currentNumber = previousNumber + currentNumber;
            else if (currentOperator == "-")
                currentNumber = previousNumber - currentNumber;
            else if (currentOperator == "*")
                currentNumber = previousNumber * currentNumber;
            else if (currentOperator == "/")
            {
                if (currentNumber != 0)
                    currentNumber = previousNumber / currentNumber;
                else
                    ((Label)Content.FindByName("resultLabel")).Text = "Error";
				    	currentNumber = 0;
					previousNumber = 0;
				
				 // Handle division by zero
            }

            ((Label)Content.FindByName("resultLabel")).Text = currentNumber.ToString();
            previousNumber = currentNumber;
        }
    }
}