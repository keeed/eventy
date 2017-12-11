using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eventy.Data;
using eventy.Models;
using eventy.Models.FamilyViewModels;

namespace eventy.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
        private readonly EventyDbContext _context;

        public FamilyController(EventyDbContext context)
        {
            _context = context;
        }

        // GET: Family
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page,
            string searchStringOldId)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParam"] = sortOrder == "Address" ? "date_desc" : "Address";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterOldId"] = searchStringOldId;

            var families = from f in _context.Families 
                           select f;

            if (!string.IsNullOrEmpty(searchString))
            {
                families = families.Where( f =>
                    f.Name.ToUpper().Contains(searchString.ToUpper())
                );
            }
            else if (!string.IsNullOrEmpty(searchStringOldId))
            {
                families = families.Where( f =>
                    f.OldFamilyNumber.ToUpper().Contains(searchStringOldId.ToUpper())
                );
            }

            switch(sortOrder)
            {
                case "name_desc": 
                    families = families.OrderByDescending(f => f.Name);
                    break;
                case "Address":
                    families = families.OrderBy(f => f.Address);
                    break;
                case "address_desc":
                    families = families.OrderByDescending(f => f.Address);
                    break;
                default:
                    families = families.OrderBy(f => f.Name);
                    break;
            }

            int pageSize = 15;

            return View(await 
                PaginatedList<Family>.CreateAsync(families.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Family/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families
                .SingleOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: Family/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Family/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFamilyViewModel familyViewModel)
        {
            var family = new Family();

            if (ModelState.IsValid)
            {
                family.Name = familyViewModel.Name;
                family.Address = familyViewModel.Address;

                _context.Add(family);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(family);
        }

        // GET: Family/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families.SingleOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: Family/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Family family)
        {
            if (id != family.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    family = _context.Families.Find(family.Id);
                    _context.Update(family);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.Id))
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
            return View(family);
        }

        // GET: Family/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Families
                .SingleOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Family/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.Families.SingleOrDefaultAsync(m => m.Id == id);
            _context.Families.Remove(family);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyExists(long id)
        {
            return _context.Families.Any(e => e.Id == id);
        }
    }
}
