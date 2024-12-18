using System.ComponentModel.DataAnnotations;

namespace BatchProcessing.Models
{
    public class BatchRequest
    {
        [Required(ErrorMessage = "The IpAddresses field is required.")]
        [MinLength(1, ErrorMessage = "You must provide at least one IP address.")]
        public string[] IpAddresses { get; set; }
    }
}
