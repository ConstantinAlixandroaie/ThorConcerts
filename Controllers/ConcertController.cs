using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThorConcerts.Database;
using ThorConcerts.ViewModels;

namespace ThorConcerts.Controllers
{
    public class ConcertController
    {
        private readonly ThorDbContext _ctx;

        public ConcertController(ThorDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<ConcertViewModel>> GetConcertsAsync()
        {
            var rv = new List<ConcertViewModel>();

            var concerts = await _ctx.Concerts.ToListAsync();
            foreach (var conert in concerts)
            {
                var vm = new ConcertViewModel() { BandName = conert.BandName, Name = conert.BandName, Capacity = conert.Capacity };
                rv.Add(vm);
            }

            return rv;
        }

        public async Task<ConcertViewModel> GetConcertByIdAsync(int id)
        {
            var concertModel = await _ctx.Concerts.FirstOrDefaultAsync(x => x.Id == id);

            if (concertModel == null)
            {
                return null;
            }

            var rv = new ConcertViewModel() { BandName = concertModel.BandName, Name = concertModel.BandName, Capacity = concertModel.Capacity };
            return rv;
        }

        public async Task AddConcert(ConcertViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            if (vm.BandName == null)
                throw new ArgumentException("Band name is null");

            if (vm.Name == null)
                throw new ArgumentException("Name is null");

            var concert = new Concert()
            {
                BandName = vm.BandName,
                Name = vm.Name,
                Capacity = vm.Capacity ?? 0
            };

            _ctx.Concerts.Add(concert);
            await _ctx.SaveChangesAsync();
        }

        public async Task EditConcert(ConcertViewModel vm)
        {
            var concert = await GetConcertByIdAsync(vm.Id);

            if (concert == null)
                throw new ArgumentException($"A concert with the given '{vm.Id}' was not found");

            if (!string.IsNullOrEmpty(vm.BandName))
                concert.BandName = vm.BandName;

            if (!string.IsNullOrEmpty(vm.Name))
                concert.Name = vm.Name;

            if (vm.Capacity != null)
                concert.Capacity = vm.Capacity ?? 0;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteConcert(int id)
        {
            //                                                        conditie
            var concert = await _ctx.Concerts.FirstOrDefaultAsync(x => x.Id == id); // FirstOrDefault inseamna, returneaza primu obiect care'l gasesti la cautare cu conditia ta, daca nu gasesti returneaza default -> adica null

            if (concert == null)
                throw new ArgumentException($"A concert with the given '{id}' was not found");

            _ctx.Concerts.Remove(concert);
            await _ctx.SaveChangesAsync();
        }
    }
}
