using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteModel : PageModel
{
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteModel(IRepo<AddressBookEntry> repo) {
        _repo = repo;
    }

    [BindProperty]
    public Guid Id { get; set; }
    public AddressBookEntry Entry { get; set; }

    public void OnGet(Guid id)
    {
        var specification = new EntryByIdSpecification(id);
        Entry = _repo.Find(specification).FirstOrDefault();
        Id = id;
    }

    public ActionResult OnPost(Guid id)
    {
        var specification = new EntryByIdSpecification(id);
        var bookEntry = _repo.Find(specification).FirstOrDefault();
        if (bookEntry != null) { 
            _repo.Remove(bookEntry);
        }
        return RedirectToPage("Index");
    }
}
