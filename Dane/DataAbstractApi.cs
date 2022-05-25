using System.Collections.ObjectModel;
using Dane;

namespace Dane
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
        public abstract void CreateBalls(int number);
        public abstract void StopBalls();
        public abstract Storage GetStorage();
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract int Width { get; }
        public abstract int Height { get; }
    }
    
    public class DataApi : DataAbstractApi
    {
        private Storage storage = new Storage();
        private int _width ;
        private int _height;
        public DataApi() { }
        public override void CreateBalls(int number)
        {
            storage = new Storage();
            storage.CreateBalls(number);
        }
        public override void StopBalls()
        {
            if (storage != null)
            {
                storage.StopBalls();
            }
        }
        public override Storage GetStorage()
        {
            return storage;
        }
        public override ObservableCollection<Ball> GetBalls()
        {
            return storage.Balls;
        }
        public override int Height
        {
            get => storage.Height;
        }
        public override int Width
        {
            get => storage.Width;
        }
    }
}