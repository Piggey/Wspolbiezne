using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;

namespace Testy.Dane
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            Ball b = DataApi.CreateBall();

            Assert.IsTrue(b.Position.X > 0);
            Assert.IsTrue(b.Position.X < DataApi.GetBoardWidth());

            Assert.IsTrue(b.Position.Y > 0);
            Assert.IsTrue(b.Position.Y < DataApi.GetBoardHeight());

            Assert.IsTrue(b.Velocity.X >= 2);
            Assert.IsTrue(b.Velocity.X <= 5);

            Assert.IsTrue(b.Velocity.Y >= 2);
            Assert.IsTrue(b.Velocity.Y <= 5);
        }

        [TestMethod]
        public void MoveTest()
        {
            Ball b = DataApi.CreateBall();
            b.Position = b.Position with { X = DataApi.GetBoardWidth() - 4 };
            b.Velocity = b.Velocity with { X = 4 };

            Assert.AreEqual(4, b.Velocity.X);
            
            b.Move();

            Assert.AreEqual(DataApi.GetBoardWidth(), b.Position.X);
            Assert.AreEqual(-4, b.Velocity.X);

            b.Position = b.Position with { Y = DataApi.GetBoardHeight() - 2 };
            b.Velocity = b.Velocity with { Y = 3 };
            Assert.AreEqual(3, b.Velocity.Y);

            b.Move();
            
            Assert.AreEqual(-3, b.Velocity.Y);

            b.Position = b.Position with { X = 10, Y = 2 };
            b.Velocity = b.Velocity with { X = 2, Y = -1 };

            Assert.AreEqual(10, b.Position.X);
            Assert.AreEqual(2, b.Position.Y);

            b.Move();
            
            Assert.AreEqual(10 + 2, b.Position.X);
            Assert.AreEqual(2 - 1, b.Position.Y);
        }
    }
}