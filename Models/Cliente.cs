using System.ComponentModel.DataAnnotations;

namespace SistemaClientes.Models
{
    public class Cliente
    {
        [Key]
        public int ID_Cliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string Endereco { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(14)]
        public string CPF { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
