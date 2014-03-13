using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Zabronim.Net.Models {
    /// <summary>
    /// Web Client
    /// </summary>
    public class WClient {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }

        [Key]
        [EmailAddressAttribute]
        [Editable(true)]
        [Required(ErrorMessage = "Введите адрес эл. почты")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Пожалуйста проверьте адрес почты")]
        [Remote("IsEmail_Available", "Home", ErrorMessage = "Пользователь с таким email уже существует")]
        public string Email { get; set; }

        public bool IsConfim { get; set; }
    }
}