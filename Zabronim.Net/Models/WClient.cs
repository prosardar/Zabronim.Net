using System.ComponentModel.DataAnnotations;

namespace Zabronim.Net.Models {
    public class WClient {
        [Required(ErrorMessage = "Введите адрес эл. почты")]
        [EmailAddressAttribute]
        public string Email { get; set; }
    }
}