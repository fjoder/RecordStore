using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordStore.Data;
using RecordStore.Models;

namespace RecordStore.Controllers
{
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TracksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index(int? page)
        {
            var applicationDbContext = _context.tracks.Include(t => t.album).Include(t => t.genre).Include(t => t.media_type);
            int pageSize = 30;
            return View(await PaginatedList<tracks>.CreateAsync(applicationDbContext, page ?? 1, pageSize));
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracks = await _context.tracks
                .Include(t => t.album)
                .Include(t => t.genre)
                .Include(t => t.media_type)
                .FirstOrDefaultAsync(m => m.TrackId == id);
            if (tracks == null)
            {
                return NotFound();
            }

            return View(tracks);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.albums, "AlbumId", "Title");
            ViewData["GenreId"] = new SelectList(_context.genres, "GenreId", "GenreId");
            ViewData["MediaTypeId"] = new SelectList(_context.media_types, "MediaTypeId", "MediaTypeId");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] tracks tracks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tracks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.albums, "AlbumId", "Title", tracks.AlbumId);
            ViewData["GenreId"] = new SelectList(_context.genres, "GenreId", "GenreId", tracks.GenreId);
            ViewData["MediaTypeId"] = new SelectList(_context.media_types, "MediaTypeId", "MediaTypeId", tracks.MediaTypeId);
            return View(tracks);
        }

        // GET: Tracks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracks = await _context.tracks.FindAsync(id);
            if (tracks == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.albums, "AlbumId", "Title", tracks.AlbumId);
            ViewData["GenreId"] = new SelectList(_context.genres, "GenreId", "GenreId", tracks.GenreId);
            ViewData["MediaTypeId"] = new SelectList(_context.media_types, "MediaTypeId", "MediaTypeId", tracks.MediaTypeId);
            return View(tracks);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] tracks tracks)
        {
            if (id != tracks.TrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TracksExists(tracks.TrackId))
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
            ViewData["AlbumId"] = new SelectList(_context.albums, "AlbumId", "Title", tracks.AlbumId);
            ViewData["GenreId"] = new SelectList(_context.genres, "GenreId", "GenreId", tracks.GenreId);
            ViewData["MediaTypeId"] = new SelectList(_context.media_types, "MediaTypeId", "MediaTypeId", tracks.MediaTypeId);
            return View(tracks);
        }

        // GET: Tracks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracks = await _context.tracks
                .Include(t => t.album)
                .Include(t => t.genre)
                .Include(t => t.media_type)
                .FirstOrDefaultAsync(m => m.TrackId == id);
            if (tracks == null)
            {
                return NotFound();
            }

            return View(tracks);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tracks = await _context.tracks.FindAsync(id);
            _context.tracks.Remove(tracks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TracksExists(long id)
        {
            return _context.tracks.Any(e => e.TrackId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
