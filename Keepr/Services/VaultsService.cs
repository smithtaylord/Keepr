namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        private readonly ProfliesService _profilesService;

        public VaultsService(VaultsRepository repo, ProfliesService profilesService)
        {
            _repo = repo;
            _profilesService = profilesService;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repo.CreateVault(vaultData);
            return vault;
        }



        internal Vault GetVaultById(int vaultId, string userId)
        {
            Vault vault = _repo.GetVaultById(vaultId);
            if (vault == null) throw new Exception($"No Vault at id: {vaultId}");
            if (vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception("You cannot see this vault");
            return vault;
        }
        internal List<Vault> GetUsersVaults(string userId)
        {
            _profilesService.GetProfileById(userId);
            List<Vault> vaults = _repo.GetUsersVaults(userId);
            return vaults;
        }
        internal List<Vault> GetMyVaults(string userId)
        {
            _profilesService.GetProfileById(userId);
            List<Vault> vaults = _repo.GetUsersVaults(userId);
            return vaults;
        }
        internal Vault EditVault(Vault vaultData)
        {
            Vault original = this.GetVaultById(vaultData.Id, vaultData.CreatorId);
            original.Name = vaultData.Name != null ? vaultData.Name : original.Name;
            original.IsPrivate = vaultData.IsPrivate != null ? vaultData.IsPrivate : original.IsPrivate;
            int rowsAffected = _repo.EditVault(original);
            if (rowsAffected == 0) throw new Exception("Could not modify for some reason");
            if (rowsAffected > 1) throw new Exception("Something went very wrong and you edited more than one row");
            return original;
        }
        internal string DeleteVault(int id, string userId)
        {
            Vault vault = _repo.GetVaultById(id);
            if (vault == null) throw new Exception($"No Vault at id: {id}");
            if (vault.CreatorId != userId) throw new Exception("You are not allowed to delete this vault, it does not belong to you");
            _repo.DeleteVault(id);
            return $"{vault.Name} vault has been deleted";
        }

    }
}