using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;

namespace WPFTestDemo.Page
{
    public class MainScreen : ScreenBase
    {
        public MainScreen(AutomationElement element) : base(element) { }

        public Button Add => FindByXPath("//Button[@Name='Add']").AsButton();//FindByName("Add").AsButton();

        public void verifyTextBlock(string name, string expectedName, int timeOut)
        {
            TextBox expected = FindByName(name).AsTextBox();
            waitForElementVisible(expected, timeOut);
/*            Assert.AreEqual(expected.Name.ToString(), expectedName);*/
        }
    }
}
