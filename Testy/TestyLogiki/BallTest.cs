using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logika;

namespace Testy
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Ball b = new Ball();

            Assert.IsTrue(b.x > 0);
            Assert.IsTrue(b.x < Board.WIDTH);

            Assert.IsTrue(b.y > 0);
            Assert.IsTrue(b.y < Board.HEIGHT);

            Assert.IsTrue(b.xSpeed >= 0.2);
            Assert.IsTrue(b.xSpeed <= 1);

            Assert.IsTrue(b.ySpeed >= 0.2);
            Assert.IsTrue(b.ySpeed <= 1);
        }

        [TestMethod]
        public void MoveTest()
        {
            Ball b = new Ball();
            b.x = Board.WIDTH - 4;
            b.xSpeed = 4;

            Assert.AreEqual(4, b.xSpeed);
            b.Move();

            Assert.AreEqual(Board.WIDTH, b.x);
            Assert.AreEqual(-4, b.xSpeed);

            b.y = Board.HEIGHT - 2;
            b.ySpeed = 3;
            Assert.AreEqual(3, b.ySpeed);

            b.Move();
            Assert.AreEqual(-3, b.ySpeed);

            b.x = 10;
            b.y = 2;
            b.xSpeed = 2;
            b.ySpeed = -1;

            Assert.AreEqual(10, b.x);
            Assert.AreEqual(2, b.y);

            b.Move();
            Assert.AreEqual(10 + 2, b.x);
            Assert.AreEqual(2 - 1, b.y);
        }
    }
}