using CollisionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CollisionLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for CollisionTest and is intended
    ///to contain all CollisionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CollisionTest
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
        ///A test for GetBallWallCollisionTime
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CollisionLibrary.dll")]
        public void GetBallWallCollisionTimeTestRightWall()
        {
            Collision target = new Collision();
            Ball2 b = new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 2.0f), 0.01f, 0.01f);
            Wall2 w = new Wall2(new Vector2(10.01f, 1.0f), WallOrientation.Right);
            float directionBallV = b.V.X;
            float expected = 2.0f;
            float actual;
            actual = target.GetBallWallCollisionTime(b, w, directionBallV);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetNextBallWallCollision
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CollisionLibrary.dll")]
        public void GetNextBallWallCollisionTestTwoDiffParrarelBalls()
        {
            List<Ball2> balls = new List<Ball2>();
            balls.Add(new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 2.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(7.0f, 0.0f), new Vector2(0.0f, 2.0f), 0.01f, 0.01f));
            List<Wall2> walls = new List<Wall2>();
            walls.Add(new Wall2(new Vector2(10.01f, 1.0f), WallOrientation.Right));
            Collision target = new Collision(balls, walls);
            target.GetNextBallWallCollision();
            Assert.AreEqual(target.NextCollisions[0].Obj1, balls[1]);
        }

        /// <summary>
        ///A test for GetNextBallWallCollision
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CollisionLibrary.dll")]
        public void GetNextBallWallCollisionTestTwoTheSameParrarelBalls()
        {
            List<Ball2> balls = new List<Ball2>();
            balls.Add(new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 2.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 2.0f), 0.01f, 0.01f));
            List<Wall2> walls = new List<Wall2>();
            walls.Add(new Wall2(new Vector2(10.01f, 1.0f), WallOrientation.Right));
            Collision target = new Collision(balls, walls);
            target.GetNextBallWallCollision();
            Assert.AreEqual(target.NextCollisions[0].Obj1, balls[0]);
            Assert.AreEqual(target.NextCollisions[1].Obj1, balls[1]);
        }


        /// <summary>
        ///A test for GetNextBallBallCollision
        ///</summary>
        [TestMethod()]
        public void GetNextBallBallCollisionTestOneColl()
        {
            List<Ball2> balls = new List<Ball2>();
            balls.Add(new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 0.0f), new Vector2(5.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 0.0f), new Vector2(10.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 10.0f), new Vector2(5.0f, 0.0f), 0.01f, 0.01f));
            Collision target = new Collision(balls, new List<Wall2>());
            target.GetNextBallBallCollision();
            Assert.AreEqual(target.NextCollisions.Count, 1);
            Assert.AreEqual(target.NextCollisions[0].Obj2, balls[1]);
            //string result = target.NextCollisions[0].Time + " / " + target.NextCollisions[0].Obj1.Id + " / " + target.NextCollisions[0].Obj2.Id;
        }

        /// <summary>
        ///A test for GetNextBallBallCollision
        ///</summary>
        [TestMethod()]
        public void GetNextBallBallCollisionTestTwoColl()
        {
            List<Ball2> balls = new List<Ball2>();
            balls.Add(new Ball2(new Vector2(5.0f, 0.0f), new Vector2(0.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 0.0f), new Vector2(5.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 0.0f), new Vector2(10.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 10.0f), new Vector2(5.0f, 0.0f), 0.01f, 0.01f));
            balls.Add(new Ball2(new Vector2(-5.0f, 0.001f), new Vector2(5.0f, 0.0f), 0.01f, 0.01f));
            Collision target = new Collision(balls, new List<Wall2>());
            target.GetNextBallBallCollision();
            Assert.AreEqual(target.NextCollisions.Count, 2);
            Assert.AreEqual(target.NextCollisions[0].Obj2, balls[1]);
            //string result = target.NextCollisions[0].Time + " / " + target.NextCollisions[0].Obj1.Id + " / " + target.NextCollisions[0].Obj2.Id + " || "
            //        + target.NextCollisions[1].Time + " / " + target.NextCollisions[1].Obj1.Id + " / " + target.NextCollisions[1].Obj2.Id;
        }
    }
}
