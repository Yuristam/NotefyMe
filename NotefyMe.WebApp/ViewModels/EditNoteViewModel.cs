using NotefyMe.Domain.Enums;

namespace NotefyMe.WebApp.ViewModels
{
    public class EditNoteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public NoteCategory NoteCategory { get; set; }
        public NoteColor NoteColor{ get; set; }
    }
}
