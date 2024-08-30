import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl=environment.HOST_API;
  constructor(private http: HttpClient) { }

  getHttpHeaders() {

    const token = localStorage.getItem("token")

    // console.log('auth.token',auth.access_token)
    let result = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token,
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type'
    });
    return result;
  }
  GetCategory() {
    const url = this.baseUrl + 'api/Category'
    const httpHeader = this.getHttpHeaders();
    return this.http.get<any>(url, { headers: httpHeader });
  }
}
