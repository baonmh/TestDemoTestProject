using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPFTestDemo.Config;
using WPFTestDemo.Page;
using WPFTestDemo.Screen;
using System.Threading;

namespace WPFTestDemo.Test
{
    [TestFixture]
    public class MainScreenTest : TestBase
    {
        [TestCase("Item Test 5", "10", "4/20/2022", "Item Test 5",
            TestName = "Veriy User Should Be Able To Add New Item", 
            Category = "Set1")]
        public void VeriyUserShouldBeAbleToAddNewItem(string desc, string price, string date, string expectedName)
        {
            MainScreen mainScreen = new MainScreen(window);

            mainScreen.MaximineWindow();

            mainScreen.Add.Click();

            SwitchToTopWindow();

            SubmitScreen submitScreen = new SubmitScreen(window);

            submitScreen.enterItemInformation(desc, price, date);

            submitScreen.submitItem();

            submitScreen.CloseWindow();

            mainScreen.verifyTextBlock(desc, expectedName, 5);
        }

        [TestCase("Item Test 4", "10", "4/20/2022", "Wrong",
    TestName = "Veriy User Should Be Able To Add New Item - Wrong",
    Category = "Set1")]
        public void VeriyUserShouldBeAbleToAddNewItem2(string desc, string price, string date, string expectedName)
        {
            MainScreen mainScreen = new MainScreen(window);

            mainScreen.MaximineWindow();

            mainScreen.Add.Click();

            SwitchToTopWindow();

            SubmitScreen submitScreen = new SubmitScreen(window);

            submitScreen.enterItemInformation(desc, price, date);

            submitScreen.submitItem();

            submitScreen.CloseWindow();

            mainScreen.verifyTextBlock(desc, expectedName, 5);
        }

/*        [TestCase("Ignore",
            TestName = "Ignore This Test",
            Category = "Set2")]
        public void Ignore(string comment)
        {
            Console.WriteLine(comment);
            Assert.Ignore(comment);
        }

        [TestCase("Flaky Test",
            TestName = "Flaky Test",
            Category = "Set2")]
        public void FlakyTest(string comment)
        {
            Random random = new Random();
            int num = random.Next(2);
            Assert.AreEqual(1, num);
        }*/

    }
}
