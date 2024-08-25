import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
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
