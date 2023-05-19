using Microsoft.EntityFrameworkCore;
using MovieDbSystem.Data.Repositories;
using MovieDbSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbSystem.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        private readonly ApplicationDbContext _context;

        public ActorService(ApplicationDbContext _context) : base(_context) { }
    }
}
