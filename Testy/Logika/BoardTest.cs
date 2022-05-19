using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logika;
using Dane;

namespace Testy.Logika
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void BoardConstructorTest()
        {
            LogicApi b = new Board();
            Assert.AreEqual(0, b.GetBallCount());
        }

        [TestMethod]
        public void AddBallTest()
        {
            LogicApi b = new Board();
            b.AddBall();

            Assert.AreEqual(1, b.GetBallCount());
            Assert.IsNotNull(b.GetBall(0));
            Assert.IsNotNull(b.GetBall(0));
            Assert.IsInstanceOfType(b.GetBall(0), typeof(Ball));
        }

        [TestMethod]
        public void RemoveBallTest()
        {
            LogicApi b = new Board();
            Assert.AreEqual(0, b.GetBallCount());

            b.AddBall();
            b.AddBall();
            
            Assert.AreEqual(2, b.GetBallCount());

            b.RemoveBall();
            Assert.AreEqual(1, b.GetBallCount());
            
            b.RemoveBall();
            Assert.AreEqual(0, b.GetBallCount());

            Assert.ThrowsException<IndexOutOfRangeException>(() => b.RemoveBall());
        }

        [TestMethod]
        public void GetBallTest()
        {
            LogicApi b = new Board();
            Assert.ThrowsException<IndexOutOfRangeException>(() => b.GetBall(1));
            Assert.ThrowsException<IndexOutOfRangeException>(() => b.GetBall(-1));
            
            b.AddBall();
            b.AddBall();
            Assert.AreEqual(2, b.GetBallCount());

            Ball ball = b.GetBall(0);
            Assert.IsNotNull(ball);
        }

        [TestMethod]
        public void GetBallCountTest()
        {
            LogicApi b = new Board();
            Assert.AreEqual(0, b.GetBallCount());
            
            b.AddBall();
            Assert.AreEqual(1, b.GetBallCount());
            
            b.RemoveBall();
            Assert.AreEqual(0, b.GetBallCount());
        }
    }
}
