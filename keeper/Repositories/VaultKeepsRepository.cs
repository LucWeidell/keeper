using System.Collections.Generic;
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
        WHERE id = @id
        LIMIT 1;
        ";
        _db.Execute(sql, new {id});
    }

    internal VaultKeep GetById(int id)
    {
        string sql= @"
        Select * From vaultKeeps WHERE id = @id LIMIT 1;
        ";
        return _db.Query<VaultKeep>(sql, new {id}).FirstOrDefault();
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

    internal List<KeepWitVaultViewModel> GetKeepsByVault(int vaultId)
    {
      string sql = @"
      SELECT
      a.*,
      k.*,
      vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN keeps k ON vk.keepId = k.id
      JOIN accounts a On k.creatorId = a.id
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<Profile, KeepWitVaultViewModel, KeepWitVaultViewModel>(sql, (prof, kwvvm) => {
        kwvvm.Creator = prof;
        return kwvvm;
      }, new {vaultId}, splitOn: "id").ToList();
    }
  }
}