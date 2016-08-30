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
        [Compare("MotDePasse", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmerMotDePasse { get; set; }
    }
}
