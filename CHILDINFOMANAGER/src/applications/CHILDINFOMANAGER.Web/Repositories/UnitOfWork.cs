using CHILDINFOMANAGER.Web.Contexts;

namespace CHILDINFOMANAGER.Web.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Childrens = new ChildrenRepository(_context);
    }

    public IChildrenRepository Childrens { get; private set; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}