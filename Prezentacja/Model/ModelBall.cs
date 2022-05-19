using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Logika;

namespace Prezentacja.Model
{
    public class ModelBall : ModelApi
    {
        private readonly LogicApi _logicApi;
        public event EventHandler<ModelBallChangedEventArgs>? BallChanged;

        private readonly IObservable<EventPattern<ModelBallChangedEventArgs>>? _eventObservable;
        private readonly List<BallInModel> _balls;

        public ModelBall()
        {
            _logicApi = LogicApi.CreateLogicApi();
            _balls = new List<BallInModel>();
            
            IDisposable observer = _logicApi.Subscribe<int>(
                x => _balls[x].Move(_logicApi.GetBall(x).Position.X, _logicApi.GetBall(x).Position.Y)
            );
            
            _eventObservable = Observable.FromEventPattern<ModelBallChangedEventArgs>(this, "BallChanged");
        }

        public override void AddBallsAndStart(int ballsAmount)
        {
            for (int i = 0; i < ballsAmount; i++)
            {
                var ball = _logicApi.AddBall();
                
                BallInModel modelBall = new BallInModel(
                    ball.Position.X,
                    ball.Position.Y,
                    ball.Radius
                );
                
                _balls.Add(modelBall);
            }

            foreach (BallInModel ball in _balls)
            {
                BallChanged?.Invoke(this, new ModelBallChangedEventArgs() { Ball = ball });
            }

        }
        
        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return (_eventObservable ?? throw new InvalidOperationException("ModelBall subscribe failure")).Subscribe(
                x => observer.OnNext(x.EventArgs.Ball), 
                observer.OnError, 
                observer.OnCompleted);
        }
    }
}