using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureMvc.Data;
using AzureMvc.Models;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace AzureMvc.Controllers
{
    public class AmigosController : Controller
    {
        private readonly AzureMvcContext _context;

        public AmigosController(AzureMvcContext context)
        {
            _context = context;
        }

        // GET: Amigos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Amigos.ToListAsync());
        }

        // GET: Amigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigos = await _context.Amigos
                .FirstOrDefaultAsync(m => m.AmigoId == id);
            if (amigos == null)
            {
                return NotFound();
            }

            return View(amigos);
        }

        // GET: Amigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmigoId,AmigoNome,Sobrenome,Email,Telefone,ImagemAmigo,Aniversario,PaisOrigem,EstadoOrigem")] Amigos amigos, IFormFile ImagemAmigo)
        {
            if (ModelState.IsValid)
            {
                amigos.ImagemAmigo = UploadImage(ImagemAmigo);
                _context.Add(amigos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amigos);
        }

        // GET: Amigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigos = await _context.Amigos.FindAsync(id);
            if (amigos == null)
            {
                return NotFound();
            }
            return View(amigos);
        }

        // POST: Amigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmigoId,AmigoNome,Sobrenome,Email,Telefone,ImagemAmigo,Aniversario,PaisOrigem,EstadoOrigem")] Amigos amigos, IFormFile ImagemAmigo)
        {
            if (id != amigos.AmigoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    amigos.ImagemAmigo = UploadImage(ImagemAmigo);
                    _context.Update(amigos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigosExists(amigos.AmigoId))
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
            return View(amigos);
        }

        // GET: Amigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigos = await _context.Amigos
                .FirstOrDefaultAsync(m => m.AmigoId == id);
            if (amigos == null)
            {
                return NotFound();
            }

            return View(amigos);
        }

        // POST: Amigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Amigos == null)
            {
                return Problem("Entity set 'AzureMvcContext.Amigos'  is null.");
            }
            var amigos = await _context.Amigos.FindAsync(id);
            if (amigos != null)
            {
                _context.Amigos.Remove(amigos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmigosExists(int id)
        {
          return _context.Amigos.Any(e => e.AmigoId == id);
        }
        private static string UploadImage(IFormFile imageFile)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=lucasresourcer;AccountKey=1PGao96lhuuABUJZYrHGqzQkoc+0Q+jwLVbwxD7H9FiwdlgkPFIwYmU9aH7f2Ahkr7fu+xHXdKGu+AStRcAb0Q==;EndpointSuffix=core.windows.net";
            string containerName = "imagemamigos";
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
            string containerName = "imagemamigos";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            string arquivo = foto.Substring(foto.LastIndexOf('/') + 1);
            var blobClient = blobContainerClient.GetBlobClient(arquivo);
            blobClient.DeleteIfExists();
        }
    }
}
