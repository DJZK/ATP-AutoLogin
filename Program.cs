using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumExample
{
    public class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void Login(string username, string password)
        {
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the username field and enter the username
            var usernameField = driver.FindElement(By.Name("username"));
            usernameField.SendKeys(username);

            // Find the password field and enter the password
            var passwordField = driver.FindElement(By.Name("password"));
            passwordField.SendKeys(password);

            // Find the submit button and click it
            var submitButton = driver.FindElement(By.XPath("//*[@id=\"localbuttons\"]/button[1]"));
            submitButton.Click();
        }
        public static void Main(string [] args)
        {

            if(args.Length != 1) {
                Console.WriteLine("Usage: .\\AutoLogin <storecode");
                return;
            }

            // Create a new instance of the Chrome driver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Navigate to the login page
                driver.Navigate().GoToUrl("http://openflow.alfamart.ho/#/Login");

                // Create a new instance of the LoginPage class
                var loginPage = new LoginPage(driver);

                // Log in using the provided username and password
                loginPage.Login(args[0], args[0] + args[0] + "@");

                Thread.Sleep(10000);
            }
        }
    }
}
