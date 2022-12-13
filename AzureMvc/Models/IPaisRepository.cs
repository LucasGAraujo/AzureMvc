namespace AzureMvc.Models
{
    public interface IPaisRepository
    {
        IEnumerable<Pais> AllPaises { get; }
    }
}
