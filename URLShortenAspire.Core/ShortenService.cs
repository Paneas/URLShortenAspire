using URLShortenAspire.DAL.Units;
using URLShortenAspire.Models.Database;

namespace URLShortenAspire.Core
{
	public class ShortenService
	{
		UnitOfWork _unitOfWork;
		public ShortenService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public URLEntity ShorternUrl(string url)
		{
			URLEntity? ent = _unitOfWork.UrlRepository.Get(u => u.OriginalURL == url);

			if (ent != null)
				return ent;

			URLEntity? existing = new URLEntity();
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

		public string? GetFullUrl(string shortURL)
		{
			URLEntity? ent = _unitOfWork.UrlRepository.Get(url => url.Shorten == shortURL);

			return ent?.OriginalURL;
		}

		private string GetRandomString()
		{
			Random random = new Random();
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

			return new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
