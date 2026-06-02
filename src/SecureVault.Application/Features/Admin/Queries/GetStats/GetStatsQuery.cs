using MediatR;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetStats;
public record GetStatsQuery() : IRequest<StatsDto>;
