using System;
using Spring.Context;
using Spring.Context.Support;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public static class Spring
    {
        public static object GetService(string sObject)
        {
            IApplicationContext ctx = ContextRegistry.GetContext(resx::General.ObjetoRuv);
            return ctx.GetObject(sObject);
        }
    }
}
