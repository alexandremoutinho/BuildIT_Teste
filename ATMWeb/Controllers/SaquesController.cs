using ATMWeb.Data;
using ATMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Controllers
{
    public class SaquesController : Controller
    {
        private readonly AtmdbContext db;
        

        public SaquesController(AtmdbContext context)
        {
            db = context;
        }

        // GET: Saques
        public async Task<IActionResult> Index()
        {
            return View(await db.Saques.ToListAsync());
        }

        // GET: Saques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saque = await db.Saques
                .FirstOrDefaultAsync(m => m.id == id);
            if (saque == null)
            {
                return NotFound();
            }

            return View(saque);
        }

        public IActionResult Op(int valor=0)
        {
            ViewData["Result"] = valor;
            return View();
        }
               

        // GET: Saques/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Valor")] Saque saque)
        {

            int cem = 0, ciq = 0, vin = 0, dez = 0, cin = 0, doi = 0;
            var notas = db.Notas.AsQueryable();

            foreach (var nt in notas)
            {
                if (nt.Valor == 100) { cem = nt.Qtde; }
                if (nt.Valor == 50) { ciq = nt.Qtde; }
                if (nt.Valor == 20) { vin = nt.Qtde; }
                if (nt.Valor == 10) { dez = nt.Qtde; }
                if (nt.Valor == 5) { cin = nt.Qtde; }
                if (nt.Valor == 2) { doi = nt.Qtde; }
                
            }
            int resto = (int)saque.Valor;
            int cedula = doi+cin+dez+vin+ciq+cem;
            
            

            if (ModelState.IsValid)
            {

                if (cem>0){ while (resto >= 100 && cem>0) { resto = resto - 100; cem -= 1; }  }
                if (ciq>0){ while (resto >= 50 && ciq>0) { resto = resto - 50; ciq -= 1; } }
                if (vin>0){ while (resto >= 20 && vin>0) { resto = resto - 20; vin -= 1; } }
                if (dez>0){ while (resto >= 10 && dez>0) { resto = resto - 10; dez -= 1; } }
                if (cin>0){ while (resto >= 5 && cin>0) { resto = resto - 5; cin -= 1; } }
                if (doi>0){ while (resto >= 2 && doi>0) { resto = resto - 2; doi -= 1; } }
                
                

                var resultnotas = cedula;

                if (resto>0)
                { var disponivel = saque.Valor - resto;
                    ViewBag.ValorSaque = $"Valor solicitado de {saque.Valor.ToString("C2")} é Maior que o Disponivel Saque";
                    ViewBag.Disponivel = $"Valor Disponível para Saque é de {disponivel.ToString("C2")}";
                }


                if (resto==0)

                {   db.Add(saque);
                    saque.DataSaque = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    await db.SaveChangesAsync();

                    foreach (var nt in notas)
                    {
                        if (nt.Valor == 100) { nt.Qtde = cem; db.SaveChanges(); }
                        if (nt.Valor == 50) { nt.Qtde = ciq; db.SaveChanges(); }
                        if (nt.Valor == 20) { nt.Qtde = vin; db.SaveChanges(); }
                        if (nt.Valor == 10) { nt.Qtde = dez; db.SaveChanges(); }
                        if (nt.Valor == 5) { nt.Qtde = cin; db.SaveChanges(); }
                        if (nt.Valor == 2) { nt.Qtde = doi; db.SaveChanges(); }

                    }

                    return RedirectToAction("Op",resultnotas);

                }
            }
            return View(saque);
        }
              

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saque = await db.Saques
                .FirstOrDefaultAsync(m => m.id == id);
            if (saque == null)
            {
                return NotFound();
            }

            return View(saque);
        }

        // POST: Saques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saque = await db.Saques.FindAsync(id);
            db.Saques.Remove(saque);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaqueExists(int id)
        {
            return db.Saques.Any(e => e.id == id);
        }


        
        

    }
}
