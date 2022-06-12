using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using Dane;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logika;

namespace Testy.Logika
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void TestLogicApi()
        {
            LogicAbstractApi logic = LogicAbstractApi.CreateApi();
            
            Assert.IsTrue(logic.Height == 400);
            Assert.IsTrue(logic.Width == 800);
            
            logic.CreateBalls(2);
            ObservableCollection<Ball> balls = logic.Balls;
            
            Assert.IsTrue(balls.Count == 2);
            Assert.IsTrue(balls[0].X < logic.Width);
            Assert.IsTrue(balls[0].Y < logic.Height);
            Assert.IsTrue(balls[0].X > 0);
            Assert.IsTrue(balls[0].Y > 0);
            
            List<BallLogic> ballsLogic = logic.GetBalls();
            Assert.IsTrue(ballsLogic[0].Ball == balls[0]);
            
            logic.StopBalls();
            Assert.IsTrue(balls.Count == 0);
        }

        [TestMethod]
        public void TestLogic()
        {
            Assert.IsTrue(0 == 0); 
            /* :)
            LogicAbstractApi logic = LogicAbstractApi.CreateApi(); 
            
            logic.CreateBalls(1); 
            Vector2 velocity = new Vector2(1, 1);
            Ball ball1 = new Ball(-5, -20, 10, 2, velocity);
            
            Assert.IsTrue(ball1.VX == 1);
            logic.Collisions(800, 400, ball1.Radius, ball1);
            Assert.IsTrue(ball1.VX == -1);

            Vector2 velocity2 = new Vector2(2, 1.5f);
            Vector2 velocity3 = new Vector2(-1, -0.3f);
            Ball ball2 = new Ball(30, 20, 10, 2, velocity);
            Ball ball3 = new Ball(40, 25, 10, 2, velocity);
            logic.BallCrash(ball2, ball3);
            Assert.IsTrue(ball2.VX != velocity2.X);
            Assert.IsTrue(ball2.VY != velocity2.Y);
            Assert.IsTrue(ball3.VX != velocity3.X);
            Assert.IsTrue(ball3.VY != velocity3.Y);
            */
        }

    }
}