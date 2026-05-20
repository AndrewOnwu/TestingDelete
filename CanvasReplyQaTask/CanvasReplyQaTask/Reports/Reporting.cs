using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CanvasReplyQaTask.Hooks;
using CanvasReplyQaTask.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CanvasReplyQaTask.Reports
{
    public class Reporting
    {
        public static ExtentReports Extent { get; set; }
        public ExtentTest Test { get; set; }
        private static Uri? _baseReportsDirectory;

        static Reporting()
        {
            var actualPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.Parent!.Parent!.Parent!.FullName;
            _baseReportsDirectory = new Uri(actualPath + "\\Reports\\Index.html");
            var reportPath = _baseReportsDirectory.LocalPath;
            var reporter = new ExtentSparkReporter(reportPath);
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            Extent = new ExtentReports();
            Extent.AttachReporter(reporter);
            Extent.AddSystemInfo("Task", "CanvasReply");
            Extent.AddSystemInfo("Browser", Hook.Config.Browser);
        }


        public void AfterStepTask(ScenarioContext scenarioContext)
        {
            
             var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stepDefType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = scenarioContext.StepContext.StepInfo.Text;
            var baseString = ((ITakesScreenshot)Webdriver.Driver).GetScreenshot().AsBase64EncodedString;
            var media = MediaEntityBuilder.CreateScreenCaptureFromBase64String(baseString).Build();
            var step = $"{stepDefType} : {stepText}";
            switch (scenarioContext.StepContext.Status)
            {
                case ScenarioExecutionStatus.Skipped:
                    Test.Log(Status.Skip, step, media);
                    Test.Skip("Test was marked as Skipped");
                    break;
                case ScenarioExecutionStatus.OK:
                    Test.Log(Status.Pass, step, media);
                    break;
                default:
                    Test.Log(Status.Fail, step, media);
                    Test.Fail($"Assertion Result: {TestContext.CurrentContext.Result.Message}");
                    break;
            }
        }


        public static void EndReport()
        {
            Extent.Flush();
        }

    }
}