using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetOrion.Models
{
    public class Dal : IDal
    {
        private readonly DbContext _dbContext;

        public Dal()
        {
            _dbContext = new DbContext();
        }

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            _dbContext.Utilisateurs.Add(utilisateur);
            _dbContext.SaveChanges();
        }

        public bool PseudoExiste(string pseudo)
        {
            var utilisateur =
                _dbContext.Utilisateurs.SingleOrDefault(
                    user => user.Pseudo.Equals(pseudo, StringComparison.OrdinalIgnoreCase));
            if (utilisateur != null)
                return true;
            return false;
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return _dbContext.Utilisateurs.ToList();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
