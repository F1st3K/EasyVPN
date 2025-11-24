using EasyZsV.Domain.Entities;
using ErrorOr;
using MediatR;

namespace EasyZsV.Application.DynamicPages.Queries.GetDynamicPages;

public record GetDynamicPagesQuery() : IRequest<ErrorOr<List<DynamicPage>>>;