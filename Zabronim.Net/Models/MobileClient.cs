using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zabronim.Net.Models {
    public class MobileClient {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Key]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set;}
    }
}