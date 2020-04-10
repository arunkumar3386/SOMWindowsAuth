using System.ComponentModel.DataAnnotations;

namespace StarOfTheMonth.Models
{

    public class ConfigurationModel
    {
        public long SlNo { get; set; }
        public long ID { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public string SMTP { get; set; }
        public string PortNo { get; set; }
        public string FromUserID { get; set; }
        public string Password { get; set; }
    }
}
