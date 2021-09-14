using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetKeeps()
    {
      return _repo.GetAll();
    }

    internal Keep GetKeep(int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if(foundKeep == null) {
        throw new Exception("Invalid Id");
      }
      return foundKeep;
    }

    internal Keep RemoveKeep(int id, Profile account)
    {
      Keep foundKeep = GetKeep(id);
      if(foundKeep.CreatorId != account.Id){
        throw new Exception("Not your Keep to remove!");
      }
      return _repo.RemoveKeep(id);
    }

    internal Keep CreateKeep(Keep rawKeep)
    {
      rawKeep.Views = 0;
      rawKeep.Shares = 0;
      rawKeep.Keeps = 0;
      return _repo.Create(rawKeep);
    }

    internal Keep EditKeep(Keep rawKeep, Profile userInfo)
    {
      Keep foundKeep = GetKeep(rawKeep.Id);
      if(foundKeep.CreatorId != userInfo.Id){
        throw new Exception("Not your Keep to Edit!");
      }
      foundKeep.Name = rawKeep.Name ?? foundKeep.Name;
      foundKeep.Description = rawKeep.Description ?? foundKeep.Description;
      foundKeep.Img = rawKeep.Img ?? foundKeep.Img;
      foundKeep.Views = rawKeep.Views ?? foundKeep.Views;
      foundKeep.Shares = rawKeep.Shares ?? foundKeep.Shares;
      foundKeep.Keeps = rawKeep.Keeps ?? foundKeep.Keeps;
      return _repo.Update(foundKeep);
    }
  }
}