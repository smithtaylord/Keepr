namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _auth = auth;
            _vaultKeepsService = vaultKeepsService;
        }

        [HttpPost]
        [Authorize]
        async public Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vaultsService.CreateVault(vaultData);
                vault.Creator = userInfo;
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.GetVaultById(id, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<KeepInVault>>> GetKeepsInVault(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                List<KeepInVault> keeps = _vaultKeepsService.GetKeepsInVault(id, userInfo?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                vaultData.Id = id;
                Vault vault = _vaultsService.EditVault(vaultData);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        async public Task<ActionResult<string>> DeleteVault(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultsService.DeleteVault(id, userInfo.Id);
                return (Ok(message));
            }
            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
    }
}