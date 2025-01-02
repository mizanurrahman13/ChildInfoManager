using Microsoft.AspNetCore.Mvc;

namespace CHILDINFOMANAGER.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MediatR;
using CHILDINFOMANAGER.Web.Commands;
using CHILDINFOMANAGER.Web.Queries;
using CHILDINFOMANAGER.Web.Repositories;

public class ChildrensController : Controller
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public ChildrensController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    // GET: Children
    public async Task<IActionResult> Index()
    {
        var children = await _mediator.Send(new GetAllChildrenQuery());
        return View(children);
    }

    // GET: ChildrensRaw
    public async Task<IActionResult> ChildrensRaw()
    {
        var children = await _mediator.Send(new GetAllChildrenByStoredProcedureQuery());
        return View("ChildrensRaw", children);
    }

    // GET: Children/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var children = await _mediator.Send(new GetChildrenByIdQuery { Id = id });
        if (children == null)
        {
            return NotFound();
        }

        return View(children);
    }

    // GET: Children/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Children/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth,PhoneNumber,HomeAddress")] CreateChildrenCommand command)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        return View(command);
    }

    // POST: Children/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateByStoredProcedure([Bind("FirstName,LastName,DateOfBirth,PhoneNumber,HomeAddress")] CreateChildrenByStoredProcedureCommand command)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        return View(command);
    }

    // GET: Children/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var children = await _mediator.Send(new GetChildrenByIdQuery { Id = id });
        if (children == null)
        {
            return NotFound();
        }
        return View(new UpdateChildrenCommand
        {
            Id = children.Id,
            FirstName = children.FirstName,
            LastName = children.LastName,
            DateOfBirth = children.DateOfBirth,
            PhoneNumber = children.PhoneNumber,
            HomeAddress = children.HomeAddress
        });
    }

    // POST: Children/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,DateOfBirth,PhoneNumber,HomeAddress")] UpdateChildrenCommand command)
    {
        if (id != command.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        return View(command);
    }

    // GET: Children/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var children = await _mediator.Send(new GetChildrenByIdQuery { Id = id });
        if (children == null)
        {
            return NotFound();
        }

        return View(children);
    }

    // POST: Children/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        //await _mediator.Send(new DeleteChildrenCommand { Id = id });
        var child = await _unitOfWork.Childrens.GetByIdAsync(id);

        _unitOfWork.Childrens.Remove(child);
        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

