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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
        try
        {
             List<Keep> keeps = _ks.GetKeeps();
             return Ok(keeps);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<List<Keep>> Get(int id)
    {
        try
        {
             Keep keep = _ks.GetKeep(id);
             return Ok(keep);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetWithAddDownload(int id)
    {
        try
        {
             Keep keep = _ks.GetKeepAddDownload(id);
             return Ok(keep);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Remove(int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            String result = _ks.RemoveKeep(id, userInfo);
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
    public async Task<ActionResult<Keep>> Create([FromBody] Keep rawKeep)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            rawKeep.CreatorId = userInfo.Id;
            Keep keep = _ks.CreateKeep(rawKeep);
            //  Ill maybe return the deleted item
             return Ok(keep);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit([FromBody] Keep rawKeep, int id)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Account>();
            rawKeep.Id = id;
            Keep keep = _ks.EditKeep(rawKeep, userInfo);
            //  Ill maybe return the deleted item
             return Ok(keep);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }
  }
}