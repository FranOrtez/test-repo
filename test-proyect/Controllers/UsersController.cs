using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using test_proyect.Models;
using test_proyect.Repositories;


namespace test_proyect.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userRepository.GetById(id);

            if (user is null)
                return NotFound("Usuario no encontrado.");

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                return BadRequest("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("El email es obligatorio.");

            var createdUser = _userRepository.Create(user);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdUser.Id },
                createdUser
            );
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] User user)
        {
            var updated = _userRepository.Update(id, user);

            if (!updated)
                return NotFound("Usuario no encontrado.");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _userRepository.Delete(id);

            if (!deleted)
                return NotFound("Usuario no encontrado.");

            return NoContent();
        }
    }
}
