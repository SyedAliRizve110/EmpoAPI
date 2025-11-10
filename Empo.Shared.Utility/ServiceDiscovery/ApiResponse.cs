namespace Empo.Shared.Utility.Core;

public class ApiResponse
{
    public int? Code { get; set; }
    public object Data { get; set; }
    public List<ErrorDetail> Errors { get; set; }

    public ApiResponse()
    {

    }
    public ApiResponse(int? Code, object Data, List<ErrorDetail> Errors)
    {
        this.Code = Code;
        this.Data = Data;
        this.Errors = Errors;
    }

    public class ErrorDetail
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}