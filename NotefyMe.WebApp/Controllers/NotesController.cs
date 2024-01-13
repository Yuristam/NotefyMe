using Microsoft.AspNetCore.Mvc;
using NotefyMe.Application.Interfaces;

namespace NotefyMe.WebApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _notesRepository.GetAllNotesAsync();
            return View(notes);
        }
    }
}
