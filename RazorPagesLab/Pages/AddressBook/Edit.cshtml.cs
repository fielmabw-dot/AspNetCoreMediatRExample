using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
		
		// Created a specification to get the entry using the provided ID, then used the repo to get the first matching entry.
		var specification = new EntryByIdSpecification(id);
		var bookEntry = _repo.Find(specification).FirstOrDefault();
		
		// Populated the request with the values from the entry to fill the form. 
		UpdateAddressRequest = new UpdateAddressRequest {
			Id = bookEntry.Id,
			Line1 = bookEntry.Line1,
			Line2 = bookEntry.Line2,
			City = bookEntry.City,
			State = bookEntry.State,
			PostalCode = bookEntry.PostalCode,
        };
	}

	public async Task<ActionResult> OnPost()
	{
        // Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
		
		// If statement used to check if the data is valid
		if (ModelState.IsValid)
        {
			// Send the new request to MediatR and ignore the return value. Then redirect back to the Index page when complete.
            _ = await _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }

		// Handles if the validation fails and then reloads the page so users can fix their errors.
        return Page();
    }
}