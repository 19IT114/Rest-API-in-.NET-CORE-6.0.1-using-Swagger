using DemoApi.Data;
using DemoApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    //[ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext _dbcontext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _dbcontext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _dbcontext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if(contact != null)
            {
                return Ok(contact);
            }
            return Ok("No matching Contact Found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest contactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = contactRequest.Name,
                Address = contactRequest.Address,
                Email = contactRequest.Email,
                Phone = contactRequest.Phone,
            };
            await _dbcontext.Contacts.AddAsync(contact);
            await _dbcontext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute]Guid id, AddContactRequest contactRequest)
        {
            var contact = await _dbcontext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if(contact != null)
            {
                contact.Name = (contactRequest.Name != null)? contactRequest.Name:contact.Name;
                contact.Address = (contactRequest.Address != null) ? contactRequest.Address : contact.Address;
                contact.Email = (contactRequest.Email != null) ? contactRequest.Email : contact.Email;
                contact.Phone = (contactRequest.Phone != null) ? contactRequest.Phone : contact.Phone;

                await _dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await _dbcontext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if(contact != null)
            {
                _dbcontext.Contacts.Remove(contact);
                await _dbcontext.SaveChangesAsync();
                return Ok("Contact Deleted Successfully.");
            }
            return NotFound();
        }
    }
}
