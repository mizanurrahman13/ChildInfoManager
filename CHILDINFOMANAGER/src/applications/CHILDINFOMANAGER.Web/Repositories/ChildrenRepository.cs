using CHILDINFOMANAGER.Web.Contexts;
using CHILDINFOMANAGER.Web.Entities;

namespace CHILDINFOMANAGER.Web.Repositories;

public class ChildrenRepository : Repository<Children>, IChildrenRepository
{
    public ChildrenRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add any note-specific methods here if needed
}