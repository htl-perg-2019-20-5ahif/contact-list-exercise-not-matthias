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
            if (people.Where(person => person.Id == newPerson.Id).Count() == 0)
            {
                people.Add(newPerson);
                return Created("", newPerson);
            }

            return BadRequest("Invalid input(e.g.required field missing or empty)");
        }

        [HttpGet]
        [Route("findByName/{nameFilter}")]
        public IActionResult GetPerson(string nameFilter)
        {
            if (!string.IsNullOrEmpty(nameFilter))
            {
                return Ok(people.Where(person => person.LastName.Contains(nameFilter)));
            }

            return BadRequest("Invalid or missing name");
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteItem(int personId)
        {
            try
            {
                var person = people.SingleOrDefault(person => person.Id == personId);
                if (person == null)
                {
                    return BadRequest("Invalid ID supplied");
                }

                people.Remove(person);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Person not found");
            }

        }
    }
}
