﻿namespace AzureMvc.Models
{
    public class Pais
    {
        public int PaisId { get; set; }
        public string? Nome { get; set; }
        public string? ImagemPais { get; set; }
        
        public List<Estado>? Estados { get; set;}
    }
}
