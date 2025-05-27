using Transfer.API.Controller.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transfer.Application.Transfers.Command.CreateTransfer;
using Transfer.Application.Transfers.Command.Transfer;
using Transfer.Application.Transfers.Command.UpdateTransfer;
using Transfer.Application.Transfers.Queries.GetAllTransfers;
using Transfer.Application.Transfers.Queries.GetTransferById;
using Transfer.Application.Transfers.Queries.GetTransferStatus;

namespace Transfer.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public TransfersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetTransferStatus(Guid id)
        {
            var query = new GetTransferStatusQuery(id);
            var result = await _mediator.Send(query);

            if (result == "NotFound")
                return NotFound();

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllTransfers()
        {
            var query = new GetAllTransfersQuery();
            return CreateActionResult(await _mediator.Send(query));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferById(Guid id)
        {
            var query = new GetTransferByIdQuery { Id = id };
            return CreateActionResult(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer(CreateTransferCommand command)
        {
            return CreateActionResult(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(Guid id)
        {
            var command = new DeleteTransferCommand { Id = id };
            return CreateActionResult(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer(Guid id, [FromBody] UpdateTransferCommand command)
        {
            command.Id = id;
            return CreateActionResult(await _mediator.Send(command));
        }
    }
}