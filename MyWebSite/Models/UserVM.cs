using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyWebSite.DAL.MyEntities;

namespace MyWebSite.Models
{
    public class UserVM
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Lütfen isim alanını boş geçmeyiniz.")]
        [MaxLength(20,ErrorMessage = "İsim alanı maksimum 20 karakter olabilir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyisim alanını boş geçmeyiniz.")]
        [MaxLength(20, ErrorMessage = "Soyisim alanı maksimum 20 karakter olabilir.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen mail alanını boş geçmeyiniz.")]
        [MaxLength(50, ErrorMessage = "mail alanı maksimum 50 karakter olabilir.")]
        [EmailAddress(ErrorMessage = "Lütfen mail formatında giriş yapınız.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Lütfen Şifre alanını boş geçmeyelim.")]
        public string Password { get; set; }

        public bool Gender { get; set; }
    }
}