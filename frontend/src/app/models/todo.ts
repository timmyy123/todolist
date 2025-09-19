export interface Todo {
  id: string;
  title: string;
  createdAt: string;
}

export interface CreateTodoRequest {
  title: string;
}