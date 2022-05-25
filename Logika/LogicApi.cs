using Dane;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;

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
        public abstract void Collisions(int width, int height, int radius, Ball ball);
        public abstract void BallCrash(Ball b1, Ball b2);
        public abstract ObservableCollection<Ball> Balls { get; }
        public abstract List<BallLogic> GetBalls();
        public abstract int Height { get; }
        public abstract int Width { get; }
    }

    public class LogicApi : LogicAbstractApi
    {
        private readonly DataAbstractApi data;

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
                ball.PropertyChanged += CheckMovement;
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

        public void CheckMovement(object sender, PropertyChangedEventArgs e)
        {
            Ball b = (Ball)sender;
            if (e.PropertyName == "VectorCurrent")
            {
                Collisions(Width, Height, b.Radius, b);
                b.CanMove = true;
            }
        }
        public override async void Collisions(int width, int height, int radius, Ball ball)
        {
            foreach (BallLogic thisBall in ballOperators)
            {
                if (thisBall.Ball == ball)
                {
                    continue;
                }
                thisBall.Ball.CanMove = false;
                float distance = Vector2.Distance(ball.VectorCurrent, thisBall.Ball.VectorCurrent);
                if (distance <= (ball.Radius + thisBall.Ball.Radius))
                {
                    if (Vector2.Distance(ball.VectorCurrent, thisBall.Ball.VectorCurrent)
                    - Vector2.Distance(ball.VectorCurrent + ball.Velocity, thisBall.Ball.VectorCurrent + thisBall.Ball.Velocity) > 0)
                    {
                        BallCrash(ball, thisBall.Ball);
                    }
                }
                thisBall.Ball.CanMove = true;
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
            Vector2 newVelocity1 = (b1.Velocity * (b1.Mass - b2.Mass) + b2.Velocity * 2 * b2.Mass) / (b1.Mass + b2.Mass);
            Vector2 newVelocity2 = (b2.Velocity * (b2.Mass - b1.Mass) + b1.Velocity * 2 * b1.Mass) / (b1.Mass + b2.Mass);
            if (newVelocity1.X > 5) newVelocity1.X = 5;
            if (newVelocity1.Y > 5) newVelocity1.Y = 5;
            if (newVelocity1.Y < -5) newVelocity1.Y = -5;
            if (newVelocity1.X < -5) newVelocity1.X = -5;
            b1.Velocity = newVelocity1;
            b2.Velocity = newVelocity2;
        }
    }
}