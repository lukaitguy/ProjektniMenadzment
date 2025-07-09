using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjektniMenadzment.Models;
using ProjektniMenadzment.Models.ViewModels;
using ProjektniMenadzment.Repositories.Interfaces;

namespace ProjektniMenadzment.Controllers
{
    public class ProjektiController : Controller
    {
        private readonly IProjektiRepository _projektiRepository;
        private readonly IZanroviRepository _zanroviRepository;

        public ProjektiController(IProjektiRepository projektiRepository, IZanroviRepository zanroviRepository)
        {
            _projektiRepository = projektiRepository;
            _zanroviRepository = zanroviRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projekti = await _projektiRepository.GetAllAsync();

            var viewModel = projekti.Select(p => new ProjekatViewModel
            {
                Id = p.Id,
                Naziv = p.Naziv,
                Status = p.Status,
                DatumPocetka = p.DatumPocetka,
                Rok = p.Rok,
                KreiraoKorisnikIme = $"{p.KreiraoKorisnik.Ime} {p.KreiraoKorisnik.Prezime}"
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _projektiRepository.GetByIdAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var zanrovi = await _zanroviRepository.GetAllAsync();

            var model = new CreateProjekatRequest
            {
                DatumPocetka = DateTime.Now,
                Zanrovi = zanrovi.Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Naziv
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjekatRequest model)
        {
            if (!ModelState.IsValid)
            {
                var zanrovi = await _zanroviRepository.GetAllAsync();
                model.Zanrovi = zanrovi.Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Naziv
                }).ToList();

                return View(model);
            }

            var projekat = new Projekti
            {
                Id = Guid.NewGuid(),
                Naziv = model.Naziv,
                Opis = model.Opis,
                Status = model.Status,
                Budzet = model.Budzet,
                DatumPocetka = model.DatumPocetka,
                Rok = model.Rok,
                ZanrId = model.ZanrId,
                KreiraoKorisnikId = Guid.Parse("a75c8586-e4db-4c81-9802-0b96e286367a"), // hardcoded for now
                DatumKreiranja = DateTime.UtcNow
            };

            await _projektiRepository.AddAsync(projekat);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var projekat = await _projektiRepository.GetByIdAsync(id);

            if (projekat == null)
                return NotFound();

            var zanrovi = await _zanroviRepository.GetAllAsync();

            var model = new EditProjekatRequest
            {
                Naziv = projekat.Naziv,
                Opis = projekat.Opis,
                Status = projekat.Status,
                Budzet = projekat.Budzet,
                DatumPocetka = projekat.DatumPocetka,
                Rok = projekat.Rok,
                ZanrId = projekat.ZanrId,
                Zanrovi = zanrovi.Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Naziv,
                    Selected = z.Id == projekat.ZanrId
                }).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProjekatRequest model)
        {
            if (!ModelState.IsValid)
            {
                var zanrovi = await _zanroviRepository.GetAllAsync();
                model.Zanrovi = zanrovi.Select(z => new SelectListItem
                {
                    Value = z.Id.ToString(),
                    Text = z.Naziv
                }).ToList();

                return View(model);
            }

            var projekat = new Projekti
            {
                Id = model.Id,
                Naziv = model.Naziv,
                Opis = model.Opis,
                Status = model.Status,
                Budzet = model.Budzet,
                DatumPocetka = model.DatumPocetka,
                Rok = model.Rok,
                ZanrId = model.ZanrId
            };

            await _projektiRepository.UpdateAsync(projekat);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var uspesno = await _projektiRepository.DeleteAsync(id);
            if (!uspesno)
                return NotFound();

            return RedirectToAction("Index");
        }

    }
}
