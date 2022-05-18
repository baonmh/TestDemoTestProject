using NUnit.Framework;
using System;
using FlaUI.Core;
using FlaUI.UIA2;
using FlaUI.UIA3;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Capturing;
using WPFTestDemo.Config;

namespace WPFTestDemo
{
    public abstract class TestBase : ReportsGenerationClass
    {
//        [OneTimeSetUp]
//        public static void AssemblyInit(TestContext context)
//        {
//            //Autofill Initialization
////            AutoFillManager.Add(typeof(TextBox), (a, s) => a.AsTextBox().Enter(s.ToString()));
////            AutoFillManager.Add(typeof(ComboBox), (a, s) => a.AsComboBox().SelectTrim(s.ToString()).Click());
////            AutoFillManager.Add(typeof(CheckBox)
////, (a, s) => a.AsCheckBox().IsToggled = Convert.ToBoolean(s));
////            AutoFillManager.Add(typeof(RadioButton)
////, (a, s) => a.AsRadioButton().IsChecked = Convert.ToBoolean(s));
//            //SQLHelper.SetExportDirectory();
//        }

        public TestContext TestContext { get; set; }

        protected AutomationType AutomationType { get; }

        protected Window window { get; private set; }

        /// <summary>
        /// Path of the directory for the screenshots
        /// </summary>
        protected string ScreenshotDir { get; }
        protected AutomationBase automation { get; }
        protected Application App { get; private set; }

        #region Win32 API
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int show);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_MAXIMIZE = 3;
        #endregion
        protected TestBase()
            : this(AutomationType.UIA3)
        {

        }
        protected TestBase(AutomationType automationType) //, TestApplicationType appType)
        {
            AutomationType = automationType;

            //ScreenshotDir = ConfigurationManager.AppSettings["FailedTestScreenshotDirectory"];

            automation = GetAutomation(automationType);
        }

        /// <summary>
        /// Setup which starts the application (if not started)
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            //App = StartApplication();
            //// try to bring the main window to foregroud
            //// no known reliable way for now.
            //// This is needed when we reuse the same application, some keystrokes
            //// are sent directly to foreground window 
            //// (If we get rid of those keystrokes, we can get rid of this)
            //ShowWindow(App.MainWindowHandle, SW_MAXIMIZE);
            ////ShowWindow(App.MainWindowHandle, SW_SHOWNORMAL);
            //SetForegroundWindow(App.MainWindowHandle);
            //App.WaitWhileMainHandleIsMissing(TimeSpan.FromMinutes(3)); // 3 minutes timeout opening app
            //TestUtilities.WaitToGetEnabled(window.FindFirstDescendant(cf => cf.ByName("Ready")));       

            App = Application.Launch(GlobalVariable.APP_PATH);
            window = App.GetMainWindow(automation);


        }

        /// <summary>
        /// Closes the application after all tests were run
        /// </summary>
        protected void CloseApplication()
        {
            automation.Dispose();
            App.Kill();
            App.Close();
            App.Dispose();
            //Thread.Sleep(3000);
            App = null;
        }

        [TearDown]  
