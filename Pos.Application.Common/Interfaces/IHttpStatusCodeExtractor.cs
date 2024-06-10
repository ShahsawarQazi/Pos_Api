using System.Net;

namespace Pos.Application.Common.Interfaces
{
    public interface IHttpStatusCodeExtractor
    {
        /// <summary>
        /// Returns <see cref="HttpStatusCode"/> against errorCode
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        int Extract(int errorCode);
    }
}
