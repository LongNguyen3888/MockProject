import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import * as _ from 'lodash';
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }
  canActivate(): boolean {
    const token = localStorage.getItem("token")
    if (!_.isEmpty(token)) {
      return true
    }
    this.router.navigateByUrl("/login")
    return false;
  }

}