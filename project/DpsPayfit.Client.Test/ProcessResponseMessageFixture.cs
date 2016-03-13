namespace DpsPayfit.Client.Test
{
    public class ProcessResponseMessageFixture
    {
        public string PxPayKey { get; set; }
        public string PxPayUserId { get; set; }
        public string Response { get; set; }

        public ProcessResponseMessageFixture()
        {
            PxPayUserId = "anId";
            PxPayKey = "aKey";
            Response = "0000840000185376f1519ff80a5ccd54";
        }

        public ProcessResponseMessage Build() {
            return new ProcessResponseMessage {
                PxPayKey = PxPayKey,
                PxPayUserId = PxPayUserId,
                Response = Response
            };
        }
    }
}
