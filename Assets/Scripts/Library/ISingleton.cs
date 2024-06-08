namespace Assets.Scripts.Library
{
    public interface ISingleton<T>
    {
        static T Instance { get; protected set; }
    }
}