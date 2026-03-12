using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProductsDemo.Models;

namespace ProductsDemo.Controllers
{
    public class BinderController : ApiController
    {
        static List<Binder> binders = new List<Binder>();
        static int newBinderId = 1;

        // Function to get the binders
        [HttpGet]
        public IEnumerable<Binder> GetAllBinders()
        {
            return binders;
        }

        // Get a specific binder by ID
        [HttpGet]
        public IHttpActionResult GetBinder(int id)
        {
            var binder = binders.FirstOrDefault(b => b.Id == id);
            if(binder == null)
            {
                return NotFound();
            }
            return Ok(binder);
        }

        // Remove binder by ID
        [HttpDelete]
        public IHttpActionResult DeleteBinder(int id)
        {
            var binder = binders.FirstOrDefault(b => b.Id == id);
            if(binder == null)
            {
                return NotFound();
            }
            binders.Remove(binder);
            return Ok(new { message = "Binder deleted successfully" });
        }
    }
}