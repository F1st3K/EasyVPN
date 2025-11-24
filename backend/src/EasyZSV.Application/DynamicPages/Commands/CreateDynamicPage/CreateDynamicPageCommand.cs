using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Commands.CreateDynamicPage;

public record CreateDynamicPageCommand(string Route, string Title, string Content) : IRequest<ErrorOr<Created>>;