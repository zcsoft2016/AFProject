using System.ComponentModel.DataAnnotations;

namespace ProjetOrion.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le pseudonyme")]
        [Display(Name = "Pseudonyme")]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "Veuillez saisir l'adresse email")]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un mot de passe")]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        //[Compare("MotDePasse", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmerMotDePasse { get; set; }

        public string Photo { get; set; }
    }

    public class UtilisateurViewModel
    {
        public Utilisateur Utilisateur { get; set; }
        public bool Authentifie { get; set; }
        public int Id { get; set; }

        public UtilisateurViewModel()
        {
        }

        public UtilisateurViewModel(Utilisateur user, int id)
        {
            Utilisateur = user;
            Id = id;
            Authentifie = true;
        }

        public UtilisateurViewModel(Utilisateur user, bool auth)
        {
            Utilisateur = user;
            Authentifie = auth;
            Id = user.Id;
        }

        public UtilisateurViewModel(bool auth)
        {
            Authentifie = auth;
        }

        public UtilisateurViewModel(Utilisateur user)
        {
            Utilisateur = user;
            if (user != null)
            {
                Id = user.Id;
                Authentifie = true;
            }
        }
    }

    public class LoginUtilisateur
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
    }
}
