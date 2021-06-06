using System;
using Microsoft.AspNetCore.Mvc;
using Music.Brainz.Common.Response;

namespace Music.Brainz.API
{
    public static class ResponseExtensions
    {
        public static ActionResult<TValue> ToActionResult<TValue>(this Response @this) where TValue : class
        {
            switch (@this)
            {
                case null:
                    throw new ArgumentNullException(nameof(@this));
                case ErrorResponse errorResponse:
                    return new ObjectResult(errorResponse.Value)
                    {
                        StatusCode = (int)errorResponse.HttpStatusCode
                    };

                case ValueResponse<TValue> valueResponse:
                    return new ObjectResult(valueResponse.Value)
                    {
                        StatusCode = (int)valueResponse.HttpStatusCode
                    };
            }

            return new StatusCodeResult((int)@this.HttpStatusCode);
        }
    }
}
