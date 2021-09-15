using keeper.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
namespace keeper.Repositories
{

  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Vault> GetAll()
    {
        string sql= @"
        Select
        v.*,
        a.*
        From vaults v
        Join accounts a ON a.id = v.creatorId
        Where v.isPrivate = 0;
        ";
        return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) => {
            vault.Creator = prof;
            return vault;
        }, splitOn: "id").ToList();
    }

        public Vault GetById(int id)
    {
        string sql= @"
        Select
        v.*,
        a.*
        From vaults v
        Join accounts a ON a.id = v.creatorId
        Where v.id = @id;
        ";
        return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) => {
            vault.Creator = prof;
            return vault;
        }, new {id}).FirstOrDefault();
    }

    internal List<Vault> GetByProfileId(string profileId)
    {
        string sql= @"
        Select
        v.*,
        a.*
        From vaults v
        Join accounts a ON a.id = v.creatorId
        Where v.creatorId = @profileId;
        ";
        return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) => {
            vault.Creator = prof;
            return vault;
        }, new {profileId}, splitOn: "id").ToList();
    }

    public void RemoveVault(int id)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE id = @id;
        ";
        _db.Execute(sql, new {id});
    }

    public Vault Create(Vault rawVault)
    {
        string sql = @"
        INSERT INTO vaults (creatorId, name, description, isPrivate)
        Values (@CreatorId, @Name, @Description, @IsPrivate);
        Select LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, rawVault);
        return GetById(id);
    }



    public Vault Update(Vault foundVault)
    {
        string sql = @"UPDATE vaults
        SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
        WHERE id = @Id
        LIMIT 1;
        ";
        _db.Execute(sql, foundVault);
        return GetById(foundVault.Id);
    }
}
}