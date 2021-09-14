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

    internal VaultKeep RemoveVaultKeep(int id, Profile userInfo)
    {
      throw new NotImplementedException();
    }

    private Keep GetKeepByID(int id)
    {
      Keep foundKeep = _ks.GetById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundKeep;
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

    internal VaultKeep CreateVaultKeep(VaultKeep rawVaultKeep)
    {
      throw new NotImplementedException();
    }
  }
}