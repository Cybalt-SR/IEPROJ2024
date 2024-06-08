namespace Assets.Scripts.Library
{
    public interface IConsistentDataHolder<TData> where TData : class
    {
        public static TData Data { get; set; }
    }
}