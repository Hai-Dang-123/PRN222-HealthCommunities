using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(LoginDTO loginData);

        Task<ResponseDTO> LogoutAsync();


    }
}
