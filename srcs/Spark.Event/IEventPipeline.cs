using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NLog;

namespace Spark.Event
{
    public interface IEventPipeline
    {
        void Emit(IEvent e);
        void AddEventHandler(IEventHandler handler);
        void AddEventHandler<T>(Action<T> handler) where T : IEvent;
    }

    public sealed class EventPipeline : IEventPipeline
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<Type, List<IEventHandler>> handlers;

        public EventPipeline() => handlers = new ConcurrentDictionary<Type, List<IEventHandler>>();

        public void Emit(IEvent e)
        {
            List<IEventHandler> handlers = this.handlers.GetValueOrDefault(e.GetType());
            if (handlers == null)
            {
                Logger.Trace($"No event handler found for {e.GetType().Name}, skipping.");
                return;
            }

            foreach (IEventHandler handler in handlers)
            {
                try
                {
                    handler.Handle(e);
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                }
            }

            Logger.Trace($"Emitted event {e.GetType().Name} for {handlers.Count} handlers");
        }

        public void AddEventHandler(IEventHandler handler)
        {
            List<IEventHandler> handlers = this.handlers.GetValueOrDefault(handler.EventType);
            if (handlers == null)
            {
                handlers = new List<IEventHandler>();
                this.handlers[handler.EventType] = handlers;
            }

            handlers.Add(handler);
            Logger.Debug($"Registered event handler {handler.GetType().Name} for event {handler.EventType.Name}");
        }

        public void AddEventHandler<T>(Action<T> handler) where T : IEvent
        {
            AddEventHandler(new SimpleEventHandler<T>(handler));
        }
    }
}