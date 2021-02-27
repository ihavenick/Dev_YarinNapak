using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dev_YarinNeYapak.Models;
using Microsoft.AspNetCore.Identity;
using Dev_YarinNeYapak.Controllers;
using Dev_YarinNeYapak.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Dev_YarinNeYapak.Controllers
{
    [Produces("application/json")]
    [Route("api/Kayit")]
    public class KayitController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public KayitController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet("{id}", Name = "GetUser")]
        [HttpGet("{id}")]
        public async Task<Kayit> GetById(string id)
        {
            var item = await _userManager.FindByIdAsync(id);
            if (item == null)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(item);
            Kayit a = new Kayit()
            {
                 adsoyad = item.adsoyad,
                  AvatarBase64 = item.AvatarBase64,
                   Email = item.Email,
                     Kullaniciadi = item.UserName,
                      
            };
            return a;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ol([FromBody] Kayit item)
        {
            if (item == null)
            {
                return BadRequest(); 
            }
            var user = new ApplicationUser { UserName = item.Kullaniciadi, Email = item.Email, adsoyad = item.adsoyad, AvatarBase64 = item.AvatarBase64 };
            if (item.facelekayitolmus)
            {
                user.facelekayitolmus = true;
                user.facebookid = item.facebookid;
            }
            else
            {
                user.facelekayitolmus = false;
                user.facebookid = "yok";
            }
            var result = await _userManager.CreateAsync(user, item.sifre);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(item.Email, "Hesabýnýzý Dogrulayýn",
                    $"Bu linke týklayarak hesabýnýzý doðrulayýn: <a href='{callbackUrl}'>link</a>");
                await _signInManager.SignInAsync(user, isPersistent: false);
                //return StatusCode(201);
                return CreatedAtRoute("GetUser", new { id = user.Id }, item);
            }

            return StatusCode(201);
        }
    }
}