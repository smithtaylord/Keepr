namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO
    keeps(
        name,
        description,
        img,
        creatorId
    )
VALUES (
        @name,
        @description,
        @img,
        @creatorId
    );
       SELECT LAST_INSERT_ID();     
            "; int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        // NOTE I ADDED ORDER BY TO GET CONSISTENT DATA BACK ON FRONT END, HOPEFULLY STILL PASSES ALL TESTS
        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            k.*, a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            ORDER BY k.id;
            "; List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }).ToList();
            return keeps;
        }

        internal Keep GetOneKeep(int id)
        {
            string sql = @"
            SELECT
            k.*, a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id;
            "; Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { id }).FirstOrDefault();
            return keep;
        }
        internal List<Keep> GetUsersKeeps(string userId)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE a.id = @userId;
            "; List<Keep> keep = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;

            }, new { userId }).ToList();
            return keep;

        }
        internal int EditKeep(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @name,
            description = @description
            WHERE id = @id
            ";
            int rows = _db.Execute(sql, original);
            return rows;
        }

        internal void DeleteKeep(int id)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }

    }
}