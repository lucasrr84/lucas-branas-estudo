using System;

namespace ride.src.infra.mediator;

public class Mediator
{
    private readonly List<Handler> handlers;

    private class Handler
    {
        public string Event { get; }
        public Func<object, Task> Callback { get; }

        public Handler(string eventName, Func<object, Task> callback)
        {
            Event = eventName;
            Callback = callback;
        }
    }


    public Mediator()
    {
        handlers = new List<Handler>();
    }

    public void register(string eventName, Func<object, Task> callback)
    {
        handlers.Add(new Handler(eventName, callback));
    }

    public async Task notify(string eventName, object data)
    {
        foreach (var handler in handlers)
        {
            if (handler.Event == eventName)
            {
                await handler.Callback(data);
            }
        }
    }
}
