import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl=environment.HOST_API;
  constructor(private http: HttpClient) { }
  Login(body:any) {
    const url = this.baseUrl + `api/Auth/login`;
    return this.http.post<any>(url,body);

  }
  Register(body:any) {
    const url = this.baseUrl + `api/Auth/register`;
    return this.http.post<any>(url,body);

  }
}
