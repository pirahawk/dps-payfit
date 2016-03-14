using System.Collections.Generic;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class RequestMessageTest
    {
        [Theory, MemberData("RequestMessageTestData")]
        public void CorrectlyFlagsValidity(RequestMessage message, bool expectValid) {
            Assert.Equal(expectValid, message.IsValid);
        }

        public static IEnumerable<object[]> RequestMessageTestData {
            get
            {
                yield return new object[] { new RequestMessageFixture().Build(), true };
                yield return new object[] { new RequestMessageFixture { ValidFlag = 0 }.Build(), false };
                yield return new object[] { new RequestMessageFixture { ResponseCode = "01" }.Build(), false };
                yield return new object[] { new RequestMessageFixture { ResponseText = "some text" }.Build(), false };
            }
        }
    }
}
