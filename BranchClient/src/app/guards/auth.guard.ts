import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router,
    private userService: UserService) {}

  canActivate(): boolean {
    
      let token = localStorage.getItem('jwt');

      if (token != null)
        return true;
      else {
        this.userService.logout();
        this.router.navigate(['home']);
        return false;
      }
  }
}
