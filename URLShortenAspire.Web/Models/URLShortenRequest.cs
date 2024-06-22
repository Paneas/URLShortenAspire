using System.ComponentModel.DataAnnotations;

namespace URLShortenAspire.Web.Models
{
	public class URLShortenRequest
	{
		[Required]
		[Url]
		public string Url { get; set; } = default!;
	}
}
