using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace DpsPayfit.Test
{

    public class DataAnnotationsValidatorTest
    {
        [Theory, MemberData("DpsGenerateRequestMessageData")]
        public void ValidatesTransactionRequestMessageCorrectly(DpsGenerateRequestMessage message)
        {
            
        }

        public static IEnumerable<object[]> DpsGenerateRequestMessageData
        {
            get
            {
                yield return new object[] {};
            }
        }

        public class TestValidType
        {
            
        }
    }
}