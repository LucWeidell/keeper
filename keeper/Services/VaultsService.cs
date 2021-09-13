using System;
using System.Collections.Generic;
using keeper.Models;

namespace keeper.Services
{
  public class VaultsService
  {
    internal List<Vault> GetVaults()
    {
        // Need to default giving only public
      throw new NotImplementedException();
    }

    internal Vault GetVault(int id, Profile userInfo)
    {
      throw new NotImplementedException();
    }

    internal Vault RemoveVault(int id, Profile userInfo)
    {
      throw new NotImplementedException();
    }

    internal Vault CreateVault(Vault rawKeep)
    {
      throw new NotImplementedException();
    }

    internal Vault EditVault(Vault rawKeep, Profile userInfo)
    {
      throw new NotImplementedException();
    }
  }
}