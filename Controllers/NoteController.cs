using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiary.DAL;
using MyDiary.Models;

namespace MyDiary.Controllers
{
    public class NoteController : Controller
    {
        private readonly MyDiaryDbContext _context;

        // This is a constructor which serves to connect with DataBase
        public NoteController(MyDiaryDbContext context)
        {
            _context = context;
        }

        // This method calls all the data with their categories
        public IActionResult Index()
        {
            var notes = _context.Notes.Include("Categories");
            return View(notes);
        }

        // GET method for creating an entity
        [HttpGet]
        public IActionResult Create()   
        {
            LoadCategories();
            return View();
        }

        // This is non-action method that does not any action, It is used for only getting categories list
        [NonAction]
        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
         
        // This is POST method to store an entity to the database
        [HttpPost]
        public IActionResult Create(Note model)
        {
            _context.Notes.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // This GET method is used to get a model via ID, in order to update it 
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                NotFound();
            }
            LoadCategories();
            var note = _context.Notes.Find(id);
            return View(note);
        }

        // This POST method for updating selecting entity
        [HttpPost]
        public IActionResult Edit(Note model) 
        {
            ModelState.Remove("Categories");
            if(ModelState.IsValid)
            {
                _context.Notes.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // This GET method calls an entity with it's ID
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadCategories();
            var note = _context.Notes.Find(id);
            return View(note);
        }

        // This POST method is used to delete an entity from DataBase
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Note model)
        {
            _context.Notes.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
