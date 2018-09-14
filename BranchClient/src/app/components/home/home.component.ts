import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Blog } from '../../models/Blog';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public blogs: Blog[];

  constructor(private router: Router,
    private blogService: BlogService) { }

  ngOnInit() {
    /*this.blogService.getAllBlogs()
      .subscribe( blogs => {
        this.blogs = blogs;
      }, err => console.log(err));
    */
  }

  toDashboard() {
    this.router.navigate([`/dashboard`]);
  }
}
