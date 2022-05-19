using Dane;

namespace Logika
{
    public abstract class LogicApi : IObserver<int>, IObservable<int>
    {
        public static LogicApi CreateLogicApi()
        {
            return new Board();
        }
        
        /// <summary>
        /// Get count of active Balls 
        /// </summary>
        /// <returns>Task count</returns>
        public abstract int GetBallCount();
        
        /// <summary>
        /// Start new thread running a single Ball in loop
        /// </summary>
        /// <returns>created Ball object</returns>
        public abstract Ball AddBall();
        
        /// <summary>
        /// Kills most recent thread of a Ball in loop created
        /// </summary>
        public abstract void RemoveBall();

        /// <summary>
        /// get Ball with specific id
        /// </summary>
        /// <param name="id">id of the Ball</param>
        /// <returns>Ball with specified id</returns>
        public abstract Ball GetBall(int id);

        /// <summary>
        /// Get Board's Width value
        /// </summary>
        /// <returns>Width</returns>
        public abstract int GetBoardWidth();
        
        /// <summary>
        /// Get Board's Height value
        /// </summary>
        /// <returns>Height</returns>
        public abstract int GetBoardHeight();

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(int value);

        public abstract IDisposable Subscribe(IObserver<int> observer);
    }
}