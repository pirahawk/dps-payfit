using System.ComponentModel.DataAnnotations;

namespace DpsPayfit
{
    /// <summary>
    /// Represents a Dps Generate-Request Message
    /// </summary>
    public class DpsGenerateRequestMessage
    {
        [Required(ErrorMessage = "The field PxPayUserId cannot be null or empty")]
        public string PxPayUserId { get; set; }

        [Required(ErrorMessage = "The field PxPayKey cannot be null or empty")]
        public string PxPayKey { get; set; }
    }
}
