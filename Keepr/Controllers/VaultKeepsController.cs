namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vkData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vkData.CreatorId = userInfo.Id;
                VaultKeep vk = _vaultKeepsService.CreateVaultKeep(vkData);
                return Ok(vk);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVaultKeep(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultKeepsService.DeleteVaultKeep(id, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}