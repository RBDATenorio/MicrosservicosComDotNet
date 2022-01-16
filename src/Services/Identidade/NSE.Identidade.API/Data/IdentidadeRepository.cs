using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.DTOs.Response;
using NSE.Identidade.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Data
{
    public class IdentidadeRepository : IIdentidadeRepository
    {
        private readonly IdentidadeDbContext _context;

        public IdentidadeRepository(IdentidadeDbContext context)
        {
            _context = context;
        }

        public async Task CriarNovoUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<UsuarioDTOResponse> ObterPorId(int id)
        {
            return await _context.Usuarios
                                .AsNoTracking()
                                .Where(u => u.Id == id)
                                .Select(u => new UsuarioDTOResponse{ UserName = u.UserName })
                                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> ObterPorUserName(string username)
        {
            return await _context.Usuarios.AsNoTracking()
                                            .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
                                            
        }
    }
}
