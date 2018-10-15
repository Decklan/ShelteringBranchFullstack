import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { User } from '../models/User';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private authBase: string = 'auth/login'; // For login purposes
  private apiBase: string = 'user'; // api addition to endpoint
  private currentUser: User; // Currently logged in user

  constructor(private http: HttpClient) { }

  /**
   * Logs in a user
   */
  login(email: string, password: string): Observable<any> {
    const endpoint: string = `${environment.baseServerUrl}/${this.authBase}`;
    const loginResource = { email, password };
    return this.http.post<any>(endpoint, loginResource);
  }

  /**
   * Retrieves a user profile by their email address
   * @param email The email for the user we are retrieving
   */
  getUserByEmail(email: string): Observable<User> {
    const endpoint: string = `${environment.baseServerUrl}/${this.apiBase}/${email}`;
    return this.http.get<User>(endpoint);
  }

  /**
   * Sets JWT to localStorage
   */
  setSession(token) {
    localStorage.setItem('jwt', token);
  }

  /**
   * Sets first and last name to local storage for user
   * @param firstName First name of user
   * @param lastName Last name of user
   */
  setUser(firstName: string, lastName: string) {
    localStorage.setItem('first', firstName);
    localStorage.setItem('last', lastName);
  }

  /**
   * Clear local storage information when the user logs out
   */
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('first');
    localStorage.removeItem('last');
  }
}
