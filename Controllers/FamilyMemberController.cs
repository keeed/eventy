using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eventy.Data;
using eventy.Models;
using eventy.Models.FamilyMemberViewModels;
using eventy.Models.GenericViewModels;

namespace eventy.Controllers
{
    public class FamilyMemberController : Controller
    {
        private readonly EventyDbContext _context;

        public FamilyMemberController(EventyDbContext context)
        {
            _context = context;
        }

        // GET: FamilyMember
        public async Task<IActionResult> Index()
        {
            return View(await _context.FamilyMembers.ToListAsync());
        }

        // GET: FamilyMember/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // GET: FamilyMember/Create
        public IActionResult Create()
        {
            var createFamilyMemberViewModel = new CreateFamilyMemberViewModel();

            createFamilyMemberViewModel.Genders = new ListItemViewModel();
            createFamilyMemberViewModel.Genders.Add("Male");
            createFamilyMemberViewModel.Genders.Add("Female");

            createFamilyMemberViewModel.HeadOfFamily = new ListItemViewModel();
            createFamilyMemberViewModel.HeadOfFamily.Add("No");
            createFamilyMemberViewModel.HeadOfFamily.Add("Yes");
            createFamilyMemberViewModel.Birthday = DateTime.Now;

            return View(createFamilyMemberViewModel);
        }

        // POST: FamilyMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFamilyMemberViewModel createFamilyMemberViewModel)
        { 
            FamilyMember familyMember = new FamilyMember();
            if (ModelState.IsValid)
            {
                familyMember.FamilyId = createFamilyMemberViewModel.FamilyId;
                familyMember.FullName = createFamilyMemberViewModel.FullName;
                familyMember.Birthday = createFamilyMemberViewModel.Birthday;
                familyMember.Gender = createFamilyMemberViewModel.SelectedGender;
                familyMember.IsHeadOfFamily = 
                    createFamilyMemberViewModel.IsHeadOfFamily == "Yes" ? true : false;

                _context.Add(familyMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createFamilyMemberViewModel);
        }

        // GET: FamilyMember/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers.SingleOrDefaultAsync(m => m.Id == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            var editFamilyMemberViewModel = new EditFamilyMemberViewModel();
            editFamilyMemberViewModel.Id = familyMember.Id;
            editFamilyMemberViewModel.FamilyId = familyMember.FamilyId;
            editFamilyMemberViewModel.FullName = familyMember.FullName;
            editFamilyMemberViewModel.Birthday = familyMember.Birthday;
            editFamilyMemberViewModel.Genders = new ListItemViewModel();
            editFamilyMemberViewModel.Genders.Add("Male");
            editFamilyMemberViewModel.Genders.Add("Female");
            editFamilyMemberViewModel.HeadOfFamily = new ListItemViewModel();
            editFamilyMemberViewModel.HeadOfFamily.Add("No");
            editFamilyMemberViewModel.HeadOfFamily.Add("Yes");
            editFamilyMemberViewModel.SelectedGender = familyMember.Gender;
            editFamilyMemberViewModel.IsHeadOfFamily = familyMember.IsHeadOfFamily ? "Yes" : "No";

            return View(editFamilyMemberViewModel);
        }

        // POST: FamilyMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditFamilyMemberViewModel editFamilyMemberViewModel)
        {
            if (id != editFamilyMemberViewModel.Id ||
                !FamilyMemberExists(editFamilyMemberViewModel.Id))
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var familyMember = new FamilyMember();
                try
                {
                    familyMember = _context.FamilyMembers.Find(editFamilyMemberViewModel.Id);
                    familyMember.Id = editFamilyMemberViewModel.Id;
                    familyMember.FamilyId = editFamilyMemberViewModel.Id;
                    familyMember.FullName = editFamilyMemberViewModel.FullName;
                    familyMember.Birthday = editFamilyMemberViewModel.Birthday;
                    familyMember.Gender = editFamilyMemberViewModel.SelectedGender;
                    familyMember.IsHeadOfFamily = editFamilyMemberViewModel.IsHeadOfFamily == "Yes" ? true : false;

                    _context.Update(familyMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyMemberExists(familyMember.Id))
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
            return View(editFamilyMemberViewModel);
        }

        // GET: FamilyMember/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMembers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // POST: FamilyMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var familyMember = await _context.FamilyMembers.SingleOrDefaultAsync(m => m.Id == id);
            _context.FamilyMembers.Remove(familyMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyMemberExists(long id)
        {
            return _context.FamilyMembers.Any(e => e.Id == id);
        }
    }
}
