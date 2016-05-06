using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClasses;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var model = new PavementModel
            {
                H1 = 3,
                H2 = 4,
                H3 = 6,
                E1 = 1000,
                E2 = 2000,
                E3 = 3000,
                E4 = 300
            };
            var calc = new CalcModule.CalcModule(@"I:\bntu\prepod\diplomy\2015-16\Bychkouski\Project\UnitTestProject\bin\Debug\ndt");
            model = calc.Calculate(model);
        }
    }
}
