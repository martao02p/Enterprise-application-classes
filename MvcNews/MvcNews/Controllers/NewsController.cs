using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcNews.Data;
using MvcNews.Models;

namespace MvcNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsDbContext _context;

        public NewsController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsItem = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsItem == null)
            {
                return NotFound();
            }

            return View(newsItem);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,Text,RowVersion")] NewsItem newsItem)
        {
            if (ModelState.IsValid)
            {
                newsItem.RowVersion = 1;

                _context.Add(newsItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsItem);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsItem = await _context.News.FindAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }
            return View(newsItem);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStamp,Text,RowVersion")] NewsItem newsItem)
        {
            if (id != newsItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(newsItem).Property("RowVersion").OriginalValue = newsItem.RowVersion;
                    newsItem.RowVersion++;

                    _context.Update(newsItem);
                    await _context.SaveChangesAsync();
                }
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!NewsItemExists(newsItem.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                catch (DbUpdateConcurrencyException e)
                {
                    var entry = e.Entries.Single();
                    var databaseEntry = entry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("", "Rekord został usunięty przez innego użytkownika.");
                    }
                    else
                    {
                        var databaseEntity = (NewsItem)databaseEntry.ToObject();
                        ModelState.AddModelError("", "Dane zostały zmienione przez innego użytkownika. Spróbuj ponownie.");

                        // aktualne wartości z bazy
                        ModelState.AddModelError("TimeStamp", "Aktualna wartość: " + ((DateTime)databaseEntity.TimeStamp).ToString("g"));
                        ModelState.AddModelError("Text", "Aktualna wartość: " + databaseEntity.Text);

                        newsItem.RowVersion = databaseEntity.RowVersion;
                        ModelState.Remove("RowVersion");
                    }

                    return View(newsItem);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(newsItem);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsItem = await _context.News.FirstOrDefaultAsync(m => m.Id == id);
            if (newsItem == null)
            {
                ViewBag.Message = "Ten rekord został już usunięty lub nie istnieje.";
                return View("DeleteNotFound");
            }

            return View(newsItem);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, NewsItem newsItem)
        {
            //var newsItem = await _context.News.FindAsync(id);
            //if (newsItem != null)
            //{
            //    _context.News.Remove(newsItem);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            try
            {
                _context.News.Remove(newsItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!NewsItemExists(newsItem.Id))
                {
                    ModelState.AddModelError("", "Rekord został już usunięty przez innego użytkownika.");
                    return RedirectToAction(nameof(Index));
                }

                var entry = e.Entries.Single();
                var databaseEntry = entry.GetDatabaseValues();
                var databaseEntity = (NewsItem)databaseEntry.ToObject();

                ModelState.AddModelError("", "Rekord został zmodyfikowany przez innego użytkownika.");
                ModelState.Remove("RowVersion");

                return View(databaseEntity);
            }


        }

        private bool NewsItemExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
