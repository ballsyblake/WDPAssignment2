using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDPAssignment1.Data;
using WDPAssignment1.Models;

namespace WDPAssignment1.Controllers
{
    public class FeedbackFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeedbackForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeedbackForm.ToListAsync());
        }

        // GET: FeedbackForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackForm = await _context.FeedbackForm
                .SingleOrDefaultAsync(m => m.Id == id);
            if (feedbackForm == null)
            {
                return NotFound();
            }

            return View(feedbackForm);
        }

        // GET: FeedbackForms/Create
        [Authorize(Roles = "Manager, User")]
        public IActionResult Create()
        {
            FeedbackForm fbf = new FeedbackForm();
            fbf.Date = DateTime.Now;
            fbf.UserName = User.Identity.Name;
            return View(fbf);
        }

        // POST: FeedbackForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,UserName,Heading,Technology,Rating,Feedback,Agree,Disagree")] FeedbackForm feedbackForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedbackForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedbackForm);
        }

        // GET: FeedbackForms/Edit/5
        [Authorize(Roles = "Manager, User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackForm = await _context.FeedbackForm.SingleOrDefaultAsync(m => m.Id == id);
            if (feedbackForm == null)
            {
                return NotFound();
            }
            return View(feedbackForm);
        }

        // POST: FeedbackForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,UserName,Heading,Technology,Rating,Feedback,Agree,Disagree")] FeedbackForm feedbackForm)
        {
            if (id != feedbackForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedbackForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackFormExists(feedbackForm.Id))
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
            return View(feedbackForm);
        }

        // GET: FeedbackForms/Delete/5
        [Authorize(Roles = "Manager, User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackForm = await _context.FeedbackForm
                .SingleOrDefaultAsync(m => m.Id == id);
            if (feedbackForm == null)
            {
                return NotFound();
            }

            return View(feedbackForm);
        }

        // POST: FeedbackForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedbackForm = await _context.FeedbackForm.SingleOrDefaultAsync(m => m.Id == id);
            _context.FeedbackForm.Remove(feedbackForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackFormExists(int id)
        {
            return _context.FeedbackForm.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Manager, User")]
        public async Task<IActionResult> IncreaseAgree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackForm = await _context.FeedbackForm.SingleOrDefaultAsync(m => m.Id == id);
            if (feedbackForm == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int total = feedbackForm.Agree + 1;
                    feedbackForm.Agree = total;

                   

                    _context.Update(feedbackForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackFormExists(feedbackForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager, User")]
        public async Task<IActionResult> IncreaseDisagree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedbackForm = await _context.FeedbackForm.SingleOrDefaultAsync(m => m.Id == id);
            if (feedbackForm == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int total = feedbackForm.Disagree + 1;
                    feedbackForm.Disagree = total;
              
                    _context.Update(feedbackForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackFormExists(feedbackForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
