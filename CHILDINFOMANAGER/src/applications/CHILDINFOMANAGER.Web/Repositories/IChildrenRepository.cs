using CHILDINFOMANAGER.Web.Entities;

namespace CHILDINFOMANAGER.Web.Repositories;

public interface IChildrenRepository : IRepository<Children>
{
    // Add any note-specific methods here if needed
    Task<IEnumerable<Children>> GetAllByStoredProcedureAsync();
    Task AddByStoredProcedureAsync(Children entity);
}
