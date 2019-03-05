using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevxOdata.Models
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext()
          : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}