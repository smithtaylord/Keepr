namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

        public KeepsController(KeepsService keepsService, Auth0Provider auth)
        {
            _keepsService = keepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]

        async public Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                keepData.CreatorId = userInfo.Id;
                Keep keep = _keepsService.CreateKeep(keepData);
                keep.Creator = userInfo;
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Keep>> GetAllKeeps()
        {
            try
            {
                List<Keep> keeps = _keepsService.GetAllKeeps();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Keep>> GetOneKeep(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Keep keep = _keepsService.GetOneKeep(id, userInfo?.Id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        async public Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Keep keep = _keepsService.EditKeep(id, keepData, userInfo.Id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<ActionResult<string>> DeleteKeep(int id)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _keepsService.DeleteKeep(id, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}