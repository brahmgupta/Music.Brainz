using System.Linq;
using System.Net;
using FluentResults;
using Music.Brainz.Common.Response;

namespace Music.Brainz.CQRS
{
    public class ErrorResponseFactory : IErrorResponseFactory
    {
        public Response CreateErrorResponse(HttpStatusCode httpStatusCode, ResultBase result)
        {
            return new ErrorResponse(httpStatusCode, result.Errors?.FirstOrDefault());
        }
    }
}
