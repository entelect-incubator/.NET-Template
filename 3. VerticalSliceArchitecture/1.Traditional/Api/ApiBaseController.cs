namespace Api;

using MediatR;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ApiBaseController : ControllerBase
{
    private IMediator mediator;

    public IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
}
