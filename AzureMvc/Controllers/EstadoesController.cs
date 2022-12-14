using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureMvc.Data;
using AzureMvc.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Azure.Storage.Blobs;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace AzureMvc.Controllers
{
    public class EstadoesController : Controller
    {
        private readonly AzureMvcContext _context;

        public EstadoesController(AzureMvcContext context)
        {
            _context = context;
        }

        // GET: Estadoes

        public async Task<ActionResult> Index(string searchString)
        {
            var estados = from m in _context.Estados select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                estados = estados.Where(s => s.Nome!.Contains(searchString));
            }

            return View(await estados.ToListAsync());
        }

        // GET: Estadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EstadoId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estadoes/Create
        public IActionResult Create()
        {
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId");
            return View();
        }

        // POST: Estadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoId,Nome,ImagemEstado,PaisId")] Estado estado, IFormFile ImagemEstado)
        {
            if (ModelState.IsValid)
            {
                estado.ImagemEstado = UploadImage(ImagemEstado);
                _context.Add(estado);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }

        // GET: Estadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }

        // POST: Estadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estado estado, IFormFile ImagemEstado)
        {
            if (id != estado.EstadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.EstadoId))
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
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "PaisId", estado.PaisId);
            return View(estado);
        }
        private static string UploadImage(IFormFile imageFile)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=lucasresourcer;AccountKey=1PGao96lhuuABUJZYrHGqzQkoc+0Q+jwLVbwxD7H9FiwdlgkPFIwYmU9aH7f2Ahkr7fu+xHXdKGu+AStRcAb0Q==;EndpointSuffix=core.windows.net";
            string containerName = "imagem";
            var reader = imageFile.OpenReadStream();
            var cloundStorageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = cloundStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(imageFile.FileName);
            Thread.Sleep(10000);
            blob.UploadFromStreamAsync(reader);
            return blob.Uri.ToString();

        }
        // GET: Estadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.Pais)
                .FirstOrDefaultAsync(m => m.EstadoId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'AzureMvcContext.Estados'  is null.");
            }
            var estado = await _context.Estados.FindAsync(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
          return _context.Estados.Any(e => e.EstadoId == id);
        }
        private static void DeleteFile(string foto)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=lucasresourcer;AccountKey=1PGao96lhuuABUJZYrHGqzQkoc+0Q+jwLVbwxD7H9FiwdlgkPFIwYmU9aH7f2Ahkr7fu+xHXdKGu+AStRcAb0Q==;EndpointSuffix=core.windows.net";
            string containerName = "imagem";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            string arquivo = foto.Substring(foto.LastIndexOf('/') + 1);
            var blobClient = blobContainerClient.GetBlobClient(arquivo);
            blobClient.DeleteIfExists();
        }
        
    }
}
