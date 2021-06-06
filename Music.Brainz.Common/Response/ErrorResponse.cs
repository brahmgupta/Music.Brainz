using System.Net;
using FluentResults;

namespace Music.Brainz.Common.Response
{
    public class ErrorResponse : ValueResponse<Error>
    {
        public ErrorResponse(Error error) : base(HttpStatusCode.BadRequest, error)
        {
        }

        public ErrorResponse(HttpStatusCode httpStatusCode, Error error) : base(httpStatusCode, error)
        {
        }
    }
}
