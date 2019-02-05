using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyWebSite.DAL.MyEntities;

namespace MyWebSite.DAL
{
    public class MyWebSiteContext : DbContext
    {
        public MyWebSiteContext()
        {

        }
        //Ders Notu  mesut öztürk hocamız derste DALdaki entitye dataannation yazıyor ama normalde yazılmaz neden.  cunku DAL ortak bir katmandır. sen entity de yazdıgın modelleri tekrar uı da yazıp dataannations verirsin. Yarın birgun bu entity mobiil tarafında dada kullanılacak. bu yzden DAL daki entitye e data Annation vermezsin.  Web UI da model yapıp burada dataannations vererek çalışırız. Bagımlı kalmamak için.
        public DbSet<User> User { get; set; }
    }
}
