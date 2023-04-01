namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

        private readonly KeepsService _keepsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vkData)
        {
            _keepsService.GetOneKeep(vkData.KeepId);
            Vault vault = _vaultsService.GetVaultById(vkData.VaultId, vkData.CreatorId);
            if (vault.CreatorId != vkData.CreatorId) throw new Exception("You can't create a vault keep for someone else.");
            VaultKeep vk = _repo.CreateVaultKeep(vkData);
            return vk;
        }


        internal List<KeepInVault> GetKeepsInVault(int vaultId, string userId)
        {
            Vault vault = _vaultsService.GetVaultById(vaultId, userId);
            List<KeepInVault> keeps = _repo.GetKeepsInVault(vaultId);
            return keeps;
        }
        internal string DeleteVaultKeep(int id, string userId)
        {
            VaultKeep vk = _repo.GetOne(id);
            if (vk == null) throw new Exception($"no vaultkeep at id: {id}");
            if (vk.CreatorId != userId) throw new Exception("You are not this account owner, you cannot delete this");
            _repo.RemoveVaultKeep(id);
            return "your vault keep has been delete";
        }
    }
}