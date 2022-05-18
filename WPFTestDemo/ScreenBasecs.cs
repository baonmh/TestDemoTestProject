using FlaUI.Core.AutomationElements.Infrastructure;
using System;
using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.Conditions;
using System.Threading;
using FlaUI.Core.Input;

namespace WPFTestDemo
{
    public abstract class ScreenBase
    {
        private AutomationElement element;
        public ScreenBase(AutomationElement element)
        {
            this.element = element;
        }

        public AutomationElement FindByAutomationId(string text)
        {
            return RetryUntilNotNull(() => element.FindFirstDescendant(cf => cf.ByAutomationId(text)));
        }

        public AutomationElement FindByName(string text)
        {
            return RetryUntilNotNull(() => element.FindFirstDescendant(cf => cf.ByName(text)));
        }

        public AutomationElement FindByValue(string text)
        {
            return RetryUntilNotNull(() => element.FindFirstDescendant(cf => cf.ByValue(text)));
        }
        public AutomationElement FindByControlType(FlaUI.Core.Definitions.ControlType text)
        {
            return RetryUntilNotNull(() => element.FindFirstDescendant(cf => cf.ByControlType(text)));
        }
        public AutomationElement FindByXPath(string text)
        {
            return RetryUntilNotNull(() => element.FindFirstByXPath(text));
        }

        public T RetryUntilNotNull<T>(System.Func<T> func, int timeout = 50) where T : class
        {
            T result = null;
            DateTime start = DateTime.UtcNow;

            while (result == null)
            {
                result = func();
                if (DateTime.UtcNow.Subtract(start).TotalSeconds > timeout) break;
                if (result == null) System.Threading.Thread.Sleep(300);
            }

            return result;
        }

        public void MaximineWindow()
        {
            var maxElement = FindByXPath("//TitleBar/Button[@Name='Maximize']");
            if (maxElement.IsAvailable)
            {
                maxElement.Click();
            }
        }

        public void CloseWindow()
        {
            string strCmdText;
            strCmdText = "/C taskkill /F /IM EditingCollections.exe";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        public bool waitForElementVisible(AutomationElement element, int timeOutSeconds)
        {
            int startTime = 0;
            while (startTime < timeOutSeconds && !element.IsAvailable)
            {
                startTime++;
                Thread.Sleep(1000);
                return true;
            }
            return false;
        }

    }
}
