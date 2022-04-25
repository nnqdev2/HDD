using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HDD.Models
{
    public partial class EmailCode
    {
        public int EmailCodeId { get; set; }
        [Required]
        public int? VehicleId { get; set; }
        [Display(Name = "Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        ErrorMessage = "Must be a valid email")]
        [Required]
        public string? Email { get; set; }
        [DisplayName("Verification Code")]
        public string? Code { get; set; }
        public DateTime? SentDate { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? Comment { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
