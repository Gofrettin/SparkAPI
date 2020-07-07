using System.Net;

namespace Spark.Gameforge
{
    public readonly struct GameforgeResponse<T>
    {
        public T Content { get; }
        public HttpStatusCode Status { get; }

        public GameforgeResponse(T content, HttpStatusCode status)
        {
            Content = content;
            Status = status;
        }
    }
}