using StackExchange.Redis;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Handlers
{
	public class RedisHandler(IConnectionMultiplexer redis)
	{
		public async Task SetListAsync<T>(string key, IEnumerable<T> items)
		{
			var db = redis.GetDatabase();

			await db.KeyDeleteAsync(key);

			foreach (var item in items)
			{
				await db.ListRightPushAsync(key, JsonSerializer.Serialize(item));
			}
		}

		public async Task<IEnumerable<T>> GetListAsync<T>(string key, int page = 1, int pageSize = int.MaxValue)
		{
			var db = redis.GetDatabase();
			var start = (page - 1) * pageSize;
			var stop = start + pageSize - 1;

			var keyType = await db.KeyTypeAsync(key);

			if (keyType != RedisType.List)
			{
				return new List<T>();
			}

			var jsonList = await db.ListRangeAsync(key, start, stop);

			if (jsonList.Length == 0)
			{
				return new List<T>();
			}

			var items = new List<T>();

			foreach (var json in jsonList)
			{
				if (json.HasValue)
				{
					var item = JsonSerializer.Deserialize<T>(json!);

					if (item != null)
					{
						items.Add(item);
					}
				}
			}

			return items;
		}

		public List<Company> GenerateCacheAsync(int companyCount, int contactPerCompany)
		{
			var companies = new List<Company>();

			for (int i = 0; i < companyCount; i++)
			{
				var company = new Company
				{
					Name = $"Company {i}",
					Industry = "Technology",
					Contacts = new List<Contact>()
				};

				for (int j = 0; j < contactPerCompany; j++)
				{
					var contact = new Contact
					{
						Name = $"Contact {j}",
						Phone = $"123-456-{j:D4}",
						Address = new Address
						{
							Street = $"Street {j}",
							City = "City",
							PostalCode = $"{j:D5}"
						}
					};
					company.Contacts.Add(contact);
				}

				companies.Add(company);
			}

			return companies;
		}
	}
}