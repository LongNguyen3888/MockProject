import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blogview',
  standalone: true,
  imports: [],
  templateUrl: './blogview.component.html',
  styleUrl: './blogview.component.css'
})
export class BlogviewComponent {
  constructor (private router: Router) {}
  OnClickWebname(): void {
    this.router.navigateByUrl('')
  }
  OnClickHome(): void {
    this.router.navigateByUrl('home')
  }
  OnClickPosts(): void {
    this.router.navigateByUrl('posts')
  }
  OnClickAboutus(): void {
    this.router.navigateByUrl('aboutus')
  }
  OnClickContact(): void {
    this.router.navigateByUrl('contact')
  }
}
