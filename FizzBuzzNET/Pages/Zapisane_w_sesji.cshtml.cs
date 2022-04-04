using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzBuzzNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FizzBuzzNET.Pages
{
    public class Zapisane_w_sesjiModel : PageModel
    {
        public Fizzbuzz fizzbuzz { get; set; }
        public Lista_user Lista_user = new Lista_user();

        public void OnGet()
        {
            var Number = HttpContext.Session.GetString("Number");
            if (Number != null)
                Lista_user =
                JsonConvert.DeserializeObject<Lista_user>(Number);

            HttpContext.Session.SetString("Number",
               JsonConvert.SerializeObject(Lista_user));

        }
    }
}
