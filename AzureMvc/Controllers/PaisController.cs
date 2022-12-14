using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureMvc.Data;
using AzureMvc.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Azure.Storage.Blobs;

namespace AzureMvc.Controllers
{
    public class PaisController : Controller
    {
        private readonly AzureMvcContext _context;

        public PaisController(AzureMvcContext context)
        {
            _context = context;
        }

        // GET: Pais
        public async Task<ActionResult> Index(string searchString)
        {
            var pais = from m in _context.Paises select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pais = pais.Where(s => s.Nome!.Contains(searchString));
            }

            return View(await pais.ToListAsync());
        }

        // GET: Pais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(m => m.PaisId == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaisId,Nome,ImagemPais")] Pais pais, IFormFile ImagemPais)
        {
            if (ModelState.IsValid)
            {
                pais.ImagemPais = UploadImage(ImagemPais);
                _context.Add(pais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Pais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaisId,Nome,ImagemPais")] Pais pais, IFormFile ImagemPais)
        {
            if (id != pais.PaisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pais.ImagemPais = UploadImage(ImagemPais);
                    _context.Update(pais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(pais.PaisId))
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
            return View(pais);
        }

        // GET: Pais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paises == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(m => m.PaisId == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paises == null)
            {
                return Problem("Entity set 'AzureMvcContext.Paises'  is null.");
            }
            var pais = await _context.Paises.FindAsync(id);
            if (pais != null)
            {
                _context.Paises.Remove(pais);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisExists(int id)
        {
          return _context.Paises.Any(e => e.PaisId == id);
        }
        private static string UploadImage(IFormFile imageFile)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=lucasresourcer;AccountKey=1PGao96lhuuABUJZYrHGqzQkoc+0Q+jwLVbwxD7H9FiwdlgkPFIwYmU9aH7f2Ahkr7fu+xHXdKGu+AStRcAb0Q==;EndpointSuffix=core.windows.net";
            string containerName = "imagempais";
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
        private static void DeleteFile(string foto)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=lucasresourcer;AccountKey=1PGao96lhuuABUJZYrHGqzQkoc+0Q+jwLVbwxD7H9FiwdlgkPFIwYmU9aH7f2Ahkr7fu+xHXdKGu+AStRcAb0Q==;EndpointSuffix=core.windows.net";
            string containerName = "imagempais";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            string arquivo = foto.Substring(foto.LastIndexOf('/') + 1);
            var blobClient = blobContainerClient.GetBlobClient(arquivo);
            blobClient.DeleteIfExists();
        }
    }
}
