using System;
using System.Threading.Tasks;

namespace Spark.Core.Extension
{
    public static class TaskExtensions
    {
        public static void OnException<T>(this Task<T> task, Action<AggregateException> action)
        {
            task.ContinueWith(x => { action(x.Exception); }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static void OnException(this Task task, Action<AggregateException> action)
        {
            task.ContinueWith(x => { action(x.Exception); }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static void Sync(this Task task)
        {
            task.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static T Sync<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}