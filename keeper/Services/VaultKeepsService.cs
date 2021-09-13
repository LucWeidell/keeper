using System;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep RemoveVaultKeep(int id, Profile userInfo)
    {
      throw new NotImplementedException();
    }

    internal VaultKeep CreateVaultKeep(VaultKeep rawVaultKeep)
    {
      throw new NotImplementedException();
    }
  }
}