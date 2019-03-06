using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rabit.Models;

namespace Rabit.DAL
{
   public class MyContext:DbContext
    {
        public MyContext():base("name=MyCon")
        {
            this.InstanceDate = DateTime.Now;
        }

        public DateTime InstanceDate { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MailLog> MailLogs { get; set; }
    }
}
