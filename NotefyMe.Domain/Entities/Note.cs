using NotefyMe.Domain.Enums;
using NotefyMe.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotefyMe.Domain.Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public NoteCategory NoteCategory { get; set; }
        public NoteColor NoteColor { get; set; }
        
        [ForeignKey("WebUser")]
        public string? WebUserId { get; set; }
        public WebUser? WebUser { get; set; }
    }
}
