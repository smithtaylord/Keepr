namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth0Provider;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetMyVaults()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetAccountVaults(userInfo.Id);
            return Ok(vaults);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Authorize]

    public async Task<ActionResult<Account>> EditAccount([FromBody] Account accountData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            accountData.Id = userInfo.Id;
            Account account = _accountService.EditById(accountData);
            return Ok(account);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
