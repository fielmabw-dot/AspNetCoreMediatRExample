using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
    : IRequestHandler<UpdateAddressRequest, Unit>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        var specification = new EntryByIdSpecification(request.Id);
        var entry = _repo.Find(specification).FirstOrDefault();
        entry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);
        _repo.Update(entry);
        return await Task.FromResult(Unit.Value);
    }
}
