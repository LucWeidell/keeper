using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal void RemoveVaultKeep(int id)
    {
        string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @id;
        ";
        _db.Execute(sql, new {id});    }

    internal VaultKeep GetById(int id)
    {
        string sql= @"
        Select
        vk.*,
        From vaultKeeps vk
        Where vk.id = @id;
        ";
        return _db.Query(sql, new {id}).FirstOrDefault();
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
      string sql = @"
      INSERT INTO vaultKeeps (creatorId, keepId, vaultId)
      Values (@CreatorId, @KeepId, @VaultId);
      SELECT LAST_INSERT_ID();
      ";
      rawVaultKeep.Id = _db.ExecuteScalar<int>(sql, rawVaultKeep);
      return rawVaultKeep;     }
  }
}