using Microsoft.Extensions.Caching.Distributed;
using URLShortenAspire.DAL.Units;
using URLShortenAspire.Models.Database;

namespace URLShortenAspire.Core
{
	public class ShortenService(UnitOfWork unitOfWork, IDistributedCache cache)
	{
		readonly UnitOfWork _unitOfWork = unitOfWork;
		private readonly IDistributedCache _cache = cache;

		public URLEntity ShorternUrl(string url)
		{

			URLEntity? ent = _unitOfWork.UrlRepository.Get(u => u.OriginalURL == url);

			if (ent != null)
				return ent;

			URLEntity? existing = new();
			string shortUrl = "";

			while (existing != null)
			{
				shortUrl = GetRandomString();

				existing = _unitOfWork.UrlRepository.Get(u => u.Shorten == shortUrl);
			}

			URLEntity upd = _unitOfWork.UrlRepository.Add(new URLEntity
			{
				OriginalURL = url,
				Shorten = shortUrl
			});

			_unitOfWork.Save();

			return upd;
		}

		public async Task<string?> GetFullUrl(string shortURL)
		{
			var cachedUrl = await _cache.GetStringAsync(shortURL);

			if (cachedUrl != null)
				return cachedUrl;

			URLEntity? ent = _unitOfWork.UrlRepository.Get(url => url.Shorten == shortURL);

			if (ent != null)
				await _cache.SetStringAsync(shortURL, ent.OriginalURL);

			return ent?.OriginalURL;
		}

		private static string GetRandomString()
		{
			Random random = new();
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

			return new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
