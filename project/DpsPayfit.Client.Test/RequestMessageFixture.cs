namespace DpsPayfit.Client.Test
{
    public class RequestMessageFixture
    {
        public RequestMessageFixture()
        {
            Uri = "http://test.com";
            ValidFlag = 1;
            ResponseCode = string.Empty;
            ResponseText = string.Empty;
        }
        public RequestMessage Build() {
            return new RequestMessage {
                Uri = Uri,
                ValidFlag = ValidFlag,
                ResponseCode = ResponseCode,
                ResponseText = ResponseText
            };
        }

        public string Uri { get; set; }
        public int ValidFlag { get; set; }
        public string ResponseCode { get; private set; }
        public string ResponseText { get; private set; }
    }
}
