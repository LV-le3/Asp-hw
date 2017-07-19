using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeApp.Models;

namespace HomeworkSnip.Controllers
{
    public class HomeworkSnipController : Controller
    {
        private readonly MyDbContext _context;

        public HomeworkSnipController(MyDbContext context)
        {
            _context = context;    
        }

    
        public async Task<IActionResult> Index(string idSearch)
        {
            return View(await _context.HomeworkSnip.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hwSnip = await _context.HomeworkSnip.SingleOrDefaultAsync(m => m.ID == id);
            if (hwSnip == null)
            {
                return NotFound();
            }

            return View(hwSnip);
        }

        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Origin,Roast,Brand")] hwSnip coffee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hwSnip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hwSnip);
        }

    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hwSnip = await _context.hwSnip.SingleOrDefaultAsync(m => m.ID == id);
            if (hwSnip== null)
            {
                return NotFound();
            }
            return View(hwSnip);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Origin,Roast,Brand")] Coffee coffee)
        {
            if (id != coffee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coffee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoffeeExists(coffee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(coffee);
        }

  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hwSnip = await _context.Coffees.SingleOrDefaultAsync(m => m.ID == id);
            if (hwSnip == null)
            {
                return NotFound();
            }

            return View(hwSnip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hwSnip = await _context.hwSnip.SingleOrDefaultAsync(m => m.ID == id);
            _context.hwSnip.Remove(hwSnip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool hwSnipExists(int id)
        {
            return _context.hwSnip.Any(e => e.ID == id);
        }
    }
}
