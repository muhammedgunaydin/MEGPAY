using Account.API.Controller.Base;
using Account.Application.Accounts.Commands.CreateAccount;
using Account.Application.Accounts.Commands.DeleteAccount;
using Account.Application.Accounts.Commands.UpdateAccount;
using Account.Application.Accounts.Queries.GetAccountById;
using Account.Application.Accounts.Queries.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var query = new GetAllAccountsQuery();
            return CreateActionResult(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var query = new GetAccountByIdQuery { Id = id };
            return CreateActionResult(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountCommand command)
        {
            return CreateActionResult(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var command = new DeleteAccountCommand { Id = id };
            return CreateActionResult(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountCommand command)
        {
            command.Id = id;
            return CreateActionResult(await _mediator.Send(command));
        }
    }
}