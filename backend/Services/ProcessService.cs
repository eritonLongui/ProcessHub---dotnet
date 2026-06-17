using ProcessHub.Entities;
using ProcessHub.Repositories.Interfaces;
using ProcessHub.Services.Interfaces;
using ProcessHub.Enums;
using ProcessHub.Data;
using ProcessHub.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProcessHub.DTOs.Process;
using ProcessHub.DTOs.Common;

namespace ProcessHub.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly AppDbContext _context;

        public ProcessService(
            IProcessRepository processRepository,
            IRepository<Client> clientRepository,
            AppDbContext context)
        {
            _processRepository = processRepository;
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task<ProcessResponseDto> CreateAsync(string title, string description, Guid clientId)
        {
            // estou repetindo muito esse trecho de código, devo alterá-lo mais para frente

            var client = await _clientRepository.GetByIdAsync(clientId);

            if (client == null)
                throw new NotFoundException("Client not found.");

            // ----------------------------------------------------------------------------

            var process = new Process(title, description, clientId);

            await _processRepository.AddAsync(process);
            await _context.SaveChangesAsync();

            // carregar com include para retornar o cliente completo
            var createdProcess = await _processRepository.GetWithDetailsAsync(process.Id);

            return MapToDto(createdProcess!);
        }

        public async Task UpdateAsync(Guid id, string title, string description)
        {
            var process = await _processRepository.GetByIdAsync(id);

            if (process == null)
                throw new NotFoundException("Process not found.");

            process.Update(title, description);

            _processRepository.Update(process);

            await _context.SaveChangesAsync();
        }

        public async Task AssignUserAsync(Guid processId, Guid userId)
        {
            var process = await _processRepository.GetByIdAsync(processId);

            if (process == null)
                throw new NotFoundException("Process not found.");

            process.AssignUser(userId);

            _processRepository.Update(process);

            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(Guid processId, ProcessStatus newStatus, Guid UserId)
        {
            var process = await _processRepository.GetByIdAsync(processId);

            if (process == null)
                throw new NotFoundException("Process not found.");

            process.ChangeStatus(newStatus, UserId);

            _processRepository.Update(process);

            await _context.SaveChangesAsync();
        }

        public async Task<ProcessResponseDto?> GetByIdAsync(Guid id)
        {
            var process = await _processRepository.GetWithDetailsAsync(id);

            if (process == null)
                return null;

            return MapToDto(process);
        }

        private static ProcessResponseDto MapToDto(Process process)
        {
            return new ProcessResponseDto(
                process.Id,
                process.Title,
                process.Description,
                process.Status.ToString(),
                new ClientSummaryDto(
                    process.Client.Id,
                    process.Client.Name,
                    process.Client.DocumentNumber
                )
            );
        }

        public async Task<IEnumerable<ProcessResponseDto>> GetByClientIdAsync(Guid clientId)
        {
            var processes = await _processRepository.GetByClientIdAsync(clientId);

            var result = new List<ProcessResponseDto>();

            foreach (var process in processes)
            {
                var detailed = await _processRepository.GetWithDetailsAsync(process.Id);
                result.Add(MapToDto(detailed!));
            }

            return result;
        }

        public async Task DeactivateAsync(Guid id)
        {
            var process = await _processRepository.GetByIdAsync(id);

            if (process == null)
                throw new NotFoundException("Process not found.");

            process.Deactivate();

            _processRepository.Update(process);

            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProcessResponseDto>> GetPagedAsync(ProcessFilterDto filter)
        {
            var query = _context.Processes.AsQueryable();

            // 🔹 Filtros
            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(p => p.Title.Contains(filter.Title));

            if (filter.Status.HasValue)
                query = query.Where(p => p.Status == filter.Status.Value);

            if (filter.ClientId.HasValue)
                query = query.Where(p => p.ClientId == filter.ClientId.Value);

            // 🔹 Total antes da paginação
            var totalCount = await query.CountAsync();

            // 🔹 Paginação
            var processes = await query
                .Include(p => p.Client)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // 🔹 Mapping
            var data = processes.Select(MapToDto);

            return new PagedResult<ProcessResponseDto>
            {
                Data = data,
                TotalCount = totalCount
            };
        }
    }
}
