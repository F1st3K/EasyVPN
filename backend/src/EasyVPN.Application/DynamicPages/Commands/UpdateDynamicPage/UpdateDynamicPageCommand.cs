using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.UpdateDynamicPage;

public record UpdateDynamicPageCommand(string Route, string NewRoute, string Title, string Content) : IRequest<ErrorOr<Updated>>;