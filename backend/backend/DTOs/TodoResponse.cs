using System;

namespace backend.DTOs
{
  public class TodoResponse
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; };
  }
}