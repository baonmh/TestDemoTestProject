using FlaUI.Core.AutomationElements;

namespace WPFTestDemo.Screen
{
    public class SubmitScreen : ScreenBase
    {
        public SubmitScreen(AutomationElement element) : base(element) { }

        public Button Submit => FindByName("Submit").AsButton();
        public TextBox Description => FindByXPath("//Text[@Name='Description']/following-sibling::Edit").AsTextBox();
        public TextBox Price => FindByXPath("//Text[@Name='Price']/following-sibling::Edit").AsTextBox();
        public TextBox Date => FindByXPath("//Text[@Name='Date Offer Ends']/following-sibling::Edit").AsTextBox();

        public void enterItemInformation(string desc, string price, string date)
        {
            Description.Text = desc;
            Price.Text = price;
            Date.Text = date;
        }

        public void submitItem()
        {
            Submit.Click();
        }
    }
}
