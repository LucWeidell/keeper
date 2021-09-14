using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;

    public ProfilesController(AccountService accountService, KeepsService ks, VaultsService vs)
    {
      _accountService = accountService;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsInProfile(string profileId)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            List<Keep> keeps = _ks.GetKeepsByProfile(profileId, userInfo);
            return Ok(keeps);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
        // {
        //      List<Keep> keeps = _ks.GetKeeps();
        //      return Ok(keeps);
        // }
        // catch (Exception err)
        // {
        //     return BadRequest(err.Message);
        // }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Keep>>> GetVaultsInProfile(string profileId)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            List<Vault> vaults = _vs.GetVaultsByProfile(profileId, userInfo);
            return Ok(vaults);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        return _accountService.GetProfileById(id);
    }
    }
}