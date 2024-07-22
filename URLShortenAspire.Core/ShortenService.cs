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
			_unitOfWork.BeginTransaction();

			URLEntity upd = _unitOfWork.UrlRepository.Add(new URLEntity
			{
				OriginalURL = url,
				Shorten = url
			});

			_unitOfWork.Commit();

			return upd;
		}

		public string? GetFullUrl(string shortURL)
		{
			URLEntity? ent = _unitOfWork.UrlRepository.Get(url => url.Shorten == shortURL);

			return ent?.Shorten;
		}
	}
}
