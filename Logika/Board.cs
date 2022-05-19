using System.Reactive;
using System.Reactive.Linq;
using Dane;

namespace Logika 
{
    public class Board : LogicApi, IObservable<int>
    {
        private List<Ball> Balls { get; }
        private int Size { get; set; }
        private static object _lock = new object();
        private CollisionDetector _collisionDetector;

        private IDisposable _unsubscriber;
        private readonly IObservable<EventPattern<BallChangedEventArgs>> _eventObservable;
        public event EventHandler<BallChangedEventArgs> BallChanged;

        public Board()
        {
            Balls = new List<Ball>();
            Size = Balls.Count;
            _eventObservable = Observable.FromEventPattern<BallChangedEventArgs>(this, "BallChanged");
            _collisionDetector = new CollisionDetector();
        }

        public override int GetBallCount()
        {
            return Size;
        }

        public override Ball AddBall()
        {
            Ball b = DataApi.CreateBall(); 
            Task t = new Task(b.Loop);
            Balls.Add(b);
            t.Start();
            Console.WriteLine($"Board: Task #{Size} created.");
            Size++;

            return b;
        }

        public override void RemoveBall()
        {
            if (Size == 0)
                throw new IndexOutOfRangeException("RemoveBall: There are no balls to be removed.");

            Size--;
            Balls[Size].Cancelled = true;
            Balls.RemoveAt(Size);
            Console.WriteLine($"Board: Task #{Size} ended.");
        }

        public override Ball GetBall(int id)
        {
            if (id < 0 || id >= Size)
                throw new IndexOutOfRangeException("GetBall: id out of range.");
            
            return Balls[id];
        }

        public override int GetBoardWidth()
        {
            return DataApi.GetBoardWidth();
        }

        public override int GetBoardHeight()
        {
            return DataApi.GetBoardHeight();
        }


        #region observer
        
            public virtual void Subscribe(IObservable<int> provider)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            
            public override void OnCompleted()
            {
                _unsubscriber.Dispose();
            }

            public override void OnError(Exception error)
            {
                throw error;
            }

            public override void OnNext(int value)
            {
                Monitor.Enter(_lock);
                try
                {
                    // check every ball against each other
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            if (i == j)
                                continue;

                            _collisionDetector.CheckCollision(Balls[i], Balls[j]);
                        }
                    }

                }
                catch (SynchronizationLockException ex)
                {
                    throw new Exception("CollisionCheck: could not synchronize.", ex);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }

            #endregion

        #region observable

            public override IDisposable Subscribe(IObserver<int> observer)
            {
                return _eventObservable.Subscribe(
                    x => observer.OnNext(x.EventArgs.BallId),
                    observer.OnError,
                    observer.OnCompleted
                );
            }

        #endregion
    }
}
