using MediatR;

namespace SCMSystem.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
