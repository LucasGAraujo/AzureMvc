using System.ComponentModel.DataAnnotations;

namespace AzureMvc.Models
{
    public class Amigos
    {
        [Key]
        public int AmigoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string ImagemAmigo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Aniversario { get; set; }
        public List<Amigos> Amigo { get; set; }
        public string PaisOrigem { get; set; }
        public string EstadoOrigem { get; set; }
        
    }
}
