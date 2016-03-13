namespace DpsPayfit.Client.Test
{
    public class RequestMessageFixture
    {
        public RequestMessageFixture()
        {
            Uri = "http://test.com";
            ValidFlag = 1;
        }
        public RequestMessage Build() {
            return new RequestMessage {
                Uri = Uri,
                ValidFlag = ValidFlag
            };
        }

        public string Uri { get; set; }
        public int ValidFlag { get; set; }
    }
}
