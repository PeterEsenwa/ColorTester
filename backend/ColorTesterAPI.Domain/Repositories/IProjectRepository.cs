using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Domain.Repositories;

public interface IProjectCommandRepository
{
    Task AddProjectAsync(Project project);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(Guid id);
}

public interface IProjectQueryRepository
{
    Task<IEnumerable<Project>> GetProjectsAsync(string userId, int page = 1, int pageSize = 5);
    Task<Project?> GetProjectAsync(Guid id, string userId);
}