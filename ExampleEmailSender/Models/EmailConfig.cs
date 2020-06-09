using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleEmailSender.Models
{
    public class EmailConfig

    {  
        [Required]
        public string To { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage ="Can't be more than 10 letters")]
        public string  Subject { get; set; }
       [Required]
        public string Body { get; set; }
    }
}
