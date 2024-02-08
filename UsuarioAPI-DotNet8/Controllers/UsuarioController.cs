using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI_DotNet8.Data;
using UsuarioAPI_DotNet8.Entities;

namespace UsuarioAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly DataContext _context;

        public UsuarioController(DataContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>>GetAllUser()
        {

            var user = await _context.Usuarios.ToListAsync();
            
            return Ok(user);

        }
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Usuario>> GetAllNameUser(int id)
        {

            var user = await _context.Usuarios.FindAsync(id);
            if(user is null) {
                return BadRequest("Id no encontrado");
            }
            return Ok(user);

        }
        [HttpPost]

        public async Task<ActionResult<List<Usuario>>> AddUser(Usuario user)
        {

             _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return Ok( await _context.Usuarios.ToListAsync());

        }
        [HttpPut]

        public async Task<ActionResult<List<Usuario>>> UpdateUser(Usuario updateUser)
        {

            var dbUser = await _context.Usuarios.FindAsync(updateUser.Id);
            if (dbUser is null)
                return NotFound("Id no encontrado");
            
            dbUser.Name = updateUser.Name;
            dbUser.FirstName = updateUser.FirstName;
            dbUser.LastName = updateUser.LastName;  
            dbUser.Place=   updateUser.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Usuarios.ToListAsync());

        }
        [HttpDelete]

        public async Task<ActionResult<List<Usuario>>> DeleteUser(int id)
        {

            var dbUser = await _context.Usuarios.FindAsync(id);
            if (dbUser is null)
                return NotFound("Id no encontrado");

            _context.Usuarios.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Usuarios.ToListAsync());

        }
    }
}
