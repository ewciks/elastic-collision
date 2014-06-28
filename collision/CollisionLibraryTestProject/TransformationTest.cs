using CollisionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace CollisionLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for TransformationTest and is intended
    ///to contain all TransformationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TransformationTest
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
        ///A test for CalcNormal
        ///</summary>
        [TestMethod()]
        public void CalcNormalTest()
        {
            Ball2 b1 = new Ball2(new Vector2(0.0f, 0.0f));
            Ball2 b2 = new Ball2(new Vector2(0.0f, 9.0f));
            Vector2 expected = new Vector2(0.0f, 1.0f);
            Vector2 actual;
            actual = Transformation.CalcNormal(b1, b2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcTangent
        ///</summary>
        [TestMethod()]
        public void CalcTangentTestWithGivenNormal()
        {
            Vector2 normal = new Vector2(1.0f, 2.0f);
            Vector2 expected = new Vector2(-2.0f, 1.0f);
            Vector2 actual;
            actual = Transformation.CalcTangent(normal);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcTangent
        ///</summary>
        [TestMethod()]
        public void CalcTangentTest()
        {
            Ball2 b1 = new Ball2(new Vector2(0.0f, 0.0f));
            Ball2 b2 = new Ball2(new Vector2(0.0f, 9.0f));
            Vector2 expected = new Vector2(-1.0f, 0.0f);
            Vector2 actual;
            actual = Transformation.CalcTangent(b1, b2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Projection
        ///</summary>
        [TestMethod()]
        public void ProjectionTest()
        {
            Vector2 v = new Vector2(1.0f, 2.0f);
            Vector2 axis = new Vector2(3.0f, 4.0f);
            float expected = v.X * axis.X + v.Y * axis.Y;
            float actual;
            actual = Transformation.Projection(v, axis);
            Assert.AreEqual(expected, actual);
        }
    }
}
