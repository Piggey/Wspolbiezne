using System;

namespace Prezentacja.Model
{
    public abstract class ModelApi : IObservable<IBall>
    {
        public static ModelApi CreateModelApi()
        {
            return new ModelBall();
        }

        public abstract void AddBallsAndStart(int ballsAmount);

        #region IObservable
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        #endregion IObservable
    }
}