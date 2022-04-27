#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vending_Machines_Managment.Models;

namespace Vending_Machines_Managment.Controllers
{
    public class VendingMachinesController : Controller
    {
        private readonly VendingMachineManagmentContext _context;

        public VendingMachinesController(VendingMachineManagmentContext context)
        {
            _context = context;
        }

        // GET: VendingMachines
        public async Task<IActionResult> Index()
        {
            var vendingMachineManagmentContext = _context.VendingMachines.Include(v => v.Type);
            return View(await vendingMachineManagmentContext.ToListAsync());
        }

        // GET: VendingMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendingMachine = await _context.VendingMachines
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendingMachine == null)
            {
                return NotFound();
            }

            return View(vendingMachine);
        }

        // GET: VendingMachines/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.VendingMachineTypes, "TypeId", "TypeId");
            return View();
        }

        // POST: VendingMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,InstalledOn,TypeId")] VendingMachine vendingMachine)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vendingMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.VendingMachineTypes, "TypeId", "TypeId", vendingMachine.TypeId);
            return View(vendingMachine);
        }

        // GET: VendingMachines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendingMachine = await _context.VendingMachines.FindAsync(id);
            if (vendingMachine == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.VendingMachineTypes, "TypeId", "TypeId", vendingMachine.TypeId);
            return View(vendingMachine);
        }

        // POST: VendingMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,InstalledOn,TypeId")] VendingMachine vendingMachine)
        {
            if (id != vendingMachine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendingMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendingMachineExists(vendingMachine.Id))
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
            ViewData["TypeId"] = new SelectList(_context.VendingMachineTypes, "TypeId", "TypeId", vendingMachine.TypeId);
            return View(vendingMachine);
        }

        // GET: VendingMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendingMachine = await _context.VendingMachines
                .Include(v => v.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendingMachine == null)
            {
                return NotFound();
            }

            return View(vendingMachine);
        }

        // POST: VendingMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendingMachine = await _context.VendingMachines.FindAsync(id);
            _context.VendingMachines.Remove(vendingMachine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendingMachineExists(int id)
        {
            return _context.VendingMachines.Any(e => e.Id == id);
        }
    }
}
