using Microsoft.AspNetCore.Mvc;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class StatesController : Controller
    {
        private readonly TourismContext _context;

        public StatesController(TourismContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var states = _context.States.ToList();
            return View(states);
        }

        [Route("states/{stateId:int}")]
        public IActionResult Show(int stateId)
        {
            var state = _context.States.Find(stateId);
            return View(state);
        }
        
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(State state)
        {
            _context.States.Add(state);
            _context.SaveChanges();
            var newStateId = state.Id;
           // return View();
            return RedirectToAction(newStateId.ToString());
        }
    }
}
