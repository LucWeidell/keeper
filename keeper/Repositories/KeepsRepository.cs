using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
        string sql= @"
        Select
        k.*,
        a.*
        From keeps k
        Join accounts a ON a.id = k.creatorId
        ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) => {
            keep.Creator = prof;
            return keep;
        }, splitOn: "id").ToList();    }

    internal Keep GetById(int id)
    {
        string sql= @"
        Select
        k.*,
        a.*
        From keeps k
        Join accounts a ON a.id = k.creatorId
        Where k.id = @id;
        ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) => {
            keep.Creator = prof;
            return keep;
        }, new {id}).FirstOrDefault();    }

    internal Keep RemoveKeep(int id)
    {
      throw new NotImplementedException();
    }

    internal Keep Create(Keep rawKeep)
    {
      string sql = @"
      INSERT INTO vaults (creatorId, name, description, img, views, shares, keeps)
      Values (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      Select LAST_INSERT_ID();
      ";
      rawKeep.Id = _db.ExecuteScalar<int>(sql, rawKeep);
      return rawKeep;    }

    internal Keep Update(Keep foundKeep)
    {
        string sql = @"UPDATE vaults
        SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views,
            shares = @Shares,
            keeps = @Keeps
        WHERE id = @Id
        LIMIT 1;
        ";
        _db.Execute(sql, foundKeep);
        return GetById(foundKeep.Id);    }
  }
}