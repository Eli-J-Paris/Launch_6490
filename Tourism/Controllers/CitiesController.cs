using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class CitiesController : Controller
    {
        private readonly TourismContext _context;

        public CitiesController(TourismContext context)
        {
            _context = context;
        }

        [Route("/states/{stateId:int}/cities")]
        public IActionResult Index(int stateId)
        {
            var state = _context.States
                .Include(s => s.Cities)
                .Where(s => s.Id == stateId)
                .First();

            return View(state);
        }

        [Route("states/{stateId:int}/cities/new")]
        public IActionResult New(int stateId)
        {
            var state = _context.States.Where(s => s.Id == stateId)
        .Include(s => s.Cities)
        .First();

            return View(state);
        }
        ///cities

        [Route("/states/{stateId:int}")]
        public IActionResult Create(City Cit,int stateId)
        {
            var state = _context.States
                 .Where(m => m.Id == stateId)
                 .Include(m => m.Cities)
                 .First();

            state.Cities.Add(Cit);
            _context.Cities.Add(Cit);
            _context.SaveChanges();

            return RedirectToAction("index", new { stateId = state.Id });
        }


    }
}
