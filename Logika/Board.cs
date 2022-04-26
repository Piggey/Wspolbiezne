
namespace Logika
{
    public class Board : LogicAPI
    {
        // keep the aspect ratio 16:9
        public const int WIDTH = 160;
        public const int HEIGHT = 90;

        public int size { get; private set; }
        public List<Ball> balls { get; private set; }

        public Board()
        {
            balls = new List<Ball>();
            this.size = balls.Count;
        }

        public override void AddBall()
        {
            balls.Add(new Ball());
            this.size++;
        }

        public override void RemoveBall()
        {
            if (this.size == 0)
                return;

            balls.RemoveAt(this.size - 1);
            this.size--;
        }

        public void Loop()
        {
            while (true)
            {
                foreach (var ball in balls)
                    ball.Move();
            }
        }

        public override void RunSimulation()
        {
            Task simulation = new Task(Loop);
            simulation.Start();
        }

        public override int GetBallCount()
        {
            return this.size;
        }
    }
}
