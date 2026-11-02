using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.AddBeautician.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using NV.Buautician.Domain.Entities;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Beautician.Controllers
{
    public class BeauticianController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;

        public BeauticianController(IMediator mediator,IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager) 
        {
            _mediator=mediator;
            _userServices=userServices;
        }

        #region AllBeautician
        [HttpGet("all-beautician")]
        public async Task<IActionResult> AllBeautician([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllBeauticianByQuery(paginationRequest);
            var beauticianResult = await _mediator.Send(query);
            #region Switch Statement
            return beauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beauticianResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region AddBeauticians
        [HttpPost("AddBeauticians")]

        public async Task<IActionResult> AddBeauticians([FromBody] AddBeauticianRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addBeauticianResult = await _mediator.Send(command);
            #region Switch Statement
            return addBeauticianResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(Beauticians), addBeauticianResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addBeauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addBeauticianResult.Errors),
                _ => BadRequest("Invalid Fields for Add Beauticians ")
            };

            #endregion
        }

        #endregion

        #region BeauticiansById
        [HttpGet("Beauticians/{id}")]
        public async Task<IActionResult> GetBeauticiansById([FromRoute] string id)
        {
            var query = new GetBeauticianByIdQuery(id);
            var beauticianResult = await _mediator.Send(query);
            #region Switch Statement
            return beauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beauticianResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region UpdateBeautician
        [HttpPatch("UpdateBeautician/{id}")]

        public async Task<IActionResult> UpdateBeautician([FromRoute] string id, [FromBody] UpdateBeauticianRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateBeauticianResult = await _mediator.Send(command);
            #region Switch Statement
            return updateBeauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateBeauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateBeauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateBeauticianResult.Errors),
                _ => BadRequest("Invalid Fields for Assign Beauticians")
            };

            #endregion

        }
        #endregion

    }
}
