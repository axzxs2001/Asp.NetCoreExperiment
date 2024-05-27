using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


namespace UITest02
{
    internal class Program
    {
        static void Main()
        {

            var application = Application.Launch("calc.exe");
            Window window = application.GetWindow("Calculator", InitializeOption.NoCache);

            Button buttonOne = window.Get<Button>(SearchCriteria.ByAutomationId("num1Button"));
            Button buttonPlus = window.Get<Button>(SearchCriteria.ByAutomationId("plusButton"));
            Button buttonTwo = window.Get<Button>(SearchCriteria.ByAutomationId("num2Button"));
            Button buttonEquals = window.Get<Button>(SearchCriteria.ByAutomationId("equalButton"));

            buttonOne.Click();
            buttonPlus.Click();
            buttonTwo.Click();
            buttonEquals.Click();

            Console.WriteLine("Performed 1 + 2 = operation.");
            application.Close();
        }

      
    }
}
