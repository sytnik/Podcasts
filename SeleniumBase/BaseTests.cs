using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumBase;

public class BaseTests
{
    IWebDriver driver;
    public BaseTests() => StartBrowser();

    public void StartBrowser()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        driver.Manage().Window.Maximize();
        driver.Url = "https://getbootstrap.com/";
    }

    [Fact]
    public void TestDownload()
    {
        driver.FindElement(By.PartialLinkText("docs")).Click();
        driver.FindElement(By.XPath("//*[@id=\"bd-docs-nav\"]/ul/li[1]/ul/li[2]/a")).Click();
        driver.FindElement(By.CssSelector("body > div.container-xxl.bd-gutter.mt-3.my-md-4.bd-layout > main > div.bd-content.ps-lg-2 > p:nth-child(5) > a"))
            .Click();
    }

    [Fact]
    public void TestModal()
    {
        driver.FindElement(By.PartialLinkText("docs")).Click();
        driver.FindElement(By.PartialLinkText("Modal")).Click();
        driver.FindElement(By.XPath("/html/body/div[2]/main/div[3]/div[1]/div[1]/button")).Click();
    }
}