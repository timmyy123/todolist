import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../services/todo.service';
import { Todo } from '../models/todo';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-list.component.html',
  // styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  todos: Todo[] = [];
  newTodoTitle = '';
  isLoading = false;
  error: string | null = null;

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.isLoading = true;
    this.error = null;
    
    this.todoService.getTodos().subscribe({
      next: (todos) => {
        this.todos = todos;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load todos';
        this.isLoading = false;
        console.error('Error loading todos:', err);
      }
    });
  }

  addTodo(): void {
    if (!this.newTodoTitle.trim()) return;

    const request = { title: this.newTodoTitle.trim() };
    this.todoService.addTodo(request).subscribe({
      next: (todo) => {
        this.todos.push(todo);
        this.newTodoTitle = '';
      },
      error: (err) => {
        this.error = 'Failed to add todo';
        console.error('Error adding todo:', err);
      }
    });
  }

  deleteTodo(id: string): void {
    this.todoService.deleteTodo(id).subscribe({
      next: () => {
        this.todos = this.todos.filter(t => t.id !== id);
      },
      error: (err) => {
        this.error = 'Failed to delete todo';
        console.error('Error deleting todo:', err);
      }
    });
  }
}