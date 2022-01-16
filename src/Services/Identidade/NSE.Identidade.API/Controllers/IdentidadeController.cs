using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.DTOs.Request;
using NSE.Identidade.API.Models;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentidadeController : Controller
    {
        private readonly IIdentidadeRepository _repository;

        public IdentidadeController(IIdentidadeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTORequest usuarioDTORequest)
        {
            return Ok();
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDTORequest usuarioDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var usuario = new Usuario(usuarioDTO.UserName, usuarioDTO.Senha);

            if (await _repository.ObterPorUserName(usuarioDTO.UserName) != null)
            {
                return BadRequest("Nome de Usuario ja cadastrado.");
            }

            await _repository.CriarNovoUsuario(usuario);

            return Created($"api/identidade/usuarios/{usuario.Id}", usuario);
        } 
        
    }
}
