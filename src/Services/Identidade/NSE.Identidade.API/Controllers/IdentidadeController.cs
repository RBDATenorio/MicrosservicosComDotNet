using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.DTOs.Request;
using NSE.Identidade.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentidadeController : Controller
    {
        private readonly IIdentidadeRepository _repository;
        private readonly IConfiguration _config;

        public IdentidadeController(IIdentidadeRepository repository,
                                    IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTORequest usuarioDTORequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var usuarioNoBanco = await _repository.ObterPorUserName(usuarioDTORequest.UserName);

            if (usuarioNoBanco is null) return NotFound("usuário não encontrado.");

            var usuario = new Usuario(usuarioDTORequest.UserName, usuarioDTORequest.Senha);

            if (usuarioNoBanco.Senha == usuario.Senha) return BadRequest("Senha inválida.");

            var token = GerarToken(usuarioNoBanco);

            return Ok(token);
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

        private string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("JwtConfigurations:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //tokenHandler.WriteToken(token);

            return tokenHandler.WriteToken(token);
        }

    }
}
