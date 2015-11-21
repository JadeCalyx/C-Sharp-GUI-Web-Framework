using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestSuite
{
    /// <summary>
    /// The Dev class is used for the development of tests. It is a type of sandbox.
    /// 
    /// This uses NUnit as the test runner. It should be installed as a NuGet package
    /// (look in packages.config, you should see both the NUnit and Nunit3TestAdapter
    /// packages). To run, build the solution, then click menu item
    /// Test-->Windows--Test Explorer. The test explorer pane should open and have a list
    /// of tests you can run. Right click the HelloWorldTest and run it as
    /// an example of how to run a test and view its output.
    /// </summary>
    [TestFixture]
    public class Dev
    {
        [OneTimeSetUp]
        public void ClassSetup()
        {

        }
        [OneTimeTearDown]
        public void ClassTeardown()
        {

        }
        [SetUp]
        public void TestSetup()
        {

        }
        [TearDown]
        public void TestTeardown()
        {

        }

        [Test]
        public void HelloWorldTest()
        {
            Console.WriteLine("hello world");
            Assert.That(true, "test should have passed");
        }


    }
}
