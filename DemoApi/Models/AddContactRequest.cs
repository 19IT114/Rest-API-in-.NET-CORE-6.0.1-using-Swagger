using System.ComponentModel.DataAnnotations;

namespace DemoApi.Model
{
    public class AddContactRequest
    {         
      
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

    }
}
