using ContactApp.Application.DTOs;
using ContactApp.Application.Interfaces;
using ContactApp.Domain.Entities;

namespace ContactApp.Application.Services
{
    /// <summary>
    /// Application service coordinating use-cases. Keeps controllers thin.
    /// Maps domain entities to DTOs for transport.
    /// </summary>
    public class ContactService
    {
        private readonly IRepository<Contact> _repository;

        public ContactService(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<List<ContactDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repository.GetAllAsync(ct);
            return list.Select(ToDto).ToList();
        }

        public async Task<ContactDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            return item == null ? null : ToDto(item);
        }

        public async Task<ContactDto> CreateAsync(ContactDto dto, CancellationToken ct = default)
        {
            var entity = new Contact
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address
            };
            var created = await _repository.AddAsync(entity, ct);
            return ToDto(created);
        }

        public async Task<ContactDto?> UpdateAsync(int id, ContactDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing == null) return null;
            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.Address = dto.Address;
            var updated = await _repository.UpdateAsync(existing, ct);
            return updated == null ? null : ToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }

        private static ContactDto ToDto(Contact c) =>
            new ContactDto(c.Id, c.FullName, c.Email, c.Phone, c.DateOfBirth, c.Address);
    }
}
