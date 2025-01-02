using CHILDINFOMANAGER.Web.Contexts;
using CHILDINFOMANAGER.Web.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CHILDINFOMANAGER.Web.Repositories;

public class ChildrenRepository : Repository<Children>, IChildrenRepository
{
    public ChildrenRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add any note-specific methods here if needed
    public async Task<IEnumerable<Children>> GetAllByStoredProcedureAsync()
    {
        return await _context.Set<Children>().FromSqlRaw("EXEC GetAllChildren").ToListAsync();
    }

    public async Task AddByStoredProcedureAsync(Children entity)
    {
        var commandText = "EXEC SaveChildren @Id, @FirstName, @LastName, @DateOfBirth, @PhoneNumber, @HomeAddress";
        var parameters = new[]
        {
            new SqlParameter("@Id", entity.Id),
            new SqlParameter("@FirstName", entity.FirstName),
            new SqlParameter("@LastName", entity.LastName),
            new SqlParameter("@DateOfBirth", entity.DateOfBirth),
            new SqlParameter("@PhoneNumber", entity.PhoneNumber),
            new SqlParameter("@HomeAddress", entity.HomeAddress)
        };

        await _context.Database.ExecuteSqlRawAsync(commandText, parameters);
    }

}