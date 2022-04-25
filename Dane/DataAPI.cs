namespace Dane
{
    public abstract class DataAPI
    {
        public static DataAPI CreateData()
        {
            return new Data();
        }

        internal class Data : DataAPI
        {
            public Data()
            { }
        }
    }
}