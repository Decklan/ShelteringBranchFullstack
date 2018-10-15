import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { Blog } from '../../models/Blog';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public blogForm: FormGroup;
  public blog: Blog;

  constructor(private router: Router,
    private blogService: BlogService) { }

  ngOnInit() {
    this.blogForm = new FormGroup ({
      title: new FormControl('', [
        Validators.required,
      ]),
      body: new FormControl('', [
        Validators.required
      ]),
      author: new FormControl('', [
        Validators.required
      ])
    })
  }

  /**
   * Adds blog
   */
  submit() {
    this.blog = new Blog();
    this.blog.title = this.blogForm.controls['title'].value;
    this.blog.body = this.blogForm.controls['body'].value;
    this.blog.author = this.blogForm.controls['author'].value;
    this.blog.creationDate = new Date();
    console.log("Blog: ", this.blog);
    
    // call add blog service
    /*this.blogService.addBlog(this.blog)
      .subscribe(newBlog => {
        // do something probably
      }, err => console.log(err));
    */
  }

  /**
   * Routes back to home page
   */
  toHome() {
    this.router.navigate([`/home`]);
  }
}
