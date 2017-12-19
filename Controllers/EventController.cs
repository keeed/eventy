using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eventy.Data;
using eventy.Models;
using eventy.Models.EventViewModels;
using Microsoft.AspNetCore.Authorization;

namespace eventy.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly EventyDbContext _context;

        public EventController(EventyDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            var eventDetailsViewModel = new EventDetailsViewModel();
            eventDetailsViewModel.Event = @event;

            var families = _context.EventsFamilies.Join(
                _context.Families,
                eventFamily => eventFamily.Id,
                family => family.Id,
                (eventFamily, family) => new {
                    Family = family,
                    EventId = eventFamily.EventId
                }
            ).Where(eventFamily => eventFamily.EventId == @event.Id);

            var familyMembersDetails = _context.FamilyMembers.Join(
                families,
                familyMember => familyMember.FamilyId,
                eventFamily => eventFamily.Family.Id,
                (familyMember, eventFamily) => new FamilyMemberDetails()
                {
                    Family = eventFamily.Family,
                    FamilyMember = familyMember
                });

            eventDetailsViewModel.FamilyMembersDetails = await familyMembersDetails.ToListAsync();

            // Do this in memory instead of manually checking each item against EF.
            var eventFamilyMembers = await _context.EventsFamilyMembers
                .Where(efm => efm.EventId == @event.Id)
                .ToListAsync();
            
            eventDetailsViewModel.FamilyMembersDetails.ForEach(fmd => {
                if (eventFamilyMembers.Any(efm => efm.FamilyMemberId == fmd.FamilyMember.Id))
                {
                    fmd.IsAttending = true;
                }
                else
                {
                    fmd.IsAttending = false;
                }
            });

            return View(eventDetailsViewModel);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEventViewModel viewModel)
        {
            Event @event = new Event();
            if (ModelState.IsValid)
            {
                @event.EventName = viewModel.EventName;
                @event.EventDate = viewModel.EventDate.ToLocalTime().ToUniversalTime();
                @event.MaxNumberOfFamilies = viewModel.MaxNumberOfFamilies;
                
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EventName,EventDate,MaxNumberOfFamilies")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var eventToUpdate = _context.Events.Find(id);
                    eventToUpdate.EventName = @event.EventName;
                    eventToUpdate.EventDate = @event.EventDate;
                    eventToUpdate.MaxNumberOfFamilies = @event.MaxNumberOfFamilies;
                    
                    _context.Update(eventToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
