using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataPreload
{
    internal interface IPreloadSubscribeService
    {
        void Subscribe<TInitializer>(Func<Task> initHandler)
            where TInitializer : IInitializer;
    }
}
