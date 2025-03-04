namespace TechVault.API.Helper
{
    public class ResponseApi
    {   
        public ResponseApi(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message??GetMessageFromStatusCode(StatusCode);
           
        }

        private string? GetMessageFromStatusCode(int statusCode) => statusCode switch
        {
            200 => "Done",
            400 => "Bad Request",
            401 => "Un Authorized",
            500 => "Server Error",
            _ => null,
        };
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
