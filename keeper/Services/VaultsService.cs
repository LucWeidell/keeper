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
      if(foundVault.IsPrivate && foundVault.CreatorId != userInfo.Id){
        throw new Exception("Not your Vault to See!");
      }
      return foundVault;    }

    internal String RemoveVault(int id, Profile userInfo)
    {
      Vault foundVault = GetVault(id, userInfo);
      if(foundVault.CreatorId != userInfo.Id){
        throw new Exception("Not your Keep to remove!");
      }
      _repo.RemoveVault(id);
      return "Delorted Vault";
    }

    internal Vault CreateVault(Vault rawVault)
    {
      return _repo.Create(rawVault);
    }

    internal Vault EditVault(Vault rawVault, Profile userInfo)
    {
      Vault foundVault = GetVault(rawVault.Id, userInfo);
      if(foundVault.CreatorId != userInfo.Id){
        throw new Exception("Not your Vault to Edit!");
      }
      foundVault.Name = rawVault.Name ?? foundVault.Name;
      foundVault.IsPrivate = (rawVault.IsPrivate != foundVault.IsPrivate) ? rawVault.IsPrivate : foundVault.IsPrivate;
      foundVault.Description = rawVault.Description ?? foundVault.Description;
      return _repo.Update(foundVault);    }
  }
}