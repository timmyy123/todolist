using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
  public interface ITodoRepository
  {
    Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Todo> AddAsync(string title, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  }
} 