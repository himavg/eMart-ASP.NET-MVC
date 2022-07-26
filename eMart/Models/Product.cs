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
    public class Product:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProductCategory ProductCategory { get; set; }

        //Relationship
        public int ProductOwnerId { get; set; }
        [ForeignKey("ProductOwnerId")]
        public ProductOwner ProductOwner { get; set; }



    }
}
