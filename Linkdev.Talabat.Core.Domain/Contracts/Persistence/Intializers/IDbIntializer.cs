using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers
{
    public interface IDbIntializer
    {
        Task InitalizeAsync();
        Task SeedAsync();
    }
}
