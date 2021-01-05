namespace RMQ
{
    /// <summary>
    /// 请求的对象
    /// </summary>
    public class RequestModel<T>
    {
        public RequestType Type { get; set; }
        public T Data { get; set; }
        public bool Exit { get; set; } = false;
    }
    /// <summary>
    /// 请求类型
    /// </summary>
    public enum RequestType
    {
        order, wl
    }
}
