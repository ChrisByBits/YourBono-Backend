using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourBonoPlatform.Bonds.Domain.Model.Queries;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Bonds.Interfaces.REST.Transform;

namespace YourBonoPlatform.Bonds.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BondsController(IBondQueryService bondQueryService, 
                            IBondCommandService bondCommandService): ControllerBase
{
    [HttpGet("user-id/{userId:int}")]
    public async Task<IActionResult> GetBondsByUserId(int userId)
    {
        var query = new GetAllBondsByUserIdQuery(userId);
        var bonds = await bondQueryService.Handle(query);
        var bondResources = bonds.Select(BondResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bondResources);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBondById(int id)
    {
        var query = new GetBondByIdQuery(id);
        var bond = await bondQueryService.Handle(query);
        if (bond == null)
        {
            return NotFound();
        }
        var bondResource = BondResourceFromEntityAssembler.ToResourceFromEntity(bond);
        return Ok(bondResource);
    }
    
    [HttpGet("cash-flow/{id:int}")]
    public async Task<IActionResult> GetBondCashFlowById(int id)
    {
        var query = new GetCashFlowByBondIdQuery(id);
        var cashFlow = await bondQueryService.Handle(query);
        if (cashFlow == null)
        {
            return NotFound();
        }
        return Ok(cashFlow);
    }
    
    
    
}