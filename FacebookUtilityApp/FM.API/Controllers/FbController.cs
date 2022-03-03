using FM.Application.DTOs.Requests.Facebook.Person;
using FM.Application.Interfaces.Repositories;
using FM.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FM.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class FbController : ControllerBase
    {
        private readonly IFbService _fbService;
        private readonly IFbRepository _fbRepository;

        public FbController(IFbService fbService, IFbRepository fbRepository)
        {
            _fbService = fbService;
            _fbRepository = fbRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailUid(string id)
        {
            var response = await _fbService.GetDetail1Uid(new GetDetail1UidRequest()
            {
                Token =
"EAAGNO4a7r2wBALtczrR211LL2X6eZCyxShy7mFrZAK6xMOYfJqBNGX6pdsnBkbIq9pTs4deunwngM4yGxUGYex1gBChM9ZB0Tm8XBFegA4zXd7CX2zQNSlldJmpJ2OCVXCTmCpRLabxMT1emvmssAWo1R7Y2m10B8fsZC0AEMOeWDzRDlyzaZALoIVeYq0MsZD",
                Uid = "100001578994326"
            });

            return response.GetResponse(response);
        }

        [HttpGet("Testing")]
        public async Task<IActionResult> Testing()
        {
            //var response = await _fbService.GetDetail1Uid(new GetDetail1UidRequest()
            //{
            //    Token =
            //        "EAAGNO4a7r2wBALtczrR211LL2X6eZCyxShy7mFrZAK6xMOYfJqBNGX6pdsnBkbIq9pTs4deunwngM4yGxUGYex1gBChM9ZB0Tm8XBFegA4zXd7CX2zQNSlldJmpJ2OCVXCTmCpRLabxMT1emvmssAWo1R7Y2m10B8fsZC0AEMOeWDzRDlyzaZALoIVeYq0MsZD",
            //    Uid = "100001578994326"
            //});

            return Ok("Testing");
        }
    }
}
