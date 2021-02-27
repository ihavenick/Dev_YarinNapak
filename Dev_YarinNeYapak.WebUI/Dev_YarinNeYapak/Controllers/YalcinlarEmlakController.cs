using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dev_YarinNeYapak.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Dev_YarinNeYapak.Controllers
{
    public class YalcinlarEmlakController : Controller
    {
        private readonly ApplicationDbContext _context;
        public YalcinlarEmlakController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            SiteYVM sYVM = new SiteYVM();


            List<SelectListItem> ApartmanListesi = new List<SelectListItem>();

            foreach (KiralikEvler item in _context.KiralikEvler)
            {
                ApartmanListesi.Add(new SelectListItem() { Text = item.KiralikEvAdi, Value = item.KiralikEvlerId.ToString() });
            }

            ViewBag.Apartmanlistesi = ApartmanListesi;

            return View(sYVM);
        }
        [HttpPost]
        public IActionResult Index(SiteYVM sYVM)
        {
            if (ModelState.IsValid)
            {
                int KiralikEvlerId;
                if (int.TryParse(sYVM.SecilenApartman, out KiralikEvlerId))
                {
                    return RedirectToAction("Goruntule", "YalcinlarEmlak", new { id = KiralikEvlerId });
                }
            }
            List<SelectListItem> ApartmanListesi = new List<SelectListItem>();

            foreach (KiralikEvler item in _context.KiralikEvler)
            {
                ApartmanListesi.Add(new SelectListItem() { Text = item.KiralikEvAdi, Value = item.KiralikEvlerId.ToString() });
            }

            ViewBag.Apartmanlistesi = ApartmanListesi;
            return View(sYVM);
        }
        public IActionResult Goruntule(int? id)
        {
            if (id != null)
            {
                ViewBag.Id = id;
                List<EvZamanYVM> Alist = new List<EvZamanYVM>();
                var a = _context.KiralikEvler
                    .Include(x=>x.NezamanKiralanmis)
                    .Where(x => x.KiralikEvlerId == id).FirstOrDefault();
                if (a != null)
                {
                    foreach (var item in a.NezamanKiralanmis)
                    {
                        string Bay;
                        string Bgun;
                        if (item.Baslangic.Month < 10)
                        {
                            Bay = "0" + item.Baslangic.Month;
                        }
                        else
                        {
                            Bay = item.Baslangic.Month.ToString();
                        }
                        if (item.Baslangic.Day < 10)
                        {
                            Bgun = "0" + item.Baslangic.Day;
                        }
                        else
                        {
                            Bgun = item.Baslangic.Day.ToString();
                        }
                        string Biay;
                        string Bigun;
                        if (item.Bitis.Month < 10)
                        {
                            Biay = "0" + item.Bitis.Month;
                        }
                        else
                        {
                            Biay = item.Bitis.Month.ToString();
                        }
                        if (item.Bitis.Day < 10)
                        {
                            Bigun = "0" + item.Bitis.Day;
                        }
                        else
                        {
                            Bigun = item.Bitis.Day.ToString();
                        }
                        EvZamanYVM EzYm = new EvZamanYVM()
                        {
                            Baslangic = item.Baslangic.Year + "-" + Bay + "-" + Bgun,
                            Bitis = item.Bitis.Year + "-" + Biay + "-" + Bigun,
                        };
                        Alist.Add(EzYm);
                    }
                }
                return View(Alist);
            }

            else
                return RedirectToAction("Index");
        }

        public IActionResult EvEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult EvEkle(KiralikEvVM KEVM)
        {
            if (ModelState.IsValid)
            {
                KiralikEvler KE = new KiralikEvler()
                {
                    KiralikEvAdi = KEVM.kiralikevadi
                };
                _context.KiralikEvler.Add(KE);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(KEVM);
        }

        public IActionResult KiraEkle(int Id)
        {
            KiraEkleVM keVM = new KiraEkleVM()
            {
                KiralikEvlerId = Id,
                Baslangic = DateTime.Now,
                Bitis = DateTime.Now,
            };
            return PartialView(keVM);
        }
        [HttpPost]
        public IActionResult KiraEkle(KiraEkleVM KEVM)
        {
            if (ModelState.IsValid)
            {
                var a = _context.KiralikEvler
                    .Include(x => x.NezamanKiralanmis)
                    .Where(x => x.KiralikEvlerId == KEVM.KiralikEvlerId).FirstOrDefault();
                KiralananTarih kt = new KiralananTarih()
                {
                    Baslangic = KEVM.Baslangic,
                    Bitis = KEVM.Bitis,
                    kiralananev = a
                };
                _context.KiralananTarih.Add(kt);
                a.NezamanKiralanmis.Add(kt);
                _context.KiralikEvler.Update(a);
                _context.SaveChanges();
                return RedirectToAction("Goruntule",new { id = KEVM.KiralikEvlerId });
            }
            return PartialView(KEVM);
        }


    }

    public class KiraEkleVM
    {
        [Required(ErrorMessage = "id hatasi")]
        [Display(Name = "Id")]
        public int KiralikEvlerId { get; set; }
        [Required(ErrorMessage = "Kira baþlangýç zamaný belirtiniz")]
        [Display(Name = "Baþlangýç Zamaný")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Baslangic { get; set; }
        [Required(ErrorMessage = "Kira bitiþ zamaný seçiniz")]
        [Display(Name = "Bitiþ zamaný")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Bitis { get; set; }
    }

    public class SiteYVM
    {
        [Required(ErrorMessage = "apartman seçiniz")]
        [Display(Name = "Apartman")]
        public string SecilenApartman { get; set; }
    }

    public class EvZamanYVM
    {
        public string Baslangic { get; set; }
        public string Bitis { get; set; }
    }


    public class KiralikEvVM
    {
        [Required(ErrorMessage = "Ev adý giriniz")]
        [Display(Name = "Kiralik ev adi")]
        public string kiralikevadi { get; set; }
    }

    public class KiralikEvler
    {
        public KiralikEvler()
        {
            NezamanKiralanmis = new List<KiralananTarih>();
        }
        public int KiralikEvlerId { get; set; }
        public string KiralikEvAdi { get; set; }
        public List<KiralananTarih> NezamanKiralanmis { get; set; }
    }
    public class KiralananTarih
    {
        public int KiralananTarihId { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public KiralikEvler kiralananev { get; set; }
    }
}