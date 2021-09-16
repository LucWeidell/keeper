using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    private readonly VaultKeepsRepository _vkRepo;

    private readonly VaultsService _vs;


    public KeepsService(KeepsRepository repo, VaultsService vs, VaultKeepsRepository vkRepo)
    {
      _repo = repo;
      _vs = vs;
      _vkRepo = vkRepo;
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
      foundKeep.Views++;
      _repo.UpdateViews(foundKeep);
      return foundKeep;
    }

    internal string RemoveKeep(int id, Profile account)
    {
      Keep foundKeep = GetKeep(id);
      if(foundKeep.CreatorId != account.Id){
        throw new Exception("Not your Keep to remove!");
      }
       _repo.RemoveKeep(id);
       return "Delorted Keep";
    }

    internal Keep CreateKeep(Keep rawKeep)
    {
      rawKeep.Views = 0;
      rawKeep.Shares = 0;
      rawKeep.Keeps = 0;
      return _repo.Create(rawKeep);
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
      List<Keep> keeps = _repo.GetByProfileId(profileId);
      return keeps;
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
      return _repo.Update(foundKeep);
    }

    internal List<KeepWitVaultViewModel> GetKeepsByVault(int vaultId, Profile userInfo)
    {
      _vs.GetVault(vaultId, userInfo);
      return _vkRepo.GetKeepsByVault(vaultId);
    }
  }
}