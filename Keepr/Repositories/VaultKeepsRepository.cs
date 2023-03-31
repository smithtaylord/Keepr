namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vkData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vkData);
            vkData.Id = id;
            return vkData;
        }

        internal List<KeepInVault> GetKeepsInVault(int vaultId)
        {
            string sql = @"
            SELECT
            vk.*,
            v.*,
            c.*,
            k.*
            FROM vaultKeeps vk
            JOIN keeps k ON vk.keepId = k.id
            JOIN vaults v ON vk.vaultId = v.id
            JOIN accounts c ON vk.creatorId = c.id
            WHERE vaultId = @vaultId;
            ";
            List<KeepInVault> keepsInVault = _db.Query<VaultKeep, Vault, Profile, KeepInVault, KeepInVault>(sql, (vk, v, p, kiv) =>
            {
                kiv.VaultKeepId = vk.Id;
                kiv.VaultId = v.Id;
                kiv.Creator = p;
                return kiv;
            }, new { vaultId }).ToList();
            return keepsInVault;
        }

        internal VaultKeep GetOne(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaultKeeps
            WHERE id = @id;
            ";
            VaultKeep vk = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
            return vk;
        }

        internal void RemoveVaultKeep(int id)
        {
            string sql = @"
            DELETE FROM 
            vaultKeeps
            WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}