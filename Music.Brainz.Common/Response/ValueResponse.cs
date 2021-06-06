using System.Net;
using FluentResults;

namespace Music.Brainz.Common.Response
{
    public class ValueResponse<TValue> : Response where TValue : class
    {
        public ValueResponse(HttpStatusCode httpStatusCode, TValue value) : base(httpStatusCode)
        {
            Value = value;
        }

        public TValue Value { get; }
        public bool IsFailed => Value is Error;
        public bool IsSuccess => !IsFailed;
        public Error Error => IsFailed ? Value as Error : null;
    }
}
