using System.Collections.ObjectModel;
using System.Text.Json;

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
        private object _lockFile = new object();
        string fileName = "logs.json";


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
                Moving();
            }
            
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

        public void Moving()
        {
            foreach (Ball ball in _balls)
            {
                Task task = Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(5);
                        ball.UpdatePosition();
                        try { token.ThrowIfCancellationRequested(); }
                        catch (System.OperationCanceledException) { break; } //Rzuca OperationCanceledException jeżeli jest zgłoszone cancel.
                    }
                });
                _tasks.Add(task);
            }
            _tasks.Add(Task.Run(async () =>
            {
                System.IO.File.WriteAllText(fileName, string.Empty);
                while (true)
                { 
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(_balls, options);
                    string jsonString2 = "[ \"Date/Time\": \"" + DateTime.Now.ToString() + "\",\n  \"Balls\": " + jsonString + " ]\n"; 
                    lock (_lockFile)
                    {
                        File.AppendAllText(fileName, jsonString2);
                    }
                    try { token.ThrowIfCancellationRequested(); }
                    catch (System.OperationCanceledException) { break; } //Rzuca OperationCanceledException jeżeli jest zgłoszone cancel.
                    await Task.Delay(2000);
                }
            }));
        }

        public Generator Generator
        {
            get => _generator;
        }

        public object LockFile
        {
            get => _lockFile;
        }

        public string FileName
        {
            get => fileName;
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