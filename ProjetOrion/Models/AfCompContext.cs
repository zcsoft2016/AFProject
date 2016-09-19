using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetOrion.Models
{
    public class AfCompContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
