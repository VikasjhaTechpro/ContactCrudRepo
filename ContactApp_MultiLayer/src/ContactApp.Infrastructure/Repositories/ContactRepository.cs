using ContactApp.Application.Interfaces;
using ContactApp.Domain.Entities;
using ContactApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Infrastructure.Repositories
{
    /// <summary>
    /// Concrete repository implementation using EF Core.
    /// Infrastructure depends on Application and Domain abstractions.
    /// </summary>
    public class ContactRepository : IRepository<Contact>
    {
        private readonly AppDbContext _db;
        public ContactRepository(AppDbContext db) => _db = db;

        public async Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Contacts.AsNoTracking().OrderBy(x => x.FullName).ToListAsync(cancellationToken);
        }

        public async Task<Contact?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Contacts.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Contact> AddAsync(Contact entity, CancellationToken cancellationToken = default)
        {
            _db.Contacts.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Contact?> UpdateAsync(Contact entity, CancellationToken cancellationToken = default)
        {
            var existing = await _db.Contacts.FindAsync(new object[] { entity.Id }, cancellationToken);
            if (existing == null) return null;
            _db.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _db.Contacts.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null) return false;
            _db.Contacts.Remove(existing);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
