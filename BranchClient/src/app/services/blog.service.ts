import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { Blog } from '../models/Blog';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  private apiBase = '/blog';
  private blogsSubject: BehaviorSubject<Blog[]>;
  private blogsObservable: Observable<Blog[]>;

  constructor(private http: HttpClient) {  
    this.blogsSubject = new BehaviorSubject([]);
    this.blogsObservable = this.blogsSubject.asObservable();
    this.blogsObservable = this.fetchBlogs();
  }

  /**
   * Retrieves all blogs from server
   */
  fetchBlogs(): Observable<Blog[]> {
    const endpoint = `${environment.baseServerUrl}${this.apiBase}`;
    return this.http.get<Blog[]>(endpoint);
  }

  /**
   * Returns all blogs
  */ 
  getAllBlogs(): Observable<Blog[]> {
    return this.blogsObservable;
  }

  /**
   * Adds a blog to the server
   * @param blog New blog to be added
   */
  addBlog(blog: Blog): Observable<Blog> {
    const endpoint = `${environment.baseServerUrl}${this.apiBase}`;
    return this.http.post<Blog>(endpoint, blog);
  }

  /**
   * Requests a blog with specified id
   * @param id Id of blog being requested
   */
  getBlogById(id: number): Observable<Blog> {
    const endpoint = `${environment.baseServerUrl}${this.apiBase}/${id}`;
    return this.http.get<Blog>(endpoint);
  }

  /**
   * Edits existing blog
   * @param blog The blog to be edited
   */
  editBlog(blog: Blog): Observable<Blog> {
    const endpoint = `${environment.baseServerUrl}${this.apiBase}/${blog.id}`;
    return this.http.put<Blog>(endpoint, blog);
  }
}
