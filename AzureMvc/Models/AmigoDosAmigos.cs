namespace AzureMvc.Models
{
    public class AmigoDosAmigos
    {
        public int AmigoDoAmigoId { get; set; }
        public Amigos Amigo { get; set; }       
        public string AmigoDoAmigoNome { get; set; }    
        public string AmigoDoAmigoSobrenome { get; set; }     
        public string AmigoDoAmigoTelefone { get; set; }      
        public string AmigoDoAmigoEmail { get; set; }       
        public DateTime AniversarioAmigoDoAmigo { get; set; }
        public string PaisAmigoDoAmigo { get; set; }     
        public string EstadoAmigoDoAmigo { get; set; }        
        public string ImagemAmigoDoAmigo { get; set; }
    }
}
