using Microsoft.EntityFrameworkCore;
using NotefyMe.Application.Interfaces;
using NotefyMe.Domain.Entities;
using NotefyMe.Infrastructure.Data;

namespace NotefyMe.Infrastructure.Repositories
{
    public class NoteRepository : INotesRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Note note)
        {
            _context.Add(note);
            return Save();
        }

        public bool Delete(Note note)
        {
            _context.Remove(note);
            return Save();
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public Task<Note> GetNoteByIdAsync(int id)
        {
            return _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Note note)
        {
            _context.Update(note);
            return Save();
        }

        public bool NoteExists(int id)
        {
            return (_context.Notes?.Any(n => n.Id == id)).GetValueOrDefault();
        }

    }
}
