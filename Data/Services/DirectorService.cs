using MovieDbSystem.Data.Repositories;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.Services
{
    public class DirectorService : EntityBaseRepository<Director>, IDirectorService
    {
        private readonly ApplicationDbContext _context;
        public DirectorService(ApplicationDbContext _context) : base(_context) { }
    }
}
