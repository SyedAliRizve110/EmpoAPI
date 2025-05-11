namespace Empo.BuildingBlocks.Application
{
    class ApiResponse
    {
        public int? Code { get; set; }
        public object Data { get; set; }
        public List<ErrorDetails> Errors { get; set; }

        public ApiResponse()
        {
                
        }
        public ApiResponse(int? code, object data, List<ErrorDetails> errors)
        {
            Code = code;
            Data = data;
            Errors = errors;
        }
        public class ErrorDetails
        {
            public int ErrorCode { get; set; }
            public string Message { get; set; }
        }
    }
}
