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

    internal void RemoveKeep(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id;
      ";
      _db.Execute(sql, new {id});
    }

    internal List<Keep> GetByProfileId(string id)
    {
      string sql= @"
        Select
        k.*,
        a.*
        From keeps k
        Join accounts a ON a.id = k.creatorId
        Where k.creatorId = @id;
        ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) => {
            keep.Creator = prof;
            return keep;
        }, new {id}, splitOn: "id").ToList();
    }

    internal Keep Create(Keep rawKeep)
    {
      string sql = @"
      INSERT INTO keeps (creatorId, name, description, img, views, shares, keeps)
      Values (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();
      ";
      rawKeep.Id = _db.ExecuteScalar<int>(sql, rawKeep);
      return GetById(rawKeep.Id);
    }

    internal Keep Update(Keep foundKeep)
    {
        string sql = @"UPDATE keeps
        SET
            name = @Name,
            description = @Description,
            img = @Img
        WHERE id = @Id
        LIMIT 1;
        ";
        _db.Execute(sql, foundKeep);
        return GetById(foundKeep.Id);    }

    internal void UpdateViews(Keep foundKeep)
    {
        string sql = @"UPDATE keeps
        SET
          views = @Views
        WHERE id = @Id
        LIMIT 1;
        ";
        _db.Execute(sql, foundKeep);
    }
    internal void UpdateKeeps(Keep foundKeep)
    {
        string sql = @"UPDATE keeps
        SET
          keeps = @Keeps
        WHERE id = @Id
        LIMIT 1;
        ";
        _db.Execute(sql, foundKeep);
    }
  }
}