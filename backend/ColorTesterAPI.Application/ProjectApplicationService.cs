using ColorTesterAPI.Application.DTOs;
using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorTesterAPI.Application
{
    public class ProjectApplicationService
    {
        private readonly IProjectCommandRepository _projectCommandRepository;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public ProjectApplicationService(
            IProjectCommandRepository projectCommandRepository,
            IProjectQueryRepository projectQueryRepository)
        {
            _projectCommandRepository = projectCommandRepository;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<Guid> CreateProjectAsync(CreateProjectRequest request, string userId)
        {
            var project = new Project
            {
                UserId = userId,
                PaletteId = request.PaletteId,
                MockupName = request.MockupName
            };

            await _projectCommandRepository.AddProjectAsync(project);

            return project.Id;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(string userId)
        {
            return await _projectQueryRepository.GetProjectsAsync(userId);
        }

        public async Task<Project?> GetProjectAsync(Guid id, string userId)
        {
            return await _projectQueryRepository.GetProjectAsync(id, userId);
        }

        public async Task UpdateProjectAsync(Guid id, string userId, UpdateProjectRequest request)
        {
            var project = await _projectQueryRepository.GetProjectAsync(id, userId);

            if (project == null)
            {
                throw new ArgumentException($"Project with id {id} not found");
            }

            project.PaletteId = request.PaletteId;

            await _projectCommandRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectCommandRepository.DeleteProjectAsync(id);
        }
    }
}