/*        public void TestCleanup()
        {
            // Capture screen if test fails
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                //string rowIndex = string.Empty;
                //if (TestContext.DataRow != null)
                //    rowIndex = "." + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow).ToString();
                ////TakeScreenshot( TestContext.FullyQualifiedTestClassName + rowIndex );
                TakeScreenshot(NUnit.Framework.TestContext.CurrentContext.Test.Name);
                Thread.Sleep(3000);
                CloseApplication();
            }*/

            //            // If test didn't pass, then close the  app such that new one will open for next test
            //            //if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            //            //{
            //            //    CloseApplication();
            //            //}
            //            //else
            //            //{
            //            //    string rowIndex = string.Empty;
            //            //    if (TestContext.DataRow != null)
            //            //        rowIndex = "." + TestContext.DataRow.Table.Rows.IndexOf(TestContext.DataRow).ToString();
            //            //    TakeScreenshot(TestContext.TestName + rowIndex);
            //            //    automation.Dispose();
            //            //    // Try to free Application object but keep the EXE running
            //            //    // No "detach" method... assume calling Dispose() will do the work
            //            //    App.Dispose();
            //            //    Thread.Sleep(3000);
            //            //    CloseApplication();
            //            }

            ////            Console.WriteLine(
            ////                "TextContext.TestName='{0}' {1} ",
            ////                TestContext.TestName,
            ////(Microsoft.VisualStudio.TestTools.UnitTesting.UnitTestOutcome.Passed == TestContext.CurrentTestOutcome ? "Pass" : "Fail"));
            //        //}


            /// <summary>
            /// Method which starts the custom application to test
            /// </summary>
            //protected Application StartApplication()
            //{
            //    ProcessStartInfo processStartInfo = new ProcessStartInfo();
            //    processStartInfo.FileName = ConfigurationManager.AppSettings["EvolutionAppLocation"];
            //    processStartInfo.WorkingDirectory = ConfigurationManager.AppSettings["StartInDirectory"];
            //    bool flag = false;
            //    Process[] processlist = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ConfigurationManager.AppSettings["EvolutionAppLocation"]));
            //    //foreach (Process p in processlist)
            //    //{
            //    //    if (p.WindowsIdentity().Name.ToLower() == (Environment.UserDomainName + @"\" + Environment.UserName).ToLower())
            //    //    {
            //    //        evolutionApplication = Application.Attach(p.Id);
            //    //        flag = true;
            //    //    }
            //    //}
            //    if (flag == false)
            //    {

            //        //temp
            //        string strCmdText;
            //        strCmdText = "/C D: && cd D:\\Evolution_SYSTEMTEST_Maint\\ && Toogood.Evolution.Forms.Ui.exe";
            //        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            //        Thread.Sleep(TimeSpan.FromSeconds(25));

            //        processlist = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ConfigurationManager.AppSettings["EvolutionAppLocation"]));

            //        //foreach (Process p in processlist)
            //        //{
            //        //    if (p.WindowsIdentity().Name.ToLower() == (Environment.UserDomainName + @"\" + Environment.UserName).ToLower())
            //        //    {
            //        //        evolutionApplication = Application.Attach(p.Id);
            //        //        flag = true;
            //        //    }
            //        //}
            //    }

            //    window = evolutionApplication.GetMainWindow(automation, new TimeSpan(0, 3, 0));
            //    Assert.IsNotNull(window, "Cannot start application UI");
            //    return evolutionApplication;
            //}

            /// <summary>
            /// Restarts the application to test
            /// </summary>
            //protected void RestartApp()
            //{
            //    CloseApplication();

            //    TestInitialize();

     //   }

        private AutomationBase GetAutomation(AutomationType automationType)
        {
            switch (automationType)
            {
                case AutomationType.UIA2:
                    return new UIA2Automation();
                case AutomationType.UIA3:
                    return new UIA3Automation();
                default:
                    throw new ArgumentOutOfRangeException(nameof(automationType), automationType, null);
            }
        }


        public static void TakeScreenshot(string screenshotName)
        {
            string dir = Environment.CurrentDirectory;
            var imagename = screenshotName + ".png";
            imagename = imagename.Replace("\"", String.Empty);
            var imagePath = Path.Combine(dir, @"Screenshots\", imagename);
            try
            {
                Directory.CreateDirectory(dir);
                Capture.Screen().ToFile(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save screenshot to directory: {0}, filename: {1}, Ex: {2}", "", imagePath, ex);
            }
        }

        protected void SwitchToTopWindow()
        {
            Window topWindow = App.GetAllTopLevelWindows(automation)[0];
            if (topWindow == null)
                return;
            else
            {
                this.window = topWindow;
            }
        }

        public void CloseWindow()
        {
            string strCmdText;
            strCmdText = "/C taskkill /F /IM EditingCollections.exe";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}