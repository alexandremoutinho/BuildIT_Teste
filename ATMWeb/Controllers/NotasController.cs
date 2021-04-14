using ATMWeb.Data;
using ATMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Controllers
{
    public class NotasController : Controller
    {
        private readonly AtmdbContext db;

        public NotasController(AtmdbContext context)
        {
            db = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await db.Notas.ToListAsync());
        }


        public async Task<IActionResult> NotasDisponiveis()
        {

            return View(await db.Notas.ToListAsync());
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotasId,Nota,Valor,Qtde,ImgNota")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                db.Add(notas);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await db.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            return View(notas);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotasId,Nota,Valor,Qtde,ImgNota")] Notas notas)
        {
            if (id != notas.NotasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(notas);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.NotasId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notas);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await db.Notas
                .FirstOrDefaultAsync(m => m.NotasId == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notas = await db.Notas.FindAsync(id);
            db.Notas.Remove(notas);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
            return db.Notas.Any(e => e.NotasId == id);
        }
    }
}
