import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public loginForm: FormGroup;

  constructor(private userService: UserService,
    private router: Router) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)])
    });
  }

  /**
   * Captures form information and attempts to log in the
   * user
   */
  login() {
    const email: string = this.loginForm.controls['email'].value;
    const password: string = this.loginForm.controls['password'].value;
    console.log(email, password);
    this.userService.login(email, password)
      .subscribe((token) => {
        this.userService.setSession(token);
        this.userService.getUserByEmail(email)
          .subscribe((user: User) => {
            this.userService.setUser(user.firstName, user.lastName);
            this.router.navigate(['dashboard']);
          }, (err) => { console.log(err) });
      }, (err) => { console.log(err) });
  }

}
