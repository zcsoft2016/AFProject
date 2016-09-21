using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetOrion.Models
{
    public class Dal : IDal
    {
        private readonly AfCompContext _afCompContext;

        public Dal()
        {
            _afCompContext = new AfCompContext();
        }

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            _afCompContext.Utilisateurs.Add(utilisateur);
            _afCompContext.SaveChanges();
        }

        public bool PseudoExiste(string pseudo)
        {
            var utilisateur =
                _afCompContext.Utilisateurs.SingleOrDefault(
                    user => user.Pseudo.Equals(pseudo, StringComparison.OrdinalIgnoreCase));
            if (utilisateur != null)
                return true;
            return false;
        }

        public bool EmailExiste(string email)
        {
            var utilisateur =
                   _afCompContext.Utilisateurs.SingleOrDefault(
                       user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (utilisateur != null)
                return true;
            return false;
        }

        public Utilisateur Authentifier(string pseudoOuEmail, string motDePasse)
        {
            var utilisateur = _afCompContext.Utilisateurs.SingleOrDefault(user =>
                user.Pseudo.Equals(pseudoOuEmail) && user.MotDePasse.Equals(motDePasse));
            if (utilisateur != null)
                return utilisateur;
            utilisateur = _afCompContext.Utilisateurs.SingleOrDefault(user =>
                user.Email.Equals(pseudoOuEmail) && user.MotDePasse.Equals(motDePasse));
            return utilisateur;
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            return _afCompContext.Utilisateurs.ToList();
        }

        public void Dispose()
        {
            _afCompContext.Dispose();
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            var utilisateurs = _afCompContext.Utilisateurs.ToList();
            return utilisateurs.SingleOrDefault(user => user.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string pseudo)
        {
            var utilisateurs = _afCompContext.Utilisateurs.ToList();
            return utilisateurs.SingleOrDefault(user => user.Pseudo.Equals(pseudo));
        }

        public void ModifierUtilisateur(Utilisateur user, string motDePasse, string photo)
        {
            var utilisateur = ObtenirUtilisateur(user.Pseudo);
            if (utilisateur == null)
                return;
            if (!string.IsNullOrEmpty(motDePasse))
                utilisateur.MotDePasse = motDePasse;
            if (!string.IsNullOrEmpty(photo))
                utilisateur.Photo = photo;
            _afCompContext.SaveChanges();
        }
    }
}
