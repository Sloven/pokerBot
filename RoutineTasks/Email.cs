using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineTasks
{
    public class Email
    {
        public static void CreateEmail() {

            IWebDriver driver = new FirefoxDriver();

            try
            {
                SelectElement selector = null;

                string DOBDay = DateTime.Now.Day.ToString();
                string DOBMonth = DateTime.Now.Month.ToString();
                string DOBYear = (DateTime.Now.Year - 20).ToString();
                string boxDomain = "@bk.ru";

                driver.Navigate().GoToUrl("http://e.mail.ru/cgi-bin/signup?from=main_noc");

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-firstname-row')]/span[2]/input")).SendKeys("firstname");

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-lastname-row')]/span[2]/input")).SendKeys("lastname");

                selector = new SelectElement(driver.FindElement(By.XPath("//form//div[contains(@class,'qc-birthday-row')]/span[2]/select[1]")));
                selector.SelectByValue(DOBDay);

                selector = new SelectElement(driver.FindElement(By.XPath("//form//div[contains(@class,'qc-birthday-row')]/span[2]/select[2]")));
                selector.SelectByValue(DOBMonth);

                selector = new SelectElement(driver.FindElement(By.XPath("//form//div[contains(@class,'qc-birthday-row')]/span[2]/select[3]")));
                selector.SelectByValue(DOBYear);

                driver.FindElement(By.Id("man2")).Click();

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-login-row')]/span[2]/input")).SendKeys("mailname122222");

                selector = new SelectElement(driver.FindElement(By.XPath("//form//div[contains(@class,'qc-login-row')]/span[2]/select")));
                selector.SelectByText(boxDomain);

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-pass-row')]/span[2]/input")).Clear();
                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-pass-row')]/span[2]/input")).SendKeys("wordpass");
                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-passverify-row')]/span[2]/input")).Clear();
                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-passverify-row')]/span[2]/input")).SendKeys("wordpass");
                driver.FindElement(By.Id("noPhoneLink")).Click();

                selector = new SelectElement(driver.FindElement(By.XPath("//form//div[contains(@class,'qc-question-row')]/span[2]/select[1]")));
                selector.SelectByText("Свой вопрос");

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-customquestion-row')]/span[2]/input")).SendKeys("question");

                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-answer-row')]/span[2]/input")).Clear();
                driver.FindElement(By.XPath("//form//div[contains(@class,'qc-answer-row')]/span[2]/input")).SendKeys("awswer");

                driver.FindElement(By.CssSelector("input.js-submit")).Click();
                driver.FindElement(By.CssSelector("div.is-signupphone2_in > form > div.popup__box > div.popup__desc > div.form__row > div.form__row__subwidget_inline > input[name=\"code\"]")).Clear();
                driver.FindElement(By.CssSelector("div.is-signupphone2_in > form > div.popup__box > div.popup__desc > div.form__row > div.form__row__subwidget_inline > input[name=\"code\"]")).SendKeys("CAPTCHA");
                driver.FindElement(By.XPath("(//input[@value='Готово'])[6]")).Click();

                var element = driver.FindElement(By.CssSelector("div.is-signupphone2_in > form > div.popup__box > div.popup__desc > div.form__row > div.form__row__subwidget_inline > img[class=\"js-captchaImage\"]"));
                String src = ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].getAttribute('src');", element).ToString();
            }
            finally
            {
                //Close the browser
                driver.Quit();
            }
        }
    }
}



