
namespace Dane
{
    public class Board : DataApi
    {
        // keep the ratio 16:9
        public const int Width = 160;
        public const int Height = 90;
        
        public List<Task> BallTasks { get; private set; }
        public List<Ball> Balls { get; set; }
        public int Size { get; private set; }

        public Board()
        {
            BallTasks = new List<Task>();
            Balls = new List<Ball>();
            Size = Balls.Count;
        }

        /// <summary>
        /// Get current count of created Ball Tasks
        /// </summary>
        /// <returns>Task count</returns>
        public override int GetBallCount()
        {
            return Size;
        }

        /// <summary>
        /// Start new Task running a single Ball in loop
        /// </summary>
        public override void AddBall()
        {
            Ball b = new Ball(Size);
            Task t = new Task(b.MoveLoop);
            BallTasks.Add(t);
            Balls.Add(b);
            t.Start();
            Console.WriteLine($"Board: Task #{Size} created.");
            Size++;
        }

        /// <summary>
        /// Kills newest Task of a Ball in loop created
        /// </summary>
        public override void RemoveBall()
        {
            if (Size == 0)
                return;
            
            Balls[Size - 1].Token.can
            
            BallTasks.RemoveAt(Size - 1);
            Console.WriteLine($"Board: Task #{Size} ended.");
            Size--;
        }
    }
}
