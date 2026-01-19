using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Ydin
{
    public static class DependecyStuffInitializor
    {
        public static class ILogger
        {
            public static ILog GetDefault()
            {
                return new AlexisLogger();
            }
        }
    }
}