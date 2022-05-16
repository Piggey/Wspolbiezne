using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;

namespace Testy.TestyDanych
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void BoardConstructorTest()
        {
            Board b = new Board();
            Assert.AreEqual(0, b.Size);
        }

        [TestMethod]
        public void AddBallTest()
        {
            Board b = new Board();
            b.AddBall();

            Assert.AreEqual(1, b.Size);
            Assert.AreNotEqual(null, b.BallTasks[0]);
            Assert.IsInstanceOfType(b.BallTasks[0], typeof(Task));
        }

        [TestMethod]
        public void RemoveBallTest()
        {
            Board b = new Board();
            Assert.AreEqual(0, b.Size);

            b.AddBall();
            b.AddBall();
            
            Assert.AreEqual(2, b.Size);

            b.RemoveBall();
            Assert.AreEqual(1, b.Size);
            
            b.RemoveBall();
            Assert.AreEqual(0, b.Size);
            
            b.RemoveBall();
            Assert.AreEqual(0, b.Size);
        }
    }
}
