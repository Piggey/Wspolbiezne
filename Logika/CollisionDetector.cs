using System.Numerics;
using Dane;

namespace Logika
{
    public class CollisionDetector
    {
        private const int Cooldown = 5;
        
        /// <summary>
        /// Check if collision between 2 Balls appear.
        /// Bounce them off if it does.
        /// </summary>
        /// <param name="b1">first Ball</param>
        /// <param name="b2">second Ball collision is checked against</param>
        /// <returns>returns true if collision was detected</returns>
        public bool CheckCollision(Ball b1, Ball b2)
        {
            float dist = Vector2.Distance(b1.Position, b2.Position);
            float r2 = b1.Radius + b2.Radius;
            
            // b2 colliding with b1
            if (Vector2.Distance(b1.Position, b2.Position) <= b1.Radius + b2.Radius)
            {
                Console.WriteLine($"CollisionDetector: Ball#{b1.Id} colliding with Ball#{b2.Id} detected.");
                
                // formulas from https://en.wikipedia.org/wiki/Elastic_collision
                float m1 = b1.Mass;
                Vector2 u1 = b1.Velocity;
                
                float m2 = b2.Mass;
                Vector2 u2 = b2.Velocity;

                float masses1 = (m1 - m2) / (m1 + m2);
                float masses2 = (2 * m2) / (m1 + m2);
                Vector2 v1 = Vector2.Multiply(masses1, u1) +
                             Vector2.Multiply(masses2, u2);
                
                masses1 = (2 * m1) / (m1 + m2);
                masses2 = (m2 - m1) / (m1 + m2);
                Vector2 v2 = Vector2.Multiply(masses1, u1) +
                             Vector2.Multiply(masses2, u2);

                b1.Velocity = v1;
                b2.Velocity = v2;
                
                Console.WriteLine($"CollisionDetector: Ball#{b1.Id} Velocity: {u1} -> {v1}");
                Console.WriteLine($"CollisionDetector: Ball#{b2.Id} Velocity: {u2} -> {v2}");

                b1.CollisionCooldown = Cooldown;
                b2.CollisionCooldown = Cooldown;
                
                return true;
            }

            if (b1.CollisionCooldown > 0)
                b1.CollisionCooldown--;

            if (b2.CollisionCooldown > 0)
                b2.CollisionCooldown--;

            return false;
        }
    }
}
