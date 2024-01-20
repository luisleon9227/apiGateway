using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net.Http.Headers;

namespace Api.Aggregators
{
    public class UsersPostsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var userResponseContent = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var postResponseContent = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<User>>(userResponseContent);
            var posts = JsonConvert.DeserializeObject<List<Post>>(postResponseContent);

            foreach (var user in users)
            {
                var userPosts = posts.Where(p => p.UserId == user.Id).ToList();
                user.Posts.AddRange(userPosts);
            }

            var postByUser = JsonConvert.SerializeObject(users);

            var stringContent = new StringContent(postByUser)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<Header>(), "OK");
        }
    }
}
