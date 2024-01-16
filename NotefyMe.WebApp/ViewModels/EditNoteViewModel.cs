using NotefyMe.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NotefyMe.WebApp.ViewModels
{
    public class EditNoteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(120, ErrorMessage = "Title can't be more than 120 characters")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public NoteCategory NoteCategory { get; set; }
        public NoteColor NoteColor{ get; set; }

        public string WebUserId { get; set; }
    }
}
