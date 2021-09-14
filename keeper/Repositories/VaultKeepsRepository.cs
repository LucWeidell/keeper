using System;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {
    internal void RemoveVaultKeep(int id)
    {
      throw new NotImplementedException();
    }

    internal VaultKeep GetById(int id)
    {
        // FIXME
        throw new NotImplementedException();

    //     string sql =@"
    //     Select
    //     v.*,
    //     a.*
    //     From vaults v
    //     Join accounts a ON a.id = v.creatorId
    //     Where v.id = @id;
    //     ";
    }

    internal VaultKeep Create(VaultKeep rawVaultKeep)
    {
      throw new NotImplementedException();
    }
  }
}