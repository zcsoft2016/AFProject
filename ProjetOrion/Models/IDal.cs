using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetOrion.Models
{
    public interface IDal : IDisposable
    {
        void AjouterUtilisateur(Utilisateur utilisateur);
        bool PseudoExiste(string pseudo);
        List<Utilisateur> ObtenirTousLesUtilisateurs();
    }
}
