using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;

namespace Testy.TestyDanych
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            Ball b = new Ball();

            Assert.IsTrue(b.Position.X > 0);
            Assert.IsTrue(b.Position.X < Board.Width);

            Assert.IsTrue(b.Position.Y > 0);
            Assert.IsTrue(b.Position.Y < Board.Height);

            Assert.IsTrue(b.Velocity.X >= 0.2);
            Assert.IsTrue(b.Velocity.X <= 1);

            Assert.IsTrue(b.Velocity.Y >= 0.2);
            Assert.IsTrue(b.Velocity.Y <= 1);
        }

        [TestMethod]
        public void MoveTest()
        {
            Ball b = new Ball();
            b.Position = b.Position with { X = Board.Width - 4 };
            b.Velocity = b.Velocity with { X = 4 };

            Assert.AreEqual(4, b.Velocity.X);
            
            b.Move();

            Assert.AreEqual(Board.Width, b.Position.X);
            Assert.AreEqual(-4, b.Velocity.X);

            b.Position = b.Position with { Y = Board.Height - 2 };
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