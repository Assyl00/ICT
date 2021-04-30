using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week9
{
    delegate void DisplayMessage(string text);
    class Brain
    {
        DisplayMessage displayMessage;
        public Brain(DisplayMessage displayMessageDelegate)
        {
            displayMessage = displayMessageDelegate;
        }

        string[] nonZeroDigit = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "PI" };
        string[] digit = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] zero = { "0" };
        string[] operation = { "+", "-", "*", "/", "mod", "root", "pow", "tan" };
        string[] equal = { "=" };
        string[] delete = { "C" };


        enum State
        {
            Zero,
            AccumulateDigits,
            ComputePending,
            Compute,
        }

        State currentState = State.Zero;
        string previousNumber = "";
        string currentNumber = "";
        string currentOperation = "";
        public void ProcessSignal(string message)
        {
            switch (currentState)
            {
                case State.Zero:
                    ProcessZeroState(message, false);
                    break;
                case State.AccumulateDigits:
                    ProcessAccumulateDigits(message, false);
                    break;
                case State.ComputePending:
                    ProcessComputePending(message, false);
                    break;
                case State.Compute:
                    ProcessCompute(message, false);
                    break;
                default:
                    break;
            }
        }

        void ProcessZeroState(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Zero;
                currentNumber = msg;
                previousNumber = currentNumber;
                displayMessage(currentNumber);
            }
            else
            {
                if (nonZeroDigit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
            }
        }


        void ProcessAccumulateDigits(string msg, bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigits;
                if (zero.Contains(currentNumber))
                {
                    currentNumber = msg;
                }
                
                else
                {
                    currentNumber = currentNumber + msg;
                }
                
                
                displayMessage(currentNumber);
            }
            else
            {
                if (digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                }
                else if(delete.Contains(msg))
                {
                    ProcessZeroState("0", true);
                }
                
            }

        }

        void ProcessComputePending(string msg, bool income)
        {
            if (income)
            {
                currentState = State.ComputePending;
                previousNumber = currentNumber;
                currentNumber = "";
                currentOperation = msg;
            }
            else
            {
                if (digit.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                
            }
        }

        void ProcessCompute(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Compute;

                double a = double.Parse(previousNumber);
                double b = double.Parse(currentNumber);


                if (currentOperation == "+")
                {
                    
                    currentNumber = (a + b).ToString();
                        
                }
                else if (currentOperation == "-")
                {
                    currentNumber = (a - b).ToString();
                }
                else if (currentOperation == "*")
                {
                    currentNumber = (a * b).ToString();
                }
                else if (currentOperation == "/")
                {
                    currentNumber = (a / b).ToString();
                }
                else if (currentOperation == "mod")
                {
                    currentNumber = (a % b).ToString();
                }
                else if(currentOperation == "root")
                {
                    currentNumber = Math.Pow(a, 1/b).ToString();
                }
                else if(currentOperation == "pow")
                {
                    currentNumber = Math.Pow(a, b).ToString();
                }

                previousNumber = currentNumber;

                displayMessage(currentNumber);

                currentNumber = "";
                
            }
            else
            {
                if (nonZeroDigit.Contains(msg))
                {
                    //previousNumber = currentNumber;
                    currentNumber = "";
                    ProcessAccumulateDigits(msg, true);
                }
                else if (zero.Contains(msg))
                {
                    currentNumber = "";
                    ProcessZeroState(msg, true);
                }
                else if (delete.Contains(msg))
                {
                    currentNumber = "";
                    ProcessZeroState("0", true);
                }
            }
        }

    }
}
