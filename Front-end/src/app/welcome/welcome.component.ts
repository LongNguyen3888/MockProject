import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {
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
  OnClickLogin(): void {
    this.router.navigateByUrl('login')
  }
  OnClickSignup(): void {
    this.router.navigateByUrl('signup')
  }
  
}
