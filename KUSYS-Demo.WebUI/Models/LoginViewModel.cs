using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KUSYS_Demo.WebUI.Models
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girininiz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi girininiz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}

