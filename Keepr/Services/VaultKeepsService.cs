namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vkData)
        {
            VaultKeep vk = _repo.CreateVaultKeep(vkData);
            return vk;
        }

        internal List<KeepInVault> GetKeepsInVault(int vaultId, string userId)
        {
            Vault vault = _vaultsService.GetVaultById(vaultId, userId);
            List<KeepInVault> keeps = _repo.GetKeepsInVault(vaultId);
            return keeps;
        }
    }
}