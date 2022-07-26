using eMart.Data;
using eMart.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Models
{
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product Image URL")]
        [Required(ErrorMessage = "Product Image URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Sale start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Sale end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory ProductCategory { get; set; }

        //Relationships
        [Display(Name = "Select Owner")]
        [Required]
        public int OwnerId { get; set; }



    }
}
