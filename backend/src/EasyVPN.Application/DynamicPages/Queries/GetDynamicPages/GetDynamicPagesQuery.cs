using EasyVPN.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyVPN.Application.DynamicPages.Queries.GetDynamicPages;

public record GetDynamicPagesQuery() : IRequest<ErrorOr<List<DynamicPage>>>;