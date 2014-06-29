using CollisionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CollisionLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for QuadraticEquationTest and is intended
    ///to contain all QuadraticEquationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuadraticEquationTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CalcDelta
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CollisionLibrary.dll")]
        public void CalcDeltaTest()
        {
            QuadraticEquation_Accessor target = new QuadraticEquation_Accessor(1.0f, -8.0f, 8.0f);
            float expected = (float) Math.Sqrt(32.0f);
            float actual;
            target.CalcDelta();
            actual = target.delta;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcSmallerPosotiveX
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CollisionLibrary.dll")]
        public void CalcSmallerPositiveXTest()
        {
            QuadraticEquation target = new QuadraticEquation(1.0f, -8.0f, 8.0f);
            float expected = 4.0f - (float)Math.Sqrt(32.0f) / 2.0f;
            float actual;
            actual = target.CalcSmallerPositiveX();
            Assert.AreEqual(expected, actual);
        }
    }
}
