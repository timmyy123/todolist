using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
  public class InMemoryTodoRepository : ITodoRepository
  {
    private readonly ConcurrentDictionary<Guid, Todo> _store = new();

    public Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      var list = _store.Values
          .OrderBy(t => t.CreatedAt)
          .ToList()
          .AsReadOnly();
      return Task.FromResult<IReadOnlyList<Todo>>(list);
    }

    public Task<Todo> AddAsync(string title, CancellationToken cancellationToken = default)
    {
      var todo = new Todo { Title = title?.Trim() ?? string.Empty };
      _store[todo.Id] = todo;
      return Task.FromResult(todo);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
      return Task.FromResult(_store.TryRemove(id, out _));
    }
  }
}