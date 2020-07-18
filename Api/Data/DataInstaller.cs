using System;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class DataInstaller
    {
        public static Action<DbContextOptionsBuilder> Install(bool isDevelopment)
        {
            if (!isDevelopment)
            {
                Console.Out.Write("No production ready data source implemented");
            }
            
            return action => { action.UseInMemoryDatabase(typeof(MyContext).Name); };
        }
    }
}
