using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzBuzzNET.Models;
using FizzBuzzNET.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace FizzBuzzNET.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FizzbuzzContext _context;
        public IndexModel(ILogger<IndexModel> logger, FizzbuzzContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Fizzbuzz> Fizzbuzzes { get; set; }
        [BindProperty]
        public Fizzbuzz Fizzbuzz { get; set; }
        public Lista_user Lista_user = new Lista_user();
        public void OnGet()
        {
            var Number = HttpContext.Session.GetString("Number");
            if (Number != null)
            {
                Lista_user = JsonConvert.DeserializeObject<Lista_user>(Number);
            }

        }
        public string response;
        public string Alert_wiek;
        public IActionResult OnPost()
        {

            var Number = HttpContext.Session.GetString("Number");
            if (Number != null)
            {
                Lista_user = JsonConvert.DeserializeObject<Lista_user>(Number);
            }
            if (!ModelState.IsValid)
            {
                Alert_wiek = "Błąd! Wybierz liczbę z zakresu 1899-2022";
                return Page();
            }
            else
            {
                Fizzbuzz.Fibuz(Fizzbuzz);
                Fizzbuzz.Data = DateTime.Now;
                _context.Fizzbuzz.Add(Fizzbuzz);
                _context.SaveChanges();

                Lista_user.user.Add(Fizzbuzz);

                HttpContext.Session.SetString("Number",
                JsonConvert.SerializeObject(Lista_user));
                response = $"{Fizzbuzz.Name}, {Fizzbuzz.LastName}, {Fizzbuzz.Number}, {Fizzbuzz.Fibuz(Fizzbuzz)}";
                return Page();
            }

            
           

        }
    }
}
