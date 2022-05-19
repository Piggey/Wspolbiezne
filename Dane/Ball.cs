using System.Numerics;

namespace Dane
{
    public class Ball
    {
        private const float SpeedMin = 2f;
        private const float SpeedMax = 5f;
        
        private const float RadiusMin = 15f;
        private const float RadiusMax = 20f;

        public int Id { get; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; set; }
        public float Mass { get; set; }
        public bool Cancelled { get; set; }
        
        public int CollisionCooldown { get; set; }

        private readonly IList<IObserver<int>> _observers; 
        
        public Ball(int id)
        {
            Random r = new Random();

            Id = id;
            Cancelled = false;

            _observers = new List<IObserver<int>>();

            Position = new Vector2(
                r.NextSingle() * DataApi.GetBoardWidth(),
                r.NextSingle() * DataApi.GetBoardHeight()
            );

            Velocity = new Vector2(
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin,
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin
            );

            Radius = r.NextSingle() * (RadiusMax - RadiusMin) + RadiusMin;
            Mass = Radius; // make mass relative to radius 
            
            Console.WriteLine($"Ball: new Ball created. [ id: {Id}, " +
            $"position: {Position}, velocity: {Velocity}, radius: {Radius}, mass: {Mass} ]");
        }

        public Ball()
        {
            Random r = new Random();

            Id = -1;
            Cancelled = false;
            
            _observers = new List<IObserver<int>>();

            Position = new Vector2(
                r.NextSingle() * DataApi.GetBoardWidth(),
                r.NextSingle() * DataApi.GetBoardHeight()
            );

            Velocity = new Vector2(
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin,
                r.NextSingle() * (SpeedMax - SpeedMin) + SpeedMin
            );

            Radius = r.NextSingle() * (RadiusMax - RadiusMin) + RadiusMin;
            Mass = Radius; 
            
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

            if (Position.X >= DataApi.GetBoardWidth())
            {
                Position = Position with { X = DataApi.GetBoardWidth(), Y = Position.Y };
                Velocity = Velocity with { X = -Velocity.X, Y = Velocity.Y };
            }

            if (Position.Y <= 0)
            {
                Position = Position with { X = Position.X, Y = 0 };
                Velocity = Velocity with { X = Velocity.X, Y = -Velocity.Y };
            }

            if (Position.Y >= DataApi.GetBoardHeight())
            {
                Position = Position with { X = Position.X, Y = DataApi.GetBoardHeight() };
                Velocity = Velocity with { X = Velocity.X, Y = -Velocity.Y };
            }
        }

        public void Loop()
        {
            while (!Cancelled)
                Move();
        }
    }
}
