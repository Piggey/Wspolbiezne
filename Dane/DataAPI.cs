namespace Dane
{
    public abstract class DataApi
    {
        public static Ball CreateBall()
        {
            return new Ball();
        }

        public static int GetBoardWidth()
        {
            return 500;
        }

        public static int GetBoardHeight()
        {
            return 500;
        }
    }
}