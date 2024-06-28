namespace URLShortenAspire.Web;

public class BackendApiClient(HttpClient httpClient)
{
	public async Task<string> ShortURL(string url, CancellationToken cancellationToken = default)
	{

		var response = await httpClient.GetAsync($"/short?url={url}", cancellationToken);

		string shortedURL = await response.Content.ReadAsStringAsync() ?? "";

		return shortedURL;
	}
}

