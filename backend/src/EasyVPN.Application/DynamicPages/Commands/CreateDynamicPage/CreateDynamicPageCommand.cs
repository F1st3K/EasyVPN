using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.CreateDynamicPage;

public record CreateDynamicPageCommand(string Route, string Title, string Content) : IRequest<ErrorOr<Created>>;