using eMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Owners = new List<ProductOwner>();
        }

        public List<ProductOwner> Owners { get; set; }
    }
}
