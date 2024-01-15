using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotefyMe.Application.Interfaces;
using NotefyMe.Domain.Entities;
using NotefyMe.Infrastructure.Extensions;
using NotefyMe.WebApp.ViewModels;
using System.Security.Claims;

namespace NotefyMe.WebApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesRepository _notesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotesController(INotesRepository notesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _notesRepository = notesRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notes = await _notesRepository.GetAllNotesByUserIdAsync(userId);
            return View(notes);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var note = await _notesRepository.GetNoteByIdAsync(id);
            return View(note);
        }

        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createNoteViewModel = new CreateNoteViewModel { WebUserId = currentUserId };

            return View(createNoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    NoteColor = createNoteViewModel.NoteColor,
                    WebUserId = createNoteViewModel.WebUserId
                };

                _notesRepository.Add(note);
                return RedirectToAction("Index");
            }
            return View(createNoteViewModel);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _notesRepository.GetNoteByIdAsync(id);
            if (note == null) return View("Error");
            
            var editNoteViewModel = new EditNoteViewModel
            {
                Title = note.Title,
                Description = note.Description,
                DateCreated = note.DateCreated,
                NoteCategory = note.NoteCategory,
                NoteColor = note.NoteColor
            };

            return View(editNoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditNoteViewModel editNoteViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit note");
                return View("Edit", editNoteViewModel);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var note = new Note
                    {
                        Id = id,
                        Title = editNoteViewModel.Title,
                        Description = editNoteViewModel.Description,
                        DateCreated = editNoteViewModel.DateCreated,
                        DateUpdated = DateTime.Now,
                        NoteCategory = editNoteViewModel.NoteCategory,
                        NoteColor = editNoteViewModel.NoteColor
                    };

                    _notesRepository.Update(note);

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_notesRepository.NoteExists(editNoteViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
                }
            }

            return View(editNoteViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var noteDetails = await _notesRepository.GetNoteByIdAsync(id);

            if (noteDetails == null)
            {
                return View("Error");
            }

            return View(noteDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noteDetails = await _notesRepository.GetNoteByIdAsync(id);
           
            if (noteDetails == null)
            {
                return View("Error");
            }

            _notesRepository.Delete(noteDetails);

            return RedirectToAction("Index");
        }
    }
}
