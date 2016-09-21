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
        bool EmailExiste(string email);
        Utilisateur Authentifier(string pseudoOuEmail, string motDePasse);
        List<Utilisateur> ObtenirTousLesUtilisateurs();
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string pseudo);
        void ModifierUtilisateur(Utilisateur user, string motDePasse, string photo);
    }
}
