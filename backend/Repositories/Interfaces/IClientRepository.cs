using ProcessHub.Entities;

namespace ProcessHub.Repositories.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<bool> ExistsByDocumentAsync(string documentNumber);
    }
}
