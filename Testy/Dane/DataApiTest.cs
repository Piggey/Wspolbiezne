using System.Collections.ObjectModel;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;

namespace Testy.Dane
{
    [TestClass]
    public class DataApiTest
    {
        [TestMethod]
        public void DataApiConstructorTest()
        {
            DataAbstractApi data = DataAbstractApi.CreateApi();
            Assert.IsTrue(data.Height == 400);
            Assert.IsTrue(data.Width == 800);
            
            data.CreateBalls(2);
            ObservableCollection<Ball> balls = data.GetStorage().Balls;
            
            Assert.IsTrue(balls.Count == 2);
            Assert.IsTrue(balls[0].X < data.Width);
            Assert.IsTrue(balls[0].Y < data.Height);
            Assert.IsTrue(balls[0].X > 0);
            Assert.IsTrue(balls[0].Y > 0);
            
            data.StopBalls();
            Assert.IsTrue(balls.Count == 0);
        }

        [TestMethod]
        public void GenerateBallTest()
        {
            Generator generator = new Generator();
            Ball ball = generator.GenerateBall();
            
            Assert.IsTrue(ball.VectorCurrent.X >= 2 + generator.Radius && ball.VectorCurrent.X <= Storage._width - generator.Radius - 1);
            Assert.IsTrue(ball.VectorCurrent.Y >= 2 + generator.Radius && ball.VectorCurrent.Y <= Storage._height - generator.Radius - 1);
            Assert.IsTrue(ball.Mass <= 2 && ball.Mass > 0);
        }

        [TestMethod]
        public void BallConstructor()
        {
            Vector2 velocity = new Vector2(1, 4);
            Ball ball = new Ball(2, 3, 5, 2, velocity);
            Assert.IsTrue(ball.VectorCurrent.X == 2);
            Assert.IsTrue(ball.VectorCurrent.Y == 3);
            Assert.IsTrue(ball.Radius == 5);
            Assert.IsTrue(ball.Mass == 2);
            Assert.IsTrue(ball.VX == 1);
            Assert.IsTrue(ball.VY == 4);
        }

        [TestMethod]
        public void StorageCreateBallsTest()
        {
            Storage storage = new Storage();
            int numberBalls0 = storage.Balls.Count;
            Assert.IsTrue(numberBalls0 == 0);
            storage.CreateBalls(0);
            Assert.IsTrue(numberBalls0 == storage.Balls.Count);
            storage.CreateBalls(2);
            Assert.IsFalse(numberBalls0 == storage.Balls.Count);
            Assert.IsTrue(2 == storage.Balls.Count);
            storage.CreateBalls(3);
            Assert.IsTrue(5 == storage.Balls.Count);
        }

        [TestMethod]
        public void StorageStopBallsTest()
        {
            Storage storage = new Storage();
            storage.CreateBalls(3);
            Assert.IsTrue(3 == storage.Balls.Count);
            storage.StopBalls();
            Assert.IsTrue(0 == storage.Balls.Count);
            storage.CreateBalls(2);
            storage.CreateBalls(2);
            Assert.IsTrue(4 == storage.Balls.Count);
            storage.StopBalls();
            Assert.IsTrue(0 == storage.Balls.Count);
        }
    }
}

