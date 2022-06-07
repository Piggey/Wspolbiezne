using Dane;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Text.Json;

namespace Logika 
{
    public abstract class LogicAbstractApi
    {
        public List<BallLogic> ballOperators;
        public static LogicAbstractApi CreateApi(DataAbstractApi data = default)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateApi());
        }     
        public abstract void CreateBalls(int numer);
        public abstract void StopBalls();
        public abstract ObservableCollection<Ball> Balls { get; }
        public abstract List<BallLogic> GetBalls();
        public abstract int Height { get; }
        public abstract int Width { get; }
        public abstract object LockFile { get; }
        public abstract string FileName { get; }

        public abstract void Collisions(int i, int i1, int ball1Radius, Ball ball1);
        public abstract void BallCrash(Ball ball2, Ball ball3);
    }

    public class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi data;
        private object _lock = new object();

        public LogicApi(DataAbstractApi dataAbstractApi)
        {
            data = dataAbstractApi;
        }
        public override void CreateBalls(int number)
        {
            ballOperators = new List<BallLogic>();
            data.CreateBalls(number);
            foreach (Ball ball in data.GetBalls())
            {
                ballOperators.Add(new BallLogic(ball));
                ball.PropertyChanged += checkMovement;
            }
        }
        public override void StopBalls() => data.StopBalls();
        public override List<BallLogic> GetBalls()
        {
            return ballOperators;
        }
        public override ObservableCollection<Ball> Balls => data.GetBalls();
        public override int Width => data.Width;
        public override int Height => data.Height;
        public override object LockFile => data.LockFile;
        public override string FileName => data.FileName;
        private string jsonString;
        Vector2 newVelocity1;
        Vector2 newVelocity2;
        Vector2 oldVelocity1;
        Vector2 oldVelocity2;


        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


        public void checkMovement(object sender, PropertyChangedEventArgs e)
        {
            Ball b = (Ball)sender;
            if (e.PropertyName == "VectorCurrent")
            {
                Collisions(Width, Height, b.Radius, b);
            }
        }
        public override void Collisions(int width, int height, int radius, Ball ball)
        {
            foreach (BallLogic thisBall in ballOperators)
            {
                if (thisBall.Ball == ball)
                {
                    continue;
                }
                float distance;
                lock(_lock){
                    distance = Vector2.Distance(ball.VectorCurrent, thisBall.Ball.VectorCurrent);
                    if (distance <= (ball.Radius + thisBall.Ball.Radius))
                    {
                        if (Vector2.Distance(ball.VectorCurrent, thisBall.Ball.VectorCurrent)
                        - Vector2.Distance(ball.VectorCurrent + ball.Velocity, thisBall.Ball.VectorCurrent + thisBall.Ball.Velocity) > 0)
                        {
                            BallCrash(ball, thisBall.Ball);
                        }
                    }
                }
            }
            if (ball.X + ball.VX > Width - radius)
            {
                ball.VX = ball.VX * (-1);
            }
            else if (ball.X + ball.VX < radius)
            {
                ball.VX = ball.VX * (-1);
            }
            else if (ball.Y + ball.VY > height - radius)
            {
                ball.VY = ball.VY * (-1);
            }
            else if (ball.Y + ball.VY < radius)
            {
                ball.VY = ball.VY * (-1);
            }
        }
        public override void BallCrash(Ball b1, Ball b2)
        {
            newVelocity1 = (b1.Velocity * (b1.Mass - b2.Mass) + b2.Velocity * 2 * b2.Mass) / (b1.Mass + b2.Mass);
            newVelocity2 = (b2.Velocity * (b2.Mass - b1.Mass) + b1.Velocity * 2 * b1.Mass) / (b1.Mass + b2.Mass);
            oldVelocity1 = b1.Velocity;
            oldVelocity2 = b2.Velocity;
            b1.Velocity = newVelocity1;
            b2.Velocity = newVelocity2;
            jsonString = "[ \"Date/Time\": \"" + DateTime.Now.ToString() + "\",\n  \"Collision\": " 
                + "\n\"Ball 1\": " + JsonSerializer.Serialize(b1, options) + " \"oldVX\":" + JsonSerializer.Serialize(oldVelocity1.X, options)
                + "\n  \"oldVY\":" + JsonSerializer.Serialize(oldVelocity1.Y, options)
                + "\n\"Ball 2\": " + JsonSerializer.Serialize(b2, options) + " \"oldVX\":" + JsonSerializer.Serialize(oldVelocity2.X, options)
                + "\n  \"oldVY\":" + JsonSerializer.Serialize(oldVelocity2.Y, options) + " ]\n";
            lock (LockFile)
            {
                File.AppendAllText(FileName, jsonString);
            }
        }
    }
}