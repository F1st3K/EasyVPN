using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Commands.RemoveDynamicPage;

public record RemoveDynamicPageCommand(string Route) : IRequest<ErrorOr<Deleted>>;