using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150), Required(ErrorMessage ="Title is not valid")]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Price is not valid")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
