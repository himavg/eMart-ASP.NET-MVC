using eMart.Data.Base;
using eMart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMart.Data.Services
{
    public class OwnersService : EntityBaseRepository<ProductOwner>, IOwnersService
    {
        

        public OwnersService(AppDbContext context) : base(context) { }
        
    }
}
