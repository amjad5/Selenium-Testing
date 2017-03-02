using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Testing
{
    [TestClass]
    public class WikipediaSearchTest
    {
        const string existingPage = "Abdus Salam";
        const string nonExistingPage = "Abcxyz";
        private IWebDriver _driver = new ChromeDriver("C:/Users/amjad/Documents/EGDownloads");
        [TestInitialize]
        public void TestInit()
        {
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            _driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main");
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _driver.Close();
        }

        [TestMethod]
        public void Search_FindsResult_ResultPageIsShown()
        {
            searchFor(existingPage);            
            Assert.AreEqual(existingPage, getFirstHeading());
        }

        [TestMethod]
        public void Search_FindsNoResult_ShowNoResultsMessage()
        {
            searchFor(nonExistingPage);
            var expected = string.Concat("The page \"" , nonExistingPage, "\" does not exist.");
            Assert.IsTrue(createLink().Contains(expected));
        }

        private string createLink()
        {
            return _driver.FindElement(By.ClassName("mw-search-createlink")).Text;
        }

        private string getFirstHeading()
        {
            return _driver.FindElement(By.Id("firstHeading")).Text;
        }

        private void searchFor(string searchTerm)
        {
            IWebElement searchInput = _driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys(searchTerm);
            searchInput.SendKeys(Keys.Enter);
        }
    }
}
