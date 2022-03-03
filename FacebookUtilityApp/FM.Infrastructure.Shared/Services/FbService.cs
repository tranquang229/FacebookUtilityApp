
using AutoMapper;
using FM.Application.DTOs.Requests.Facebook.Person;
using FM.Application.Interfaces.Shared;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Facebook.Uid;
using FM.Infrastructure.Contexts;
using Newtonsoft.Json;

namespace FM.Infrastructure.Shared.Services
{
    public class FbService : IFbService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FbService(HttpClient httpClient, IMapper mapper, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _context = context;
        }

        public async Task<BaseResponse<FbDetailRoot>> GetDetail1Uid(GetDetail1UidRequest request)
        {
            string url = $"https://graph.facebook.com/" + request.Uid +
                         "?fields=age_range,birthday,devices,education,email,gender,hometown,relationship_status,is_verified,work,name,location{name,location},updated_time,locale, feed.limit(200){{created_time, message,status_type,icon,description,link,name,permalink_url}},friends.limit(1).summary(1),subscribers.limit(0).summary(1)&access_token=" + request.Token;
            var response = await _httpClient.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            FbDetailRoot fbDetailRoot = JsonConvert.DeserializeObject<FbDetailRoot>(result);
            return new BaseResponse<FbDetailRoot>(fbDetailRoot);
        }
    }
}
