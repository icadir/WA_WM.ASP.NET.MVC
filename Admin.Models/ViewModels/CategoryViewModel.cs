using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Kategori Adı 3 ile 100 karakter arasında olabilir", MinimumLength = 3)]
        [DisplayName("Kategori Adı")]
        [Required]
        public string CategoryName { get; set; }
        [Range(0, 99)]
        [DisplayName("KDV Oranı")]
        public decimal TaxRate { get; set; }
        [DisplayName("Üst Kategori")]
        public int? SupCategoryId { get; set; }
        public string CategoryPath { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
