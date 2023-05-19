using MovieDbSystem.Data.Repositories;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.Services
{
    public interface IDirectorService : IEntityBaseRepository<Director>
    {
    }
}
