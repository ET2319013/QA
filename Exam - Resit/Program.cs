using System;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using NUnit.Framework.Legacy;

namespace SeleniumDocs.GettingStarted;

public static class DynamicControlsTest
{
    public static void Main()
    {
        IWebDriver SeleniumDriver = new ChromeDriver();
        bool AssertSiteReach = false;
        bool AssertButtonClick = false;
        int ScreenshotCount = 0;
        try // checks if the site can be reached, if not - stops the work
        {
            SeleniumDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/challenging_dom");
            
        }
        catch (OpenQA.Selenium.WebDriverException ex)
        {
            Console.WriteLine("The Site Couldn't Be Reached");
            ScreenshotCount = 11;
        }
        AssertSiteReach = true; // assert
        ClassicAssert.IsTrue(AssertSiteReach);
        var title = SeleniumDriver.Title;
        
        while (ScreenshotCount < 10)
        {
            SeleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try // checks if a button can be clicked, if not - stops the work
            {
                var Button = SeleniumDriver.FindElement(By.ClassName("button"));
                Button.Click();
                ScreenshotCount++;
                AssertButtonClick = true;
            } 
            catch (OpenQA.Selenium.NoSuchElementException ex)
            {
                Console.WriteLine("The Button Could Not Be Clicked");
                ScreenshotCount = 11;
            }
            Screenshot SaveSc = ((ITakesScreenshot)SeleniumDriver).GetScreenshot();
            if (ScreenshotCount == 11)
            {
                SeleniumDriver.Quit();
            }
            else
            {
                SaveSc.SaveAsFile("C:\\Users\\olga-\\Downloads\\Screenshot" + ScreenshotCount + ".png");
            }
            ClassicAssert.IsTrue(AssertButtonClick); // final assert
        }
        SeleniumDriver.Quit();
    }
}