using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineTasks
{
    public class Helper
    {
        public static string GenerateFirstName() {
            return Faker.Name.First();
        }

        public static string GenerateLastName()
        {
            return Faker.Name.Last();
        }
    }


    public static class WebDriverExtensions
    {
        public static void ClickOnHiden(this IWebDriver driver, By by)
        {
            Actions builder = new Actions(driver);
            builder.Click(driver.FindElement(by));
            IAction selectMultiple = builder.Build();
            selectMultiple.Perform();
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, bool untilLocated)
        {
            IWebElement webElement = null;
            if (untilLocated)
            {
                while (webElement == null || !webElement.Displayed)
                {
                    webElement = driver.FindElement(by);
                    Thread.Sleep(1000);
                }
            }
            return webElement;
        }


        
    }
}
