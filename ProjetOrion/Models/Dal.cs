using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetOrion.Models
{
    public class Dal : IDal
    {
        private readonly BddContext bddContext;

        public Dal()
        {
            bddContext = new BddContext();
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return bddContext.Utilisateurs.ToList();
        }

        public void Dispose()
        {
            bddContext.Dispose();
        }

    }
}
