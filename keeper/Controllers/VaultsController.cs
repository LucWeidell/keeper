using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
    [Route("/api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [HttpGet]
    public ActionResult<List<Vault>> Get()
    {
        try
        {
            List<Vault> vaults = _vs.GetVaults();
            return Ok(vaults);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Vault>>> Get(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Vault vault = _vs.GetVault(id, userInfo);
             return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Remove(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Vault vault = _vs.RemoveVault(id, userInfo);
            //  Ill maybe return the deleted item
             return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault rawVault)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            rawVault.creatorId = userInfo.Id;
            Vault vault = _vs.CreateVault(rawVault);
            //  Ill maybe return the deleted item
             return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault rawVault, int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            rawVault.Id = id;
            Vault vault = _vs.EditVault(rawVault, userInfo);
            //  Ill maybe return the deleted item
             return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }
}