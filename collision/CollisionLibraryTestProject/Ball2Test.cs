using CollisionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace CollisionLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for Ball2Test and is intended
    ///to contain all Ball2Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Ball2Test
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
        ///A test for Ball2 Constructor
        ///</summary>
        [TestMethod()]
        public void Ball2ConstructorTestStationaryWithCoord()
        {
            Vector2 coord = new Vector2(13.1f, 2.0f);
            Ball2 target = new Ball2(coord);
            Ball2 expected = new Ball2(Vector2.Zero, coord, 0.0f, 0.0f);
            Assert.AreEqual(target, expected);
        }

        /// <summary>
        ///A test for Ball2 Constructor
        ///</summary>
        [TestMethod()]
        public void Ball2ConstructorTestDefault()
        {
            Ball2 target = new Ball2();
            Ball2 expected = new Ball2(Vector2.Zero, Vector2.Zero, 0.0f, 0.0f);
            Assert.AreEqual(target, expected);
        }

        /// <summary>
        ///A test for Ball2 Constructor
        ///</summary>
        [TestMethod()]
        public void Ball2ConstructorTest2()
        {
            Vector2 vel = new Vector2(23.0f, -13.1f);
            Vector2 coord = new Vector2(2.0f, 1.0f);
            float mass = 0.002f;
            float rad = 0.1f;
            Ball2 target = new Ball2(vel, coord, mass, rad);
            Ball2 expected = new Ball2();
            expected.V = vel;
            expected.Coordinates = coord;
            expected.M = mass;
            expected.R = rad;
            Assert.AreEqual(target, expected);
        }

        /// <summary>
        ///A test for Coordinates
        ///</summary>
        [TestMethod()]
        public void CoordinatesTest()
        {
            Ball2 target = new Ball2(new Vector2(0.2f, -10.1f));
            Vector2 expected = new Vector2(0.2f, -10.1f);
            Vector2 actual;
            target.Coordinates = expected;
            actual = target.Coordinates;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for M
        ///</summary>
        [TestMethod()]
        public void MTest()
        {
            Ball2 target = new Ball2(Vector2.Zero, Vector2.Zero, 0.1f, 0.0f);
            float expected = 0.1f;
            float actual;
            target.M = expected;
            actual = target.M;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for R
        ///</summary>
        [TestMethod()]
        public void RTest()
        {
            Ball2 target = new Ball2(Vector2.Zero, Vector2.Zero, 0.0f, 0.002f);
            float expected = 0.002f; 
            float actual;
            target.R = expected;
            actual = target.R;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for V
        ///</summary>
        [TestMethod()]
        public void VTest()
        {
            Ball2 target = new Ball2(new Vector2(-11.3f, 2.0f), Vector2.Zero, 0.0f, 0.0f);
            Vector2 expected = new Vector2(-11.3f, 2.0f);
            Vector2 actual;
            target.V = expected;
            actual = target.V;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for instances counter - creating two balls
        ///</summary>
        [TestMethod()]
        public void InstancesTestTwoBalls()
        {
            Ball2 target = new Ball2();
            Ball2 target2 = new Ball2();
            int expected = 2;
            int actual = Ball2.GetActiveIntances();
            Assert.AreEqual(expected, actual);
        }

    }
}
