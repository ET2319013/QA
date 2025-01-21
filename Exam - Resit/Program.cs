using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Legacy;

namespace SeleniumDocs.GettingStarted
{
    public static class DynamicControlsTest
    {
        public static void Main()
        {
            // Initialize the Selenium WebDriver using ChromeDriver
            IWebDriver seleniumDriver = new ChromeDriver();

            // Variables to track site accessibility and button clickability
            bool isSiteAccessible = false;
            bool isButtonClickable = false;
            int screenshotCount = 0;

            try
            {
                // Navigate to the specified URL to check site accessibility
                seleniumDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/challenging_dom");
                isSiteAccessible = true;
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                // Log an error if the site cannot be reached
                Console.WriteLine("The site couldn't be reached: " + ex.Message);
                screenshotCount = 11; // Exceeds the maximum limit to exit the loop
            }

            // Assert that the site is accessible
            ClassicAssert.IsTrue(isSiteAccessible);

            // Retrieve and display the site title for verification purposes
            var siteTitle = seleniumDriver.Title;
            Console.WriteLine("Site Title: " + siteTitle);

            // Take screenshots while ensuring button clickability
            while (screenshotCount < 10)
            {
                // Set an implicit wait time for finding elements
                seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                try
                {
                    // Attempt to locate and click the button on the webpage
                    var button = seleniumDriver.FindElement(By.ClassName("button"));
                    button.Click();
                    screenshotCount++;
                    isButtonClickable = true;
                }
                catch (OpenQA.Selenium.NoSuchElementException ex)
                {
                    // Log an error if the button cannot be clicked
                    Console.WriteLine("The button could not be clicked: " + ex.Message);
                    screenshotCount = 11; // Exceeds the maximum limit to exit the loop
                }

                // Capture and save a screenshot of the current state
                Screenshot screenshot = ((ITakesScreenshot)seleniumDriver).GetScreenshot();

                if (screenshotCount == 11)
                {
                    // Exit the loop and close the driver if an issue occurs
                    seleniumDriver.Quit();
                }
                else
                {
                    string screenshotPath = $"C:\\Users\\olga-\\Downloads\\Screenshot{screenshotCount}.png";
                    screenshot.SaveAsFile(screenshotPath);
                    Console.WriteLine($"Screenshot saved at: {screenshotPath}");
                }

                // Assert that the button was successfully clicked
                ClassicAssert.IsTrue(isButtonClickable);
            }

            // Quit the Selenium WebDriver after execution
            seleniumDriver.Quit();
        }
    }
}
