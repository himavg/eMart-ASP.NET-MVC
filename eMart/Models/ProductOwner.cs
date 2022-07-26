using eMart.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Models
{
    public class ProductOwner:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(50,MinimumLength = 3 , ErrorMessage ="Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Logo is required")]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        //Relationship
        public List<Product> Products { get; set; }
    }
}
