using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Commands.RemoveDynamicPage;

public record RemoveDynamicPageCommand(string Route) : IRequest<ErrorOr<Deleted>>;