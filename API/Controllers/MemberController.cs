using API.Contracts;
using Application.Members.Commands.CreateMember;
using Application.Members.Commands.DeleteMember;
using Application.Members.Commands.UpdateEmail;
using Application.Members.Commands.UpdateMember;
using Application.Members.Commands.UpdatePassword;
using Application.Members.Queries.GetAll;
using Application.Members.Queries.GetByEmail;
using Application.Members.Queries.GetById;
using Application.Members.Queries.GetByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class MemberController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequest request)
        => Ok(await sender.Send(
            new CreateMemberCommand(
                request.FirstName, 
                request.LastName, 
                request.Email, 
                request.Password)));

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateMemberRequest request)
        => Ok(await sender.Send(
            new UpdateMemberCommand(
                request.MemberId,
                request.FirstName,
                request.LastName)));

    [HttpPut("email")]
    public async Task<IActionResult> UpdateEmail(UpdateEmailRequest request)
         => Ok(await sender.Send(
             new UpdateEmailCommand(
                 request.MemberId,
                 request.Email)));

    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
        => Ok(await sender.Send(
            new UpdatePasswordCommand(
                request.MemberId,
                request.Password)));

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id) => Ok(await sender.Send(new DeleteMemberCommand(id)));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await sender.Send(new GetAllMembersQuery()));

    [HttpGet("id")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await sender.Send(new GetByIdQuery(id)));

    [HttpGet("email")]
    public async Task<IActionResult> GetByEmail(string email) => Ok(await sender.Send(new GetByEmailQuery(email)));

    [HttpGet("name")]
    public async Task<IActionResult> GetByName(string name) => Ok(await sender.Send(new GetByNameQuery(name)));
}
