using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace contact_list.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactListController : ControllerBase
    {
        public static readonly List<Person> people = new List<Person>();

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return Ok(people);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody]Person newPerson)
        {
            if (newPerson.Email == null || newPerson.Id == 0)
            {
                return BadRequest("Invalid input (e.g. required field missing or empty)");
            }

            if (people.Any(person => person.Id == newPerson.Id))
            {
                return BadRequest("Person already existing.");
            }

            people.Add(newPerson);
            return Created("", newPerson);
        }

        [HttpGet]
        [Route("findByName")]
        public IActionResult GetPerson([FromQuery] string nameFilter)
        {
            if (string.IsNullOrEmpty(nameFilter))
            {
                return BadRequest("Invalid or missing name");
            }

            return Ok(people.Where(person => person.LastName.Contains(nameFilter)));
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteItem(int personId)
        {
            if (personId <= 0 && personId > people.Count)
            {
                return BadRequest("Invalid ID supplied");
            }

            var person = people.SingleOrDefault(person => person.Id == personId);
            if (person == null)
            {
                return NotFound("Person not found");
            }
            
            people.Remove(person);
            return NoContent();
        }
    }
}
