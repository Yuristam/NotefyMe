using Microsoft.AspNetCore.Mvc;
using NotefyMe.Application.Interfaces;
using NotefyMe.Domain.Entities;
using NotefyMe.WebApp.ViewModels;

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

        public async Task<IActionResult> Details(int id)
        {
            var note = await _notesRepository.GetNoteByIdAsync(id);
            return View(note);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteViewModel createNoteViewModel)
        {
            if(ModelState.IsValid)
            {
                var note = new Note
                {
                    Id = createNoteViewModel.Id,
                    Title = createNoteViewModel.Title,
                    Description = createNoteViewModel.Description,
                    DateCreated = DateTime.Now,
                    DateUpdated = null,
                    NoteCategory = createNoteViewModel.NoteCategory,
                    NoteColor = createNoteViewModel.NoteColor
                };

                _notesRepository.Add(note);
                return RedirectToAction("Index");
            }
            return View(createNoteViewModel);
        }
    }
}
