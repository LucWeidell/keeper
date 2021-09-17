using System;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultsRepository _vs;
    private readonly KeepsRepository _ks;
    private readonly VaultKeepsRepository _vks;

    public VaultKeepsService(VaultsRepository vs, KeepsRepository ks, VaultKeepsRepository vks)
    {
      _vs = vs;
      _ks = ks;
      _vks = vks;
    }

    internal string RemoveVaultKeep(int id, Profile userInfo)
    {
      VaultKeep foundVaultKeep = GetVaultKeepByID(id);
      if(foundVaultKeep.CreatorId != userInfo.Id){
        throw new Exception("Not your VaultKeep to remove!");
      }
      _vks.RemoveVaultKeep(id);
      return "Delorted VaultKeep";
    }

    private Keep GetKeepByID(int id)
    {
      Keep foundVaultKeep = _ks.GetById(id);
      if (foundVaultKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundVaultKeep;
    }
    private Vault GetVaultByID(int id)
    {
      Vault foundVault = _vs.GetById(id);
      if (foundVault == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundVault;
    }
    internal VaultKeep GetVaultKeepByID(int id)
    {
      VaultKeep foundVaultKeep = _vks.GetById(id);
      if (foundVaultKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundVaultKeep;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep rawVaultKeep, Profile userInfo)
    {
      if(userInfo == null){
        throw new Exception("Must be logged in to put keep in vault.");
      }
      Vault foundVault = GetVaultByID(rawVaultKeep.vaultId);
      if(foundVault.CreatorId != userInfo.Id){
        throw new Exception("Can only add keeps to your vaults.");
      }
      rawVaultKeep.CreatorId = userInfo.Id;
      return _vks.Create(rawVaultKeep);
    }
  }
}