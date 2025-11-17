namespace BAL.Dto
{
    public interface IBasicResponse<T>
    {
        bool Status { get; set; }
        T Data { get; set; }
        string Message { get; set; }

    }
    public class BasicResponse<T> : IBasicResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

}
