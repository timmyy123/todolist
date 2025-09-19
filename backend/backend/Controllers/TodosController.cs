using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [ApiController]
  [Route("api/todos")]
  public class TodosController : ControllerBase
  {
    private readonly ITodoRepository _repo;

    public TodosController(ITodoRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TodoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
      var items = await _repo.GetAllAsync(ct);
      var response = items.Select(MapToResponse).ToList();
      return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request, CancellationToken ct)
    {
      var title = request?.Title?.Trim() ?? string.Empty;
      if (string.IsNullOrWhiteSpace(title))
      {
        return ValidationProblem(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
          ["title"] = new[] { "Title is required" }
        }));
      }

      var created = await _repo.AddAsync(title, ct);
      var dto = MapToResponse(created);
      return CreatedAtAction(nameof(GetAll), null, dto);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
      var removed = await _repo.DeleteAsync(id, ct);
      if (!removed)
      {
        return NotFound();
      }
      return NoContent();
    }

    private static TodoResponse MapToResponse(Todo model)
    {
      return new TodoResponse
      {
        Id = model.Id,
        Title = model.Title,
        CreatedAt = model.CreatedAt.UtcDateTime
      };
    }
  }
}