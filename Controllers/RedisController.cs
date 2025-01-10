using Microsoft.AspNetCore.Mvc;
using WebApplication1.Handlers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RedisController(RedisHandler redisListHandler) : ControllerBase
	{
		[HttpGet("generate")]
		public async Task GenerateDataAsync()
		{
			var companies = redisListHandler.GenerateCacheAsync(1075, 5);
			await redisListHandler.SetListAsync("ListKey", companies);
		}

		[HttpGet("data")]
		public async Task<IEnumerable<Company>> GetCompaniesListAsync()
		{
			var companies = await redisListHandler.GetListAsync<Company>("ListKey", 1, int.MaxValue);

			if (companies == null)
			{
				return new List<Company>();
			}

			return companies;
		}
	}
}
