using NotefyMe.Domain.Entities;

namespace NotefyMe.Application.Interfaces
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        bool Add(Note note);
        bool Update(Note note);
        bool Delete(Note note);
        bool Save();
        bool NoteExists(int id);
    }
}
