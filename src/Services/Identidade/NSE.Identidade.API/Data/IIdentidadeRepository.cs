using NSE.Identidade.API.DTOs.Response;
using NSE.Identidade.API.Models;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Data
{
    public interface IIdentidadeRepository
    {
        Task<UsuarioDTOResponse> ObterPorId(int id);
        Task<Usuario> ObterPorUserName(string username);
        Task CriarNovoUsuario(Usuario usuario);
    }
}
