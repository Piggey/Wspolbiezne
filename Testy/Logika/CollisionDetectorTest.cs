using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dane;
using Logika;

namespace Testy.Logika
{
    [TestClass]
    public class CollisionDetectorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var collisionDetector = new CollisionDetector();
            Assert.IsNotNull(collisionDetector);
        }

        [TestMethod]
        public void CheckCollisionTest()
        {
            // non-moving Ball gets hit by moving Ball
            // equal masses
            LogicApi b = LogicApi.CreateLogicApi();
            b.AddBall();
            b.AddBall();

            Ball b1 = b.GetBall(0);
            Ball b2 = b.GetBall(1);
            
            b1.Mass = 50;
            b2.Mass = 50;
            b1.Radius = 4;
            b2.Radius = 4;
            
            // assert value changed
            Assert.AreEqual(50, b.GetBall(0).Mass);
            Assert.AreEqual(4, b.GetBall(1).Radius);

            b1.Position = new Vector2(10, 10);
            b1.Velocity = new Vector2(3, 0);

            b2.Position = new Vector2(150, 10);
            b2.Velocity = Vector2.Zero;
            
            Assert.AreEqual(3, b1.Velocity.X);
            Assert.AreEqual(0, b2.Velocity.X);
            
            var collisionDetector = new CollisionDetector();
            // wait for collision to happen
            while (!collisionDetector.CheckCollision(b1, b2)) { }

            Assert.AreEqual(0, b1.Velocity.X);
            Assert.AreEqual(3, b2.Velocity.X);
            
            b.RemoveBall();
            b.RemoveBall();
        }
        
    }
}
