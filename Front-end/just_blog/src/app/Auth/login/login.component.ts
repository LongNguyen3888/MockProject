import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,
    RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private authen_services = inject(AuthService)
  private router = inject(Router)
  username!: string;
  pass!: string;
  Submit() {
    let item =
    {
      "userName": this.username,
      "password": this.pass
    }
      this.authen_services.Login(item).subscribe(res => {
        console.log("ress", res);
        if (res && !_.isEmpty(res.token)) {
          localStorage.setItem("token",res.token)
          localStorage.setItem("userInformation",res.userInformation)
          this.router.navigateByUrl('home')
        }
        else
        {
          alert("Tài khoản hoăc mật khẩu không đúng")
        }


      }
      )



  }
}
