using System.Net;
using Pos.Application.Common.Interfaces;

namespace Pos.Application.Common.Extensions
{
    public class HttpStatusCodeExtractor : IHttpStatusCodeExtractor
    {
        public int Extract(int errorCode)
        {
            // If code an HttpStatusCode, return it
            if (Enum.IsDefined(typeof(HttpStatusCode), errorCode))
                return errorCode;

            errorCode = errorCode switch
            {
                _ => (int)HttpStatusCode.BadRequest,
            };
            return errorCode;
        }
    }
}
