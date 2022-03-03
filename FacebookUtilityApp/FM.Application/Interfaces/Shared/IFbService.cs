using FM.Application.DTOs.Requests.Facebook.Person;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Facebook.Uid;

namespace FM.Application.Interfaces.Shared
{
    public interface IFbService
    {
        Task<BaseResponse<FbDetailRoot>> GetDetail1Uid(GetDetail1UidRequest request);
    }
}
