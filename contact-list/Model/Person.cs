using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace contact_list
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
