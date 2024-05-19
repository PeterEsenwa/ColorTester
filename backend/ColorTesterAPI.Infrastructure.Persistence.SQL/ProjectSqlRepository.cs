using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL
{
    public class ProjectSqlRepository : IProjectCommandRepository, IProjectQueryRepository
    {
        private readonly ColorTesterDbContext _context;

        public ProjectSqlRepository(ColorTesterDbContext context)
        {
            _context = context;
        }

        public async Task AddProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(string userId, int page = 1, int pageSize = 5)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectAsync(Guid id, string userId)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        }
    }
}