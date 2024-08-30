import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth.service';
import _ from 'lodash';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,
    RouterModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private authen_services = inject(AuthService)
  private router = inject(Router)
  confirmpass!: string;
  pass!: string
  username!: string
  Submit() {
    if(this.confirmpass==this.pass)
    {

    let item = {
      userName: this.username,
      password: this.pass

    }
    this.authen_services.Register(item).subscribe(res=>
    {
      console.log("Register",res);
      if(res&&!_.isEmpty(res.token))
      {
        localStorage.setItem("token",res.token)
        this.router.navigateByUrl("home")
      }
      
    }
    )
  }
  else{
    alert("Confirm pass không chính xác !")
  }
  }

}
