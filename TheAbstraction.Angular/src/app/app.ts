import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';

interface Post {
  id: number;
  title: string;
  body: string;
}

@Component({
  selector: 'app-root',
  standalone: false,
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App implements OnInit {
  readonly posts = signal<Post[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  constructor(private readonly http: HttpClient) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.loading.set(true);
    this.error.set('');

    this.http.get<Post[]>('https://jsonplaceholder.typicode.com/posts', {
      cache: 'no-store',
    }).subscribe({
      next: (posts) => {
        this.posts.set(posts.slice(0, 5));
        this.loading.set(false);
      },
      error: (response: HttpErrorResponse) => {
        console.error('Không tải được bài viết từ JSONPlaceholder:', response);
        this.error.set(response.status === 0
          ? 'Không kết nối được API. Hãy kiểm tra mạng hoặc thử lại.'
          : `API trả về lỗi ${response.status}. Hãy thử tải lại.`);
        this.loading.set(false);
      },
    });
  }
}
