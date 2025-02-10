using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface IRegisterService
    {
        Task<ResponseDTO> Register(RegisterDTO register);
    }
}
