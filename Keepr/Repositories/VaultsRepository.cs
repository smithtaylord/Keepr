namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO
            vaults(
                name,
                description,
                img,
                creatorId,
                isPrivate
                )
            VALUES (
                @name,
                @description,
                @img,
                @creatorId,
                @isPrivate
                );
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }



        internal Vault GetVaultById(int vaultId)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @vaultId
            "; Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
            {
                vault.Creator = prof;
                return vault;
            }, new { vaultId }).FirstOrDefault();
            return vault;
        }
        internal int EditVault(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @name,
            isPrivate = @isPrivate
            WHERE id = @id;
            ";
            int rows = _db.Execute(sql, original);
            return rows;
        }
        internal void DeleteVault(int id)
        {
            string sql = @"
            DELETE FROM vaults
            WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}