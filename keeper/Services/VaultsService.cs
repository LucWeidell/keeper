using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal List<Vault> GetVaults()
    {
        // Need to default giving only public
      return _repo.GetAll();
    }

    internal Vault GetVault(int id, Profile userInfo)
    {
      Vault foundVault = _repo.GetById(id);
      if(foundVault == null) {
        throw new Exception("Invalid Id");
      }
      if(foundVault.IsPrivate && foundVault.creatorId != userInfo.Id){
        throw new Exception("Not your Keep to See!");
      }
      return foundVault;    }

    internal Vault RemoveVault(int id, Profile userInfo)
    {
      Vault foundVault = GetVault(id, userInfo);
      if(foundVault.creatorId != userInfo.Id){
        throw new Exception("Not your Keep to remove!");
      }
      return _repo.RemoveVault(id);    }

    internal Vault CreateVault(Vault rawVault)
    {
      return _repo.Create(rawVault);
    }

    internal Vault EditVault(Vault rawVault, Profile userInfo)
    {
      Vault foundVault = GetVault(rawVault.Id, userInfo);
      if(foundVault.creatorId != userInfo.Id){
        throw new Exception("Not your Vault to Edit!");
      }
      foundVault.Name = rawVault.Name ?? foundVault.Name;
      foundVault.IsPrivate = (rawVault.IsPrivate != foundVault.IsPrivate) ? rawVault.IsPrivate : foundVault.IsPrivate;
      foundVault.Description = rawVault.Description ?? foundVault.Description;
      return _repo.Update(foundVault);    }
  }
}