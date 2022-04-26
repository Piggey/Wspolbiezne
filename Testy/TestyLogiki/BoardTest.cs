using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logika;

namespace Testy
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Board b = new Board();
            Assert.AreEqual(0, b.size);
        }

        [TestMethod]
        public void AddBallTest()
        {
            Board b = new Board();
            b.AddBall();

            Assert.AreEqual(1, b.size);
            Assert.AreNotEqual(null, b.balls[0]);
            Assert.IsInstanceOfType(b.balls[0], typeof(Ball));
        }

        [TestMethod]
        public void RemoveBallTest()
        {
            Board b = new Board();

            Assert.AreEqual(0, b.GetBallCount());
            b.RemoveBall();
            Assert.AreEqual(0, b.GetBallCount());

            b.AddBall();
            Assert.AreEqual(1, b.GetBallCount());
            Assert.AreNotEqual(null, b.balls[0]);
            Assert.IsInstanceOfType(b.balls[0], typeof(Ball));

            b.RemoveBall();
            Assert.AreEqual(0, b.GetBallCount());
        }
    }
}
