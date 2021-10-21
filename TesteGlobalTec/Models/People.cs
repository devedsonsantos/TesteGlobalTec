using System;
using System.ComponentModel.DataAnnotations;

namespace TesteGlobalTec.Models
{
    public class People
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "Este campo deve conter entre 3 a 80 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 80 caracteres!")]
        public string Name { get; set; }

        [Required]
        public string Cpf { get; set; }

        [MaxLength(2, ErrorMessage = "Este campo deve conter 2 caracteres!")]
        [MinLength(2, ErrorMessage = "Este campo deve conter 2 caracteres!")]
        public string Uf { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}