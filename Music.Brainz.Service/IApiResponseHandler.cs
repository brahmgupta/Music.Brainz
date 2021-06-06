using System.Net.Http;
using System.Threading.Tasks;
using FluentResults;

namespace Music.Brainz.Infrastructure
{
    public interface IApiResponseHandler
    {
        Task<Result<TResult>> HandleResponse<TResult>(HttpResponseMessage response);
    }
}