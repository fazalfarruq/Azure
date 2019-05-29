using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AddressWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace AddressWebApp.Controllers
{
    [Route("api/address")]
    [AllowAnonymous]
    public class AddressApiController : Controller
    {
        private DataContext db = new DataContext();
        private List<string> _cities;

        public AddressApiController()
        {
            _cities = new List<string> { "Toronto", "Calgary", "Austin", "Dallas", "Vancouver", "Montreal", "Venice", "Toronto-2", "Vancouver-2" };
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var cities = db.Addresses.Where(a => a.City.Contains(term)).Select(p => p.City).ToList();
                //var cities = _cities.Where(c => c.Contains(term, StringComparison.InvariantCultureIgnoreCase)).ToList();
                return Ok(cities);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}