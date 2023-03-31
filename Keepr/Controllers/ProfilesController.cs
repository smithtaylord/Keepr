namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfliesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public ProfilesController(ProfliesService profilesService, Auth0Provider auth, KeepsService keepsService, VaultsService vaultsService)
        {
            _profilesService = profilesService;
            _auth = auth;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfileById(string id)
        {
            try
            {
                Profile profile = _profilesService.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetUsersKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _keepsService.GetUsersKeeps(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetUsersVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vaultsService.GetUsersVaults(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}