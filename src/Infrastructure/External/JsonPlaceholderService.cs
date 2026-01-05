using Application.Posts.Dto;
using System.Net.Http.Json;

namespace Infrastructure.External
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PostDTO>> GetPostsAsync()
        {
            try
            {
                var posts = await _httpClient.GetFromJsonAsync<List<PostDTO>>("posts");

                return posts ?? throw new ApplicationException(
                    "No data returned from posts endpoint."
                );
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(
                    "Error calling JsonPlaceholder GET/posts",
                    ex
                );
            }
        }

        public async Task<PostDTO> CreatePostAsync(PostDTO post)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("posts", post);
                response.EnsureSuccessStatusCode();

                var created = await response.Content.ReadFromJsonAsync<PostDTO>();

                return created ?? throw new ApplicationException(
                    "No data returned when creating post"
                );
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(
                    "Error calling JsonPlaceholder POST/posts",
                    ex
                );
            }
        }
    }
}
