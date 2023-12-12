using System.Collections.Generic;
namespace CodeComm
{
    public class ApiResponse<T>
    {
        public bool status { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public T data { get; set; }
    }




}
