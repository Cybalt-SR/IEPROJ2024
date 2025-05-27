
using System.Collections.Generic;


public interface IMediator<T> where T : class
{
    public void Notify(string notification, T notifier);
}

