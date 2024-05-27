

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;



// 设置 Appium 选项
var appCapabilities = new AppiumOptions();
//appCapabilities.AddAdditionalAppiumOption("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
//appCapabilities.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
appCapabilities.AddAdditionalOption("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
// 初始化 WindowsDriver
var driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appCapabilities);
var driver = new WindowsDriver(appCapabilities);

driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

driver.FindElement(OpenQA.Selenium.By.Name("Clear")).Click();
driver.FindElement(OpenQA.Selenium.By.Name("One")).Click();
driver.FindElement(OpenQA.Selenium.By.Name("Plus")).Click();
driver.FindElement(OpenQA.Selenium.By.Name("Two")).Click();
driver.FindElement(OpenQA.Selenium.By.Name("Equals")).Click();


// 获取计算结果
var result = driver.FindElement(OpenQA.Selenium.By.Id("CalculatorResults"));
Console.WriteLine("Calculation result: " + result.Text);

// 关闭应用
driver.Quit();

