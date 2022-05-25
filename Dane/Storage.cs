using System.Collections.ObjectModel;

namespace Dane
{
    public class Storage 
    {
        public static int _width = 800;
        public static int _height = 400;
        private Generator _generator = new Generator();
        private ObservableCollection<Ball> _balls = new ObservableCollection<Ball>();
        private List<Task> _tasks = new List<Task>();
        CancellationTokenSource tokenSource;
        CancellationToken token;
        private object _lock = new object();

        public Storage()
        {
        }
        public int Width { get => _width; }
        public int Height { get => _height; }
        public Storage GetStorage()
        {
            return this;
        }
        
        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
        }

        public void CreateBalls(int number)
        {
            if (number > 0)
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                for (int i = 0; i < number; i++)
                {
                    Ball ball = _generator.GenerateBall();
                    AddBall(ball);
                }
            }
            Moving();
        }

        public void StopBalls()
        {
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                _tasks.Clear();
                _balls.Clear();
            }
        }

        public async void Moving()
        {
            foreach (Ball ball in _balls)
            {
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(8);
                        lock(_lock)
                        {
                            ball.UpdatePosition();
                            while (ball.CanMove == false) { }
                        }
                        ball.UpdatePosition();
                        try { token.ThrowIfCancellationRequested(); }
                        catch (System.OperationCanceledException) { break; } //Rzuca OperationCanceledException jeżeli jest zgłoszone cancel.
                    }
                });
                _tasks.Add(task);
            }
        }

        public Generator Generator
        {
            get => _generator;
        }

        public ObservableCollection<Ball> Balls
        {
            get => _balls;
        }

        public List<Task> Tasks
        {
            get => _tasks;
        }
    }
}