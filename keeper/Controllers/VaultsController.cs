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
        private readonly KeepsService _ks;

    public VaultsController(VaultsService vs, KeepsService ks)
    {
      _vs = vs;
      _ks = ks;
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


    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<KeepWitVaultViewModel>>> GetKeeps(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            List<KeepWitVaultViewModel> keeps = _ks.GetKeepsByVault(id, userInfo);
             return Ok(keeps);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
             string result = _vs.RemoveVault(id, userInfo);
            //  Ill maybe return the deleted item
             return Ok(result);
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
            rawVault.CreatorId = userInfo.Id;
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