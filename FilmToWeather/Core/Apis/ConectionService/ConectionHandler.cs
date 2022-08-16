using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.ConectionService
{
    public class ConectionHandler : IConectionHandler
    {
        public async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {
                throw new InvalidOperationException("Inquiry not available");
            }
            catch(FlurlHttpException e) when (e.StatusCode == 400)
            {
                throw new InvalidOperationException("Bad request");
            }
        }
    }
}
