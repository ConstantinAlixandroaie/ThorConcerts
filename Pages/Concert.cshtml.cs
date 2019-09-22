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
    public class ConcertModel : PageModel
    {
        private readonly ConcertController _controller;

        public ConcertViewModel ViewModel { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }

        public ConcertModel(ThorDbContext ctx)
        {
            _controller = new ConcertController(ctx);
        }

        public async Task OnGet(int id)
        {
            try
            {
                ViewModel = await _controller.GetConcertByIdAsync(id);
                IsError = false;
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }
    }
}