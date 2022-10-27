using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Models
{
    public class License
    {
        public int Id { get; set; }
        [DisplayName("License Key")]
        public string Key { get; set; }

        [DisplayName("Expire Date")]
        [DataType(DataType.Date)]
        public DateTime ExpireDate { get; set; }
        
        [DisplayName("Created At")]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public Machine Machine { get; set; }

        public bool IsActive()
        {
            return ExpireDate > DateTime.Now;
        }
    }
}
