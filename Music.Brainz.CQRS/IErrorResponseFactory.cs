using System.Net;
using FluentResults;
using Music.Brainz.Common.Response;

namespace Music.Brainz.CQRS
{
    public interface IErrorResponseFactory
    {
        Response CreateErrorResponse(HttpStatusCode httpStatusCode, ResultBase result);
    }
}