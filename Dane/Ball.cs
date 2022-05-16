using System.Numerics;

namespace Dane
{
    public class Ball
    {
        private const float SpeedMin = 0.2f;
        private const float SpeedMax = 1.0f;
        private const float RadiusMin = 20.0f;
        private const float RadiusMax = 50.0f;
        private const float MassMin = 1.0f;
        private const float MassMax = 5.0f;

        public int Id { get; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; private set; }
        public float Mass { get; private set; }
        
        public Ball(int id)
        {
            Random r = new Random();

            Id = id;

            Position = new Vector2(
                r.NextSingle() * Board.Width,
                r.NextSingle() * Board.Height
            );

            Velocity = new Vector2(
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin,
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin
            );

            Radius = r.NextSingle() * (RadiusMax - RadiusMin) + RadiusMin;
            Mass = r.NextSingle() * (MassMax - MassMin) + MassMin;
            
            Console.WriteLine($"Ball: new Ball created. [ id: {Id}, " +
            $"position: {Position}, velocity: {Velocity}, radius: {Radius}, mass: {Mass} ]");
        }

        public void Move()
        {
            Position = Vector2.Add(Position, Velocity);
            if (Position.X <= 0)
            {
                Position = Position with { X = 0, Y = Position.Y };
                Velocity = Velocity with { X = -Velocity.X, Y = Velocity.Y };
            }

            if (Position.X >= Board.Width)
            {
                Position = Position with { X = Board.Width, Y = Position.Y };
                Velocity = Velocity with { X = -Velocity.X, Y = Velocity.Y };
            }

            if (Position.Y <= 0)
            {
                Position = Position with { X = Position.X, Y = 0 };
                Velocity = Velocity with { X = Velocity.X, Y = -Velocity.Y };
            }

            if (Position.Y >= Board.Height)
            {
                Position = Position with { X = Position.X, Y = Board.Height };
                Velocity = Velocity with { X = Velocity.X, Y = -Velocity.Y };
            }
        }
    }
}
