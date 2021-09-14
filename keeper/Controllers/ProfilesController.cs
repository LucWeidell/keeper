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
        private readonly ProfilesService _ps;

    public ProfilesController(AccountService accountService, ProfilesService ps)
    {
      _accountService = accountService;
      _ps = ps;
    }


    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsInProfile(string profileId)
    {
        try
        {
            // FIXME
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            return Ok();
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
            // FIXME
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            return Ok();
        }
        catch (System.Exception)
        {

            throw;
        }
    }
    }
}