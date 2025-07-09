using Microsoft.AspNetCore.Mvc;
using ProjektniMenadzment.Models;
using ProjektniMenadzment.Models.ViewModels;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Controllers
{
    public class ZadaciController : Controller
    {
        private readonly IZadaciRepository _zadaciRepository;

        public ZadaciController(IZadaciRepository zadaciRepository)
        {
            _zadaciRepository = zadaciRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(Guid projekatId)
        {
            var zadaci = await _zadaciRepository.GetByProjekatIdAsync(projekatId);

            var viewModel = zadaci.Select(z => new ZadaciViewModel
            {
                Id = z.Id,
                Naslov = z.Naslov,
                Opis = z.Opis,
                Status = z.Status,
                Prioritet = z.Prioritet,
                Rok = z.Rok != null ? z.Rok.Value : null,
                DatumKreiranja = z.DatumKreiranja,
                DodeljenKorisnikuIme = z.DodeljenKorisniku != null
                ? $"{z.DodeljenKorisniku.Ime} {z.DodeljenKorisniku.Prezime}"
                : "Nije dodeljen"
            }).ToList();

            ViewBag.ProjekatId = projekatId;

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var zadatak = await _zadaciRepository.GetByIdAsync(id);
            if (zadatak == null)
                return NotFound();

            var model = new ZadaciViewModel
            {
                Id = zadatak.Id,
                Naslov = zadatak.Naslov,
                Opis = zadatak.Opis,
                Status = zadatak.Status,
                Prioritet = zadatak.Prioritet,
                Rok = zadatak.Rok,
                DatumKreiranja = zadatak.DatumKreiranja,
                DodeljenKorisnikuIme = zadatak.DodeljenKorisniku != null
                    ? $"{zadatak.DodeljenKorisniku.Ime} {zadatak.DodeljenKorisniku.Prezime}"
                    : "Nije dodeljen"
            };
            ViewBag.ProjekatId = zadatak.ProjekatId;

            return View(model);
        }
        [HttpGet]
        public IActionResult Create(Guid projekatId)
        {
            var zadatak = new CreateZadatakRequest
            {
                ProjekatId = projekatId,               
            };
            return View(zadatak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateZadatakRequest zadatak)
        {
            if (!ModelState.IsValid)
                return View(zadatak);

            var createZadatak = new Zadaci
            {
                Id = Guid.NewGuid(),
                ProjekatId = zadatak.ProjekatId,
                Naslov = zadatak.Naslov,
                Opis = zadatak.Opis,
                Rok = zadatak.Rok,
                Prioritet = zadatak.Prioritet,
                DodeljenKorisnikuId = null,
                DatumKreiranja = DateTime.UtcNow,
                Status = "Nije zapocet",
                KreiraoKorisnikId = Guid.Parse("a75c8586-e4db-4c81-9802-0b96e286367a")
            };

            await _zadaciRepository.AddAsync(createZadatak);
            return RedirectToAction("Index", new { projekatId = zadatak.ProjekatId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var zadatak = await _zadaciRepository.GetByIdAsync(id);
            if (zadatak == null) return NotFound();

            var model = new CreateZadatakRequest
            {
                ProjekatId = zadatak.ProjekatId,
                Naslov = zadatak.Naslov,
                Opis = zadatak.Opis,
                Prioritet = zadatak.Prioritet,
                Rok = zadatak.Rok.HasValue ? zadatak.Rok.Value : null
            };

            ViewBag.ZadatakId = zadatak.Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateZadatakRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var zadatak = await _zadaciRepository.GetByIdAsync(id);
            if (zadatak == null) return NotFound();

            zadatak.Naslov = model.Naslov;
            zadatak.Opis = model.Opis;
            zadatak.Prioritet = model.Prioritet;
            zadatak.Rok = model.Rok;

            await _zadaciRepository.UpdateAsync(zadatak);

            return RedirectToAction("Index", new { projekatId = model.ProjekatId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var zadatak = await _zadaciRepository.GetByIdAsync(id);
            if (zadatak == null) return NotFound();

            return View(zadatak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zadatak = await _zadaciRepository.GetByIdAsync(id);
            if (zadatak == null)
                return NotFound();

            var projekatId = zadatak.ProjekatId;

            await _zadaciRepository.DeleteAsync(id);

            return RedirectToAction("Index", new { projekatId });
        }


    }
}
