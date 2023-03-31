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
    }
}