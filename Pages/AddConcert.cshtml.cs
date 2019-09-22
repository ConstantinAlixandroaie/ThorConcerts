using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThorConcerts.Controllers;
using ThorConcerts.Database;
using ThorConcerts.ViewModels;

namespace ThorConcerts.Pages
{
    public class AddConcertModel : PageModel
    {
        private ConcertController _controller;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string BandName { get; set; }

        [BindProperty]
        public int Capacity { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }

        public AddConcertModel(ThorDbContext ctx)
        {
            _controller = new ConcertController(ctx);
        }

        public async Task OnPost()
        {
            try
            {
                var vm = new ConcertViewModel()
                {
                    BandName = BandName,
                    Capacity = Capacity,
                    Name = Name
                };

                await _controller.AddConcert(vm);
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
                throw;
            }
        }
    }
}