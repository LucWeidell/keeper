using System;
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
    [Authorize]

    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController(VaultKeepsService vks)
    {
        _vks = vks;
    }





    [HttpDelete("{id}")]
    public async Task<ActionResult<VaultKeep>> Remove(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            string result = _vks.RemoveVaultKeep(id, userInfo);
            //  Ill maybe return the deleted item
             return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep rawVaultKeep)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            rawVaultKeep.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vks.CreateVaultKeep(rawVaultKeep);
            //  Ill maybe return the deleted item
             return Ok(vaultKeep);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
}
}