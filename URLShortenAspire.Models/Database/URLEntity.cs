namespace URLShortenAspire.Models.Database
{
	public class URLEntity : BaseEntity
	{
		public string OriginalURL { get; set; } = default!;
		public string Shorten { get; set; } = default!;
	}
}
