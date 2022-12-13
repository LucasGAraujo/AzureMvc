namespace AzureMvc.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string ImagemEstado { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

    }
}
