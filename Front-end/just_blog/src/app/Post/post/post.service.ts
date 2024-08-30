import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class PostService {

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
  // SearchPost/byTitle
  SearchPost(search:string) {
    const url = this.baseUrl + `api/SearchPost/byTitle/${search}`
    const httpHeader = this.getHttpHeaders();
    return this.http.get<any>(url, { headers: httpHeader });
  }
  GetPostDetail(id:string) {
    const url = this.baseUrl + `api/Post/${id}`
    const httpHeader = this.getHttpHeaders();
    return this.http.get<any>(url, { headers: httpHeader });
  }
  GetPost(index:number,page_size:number) {
    const url = this.baseUrl + `api/Post?pageIndex=${index}&pageSize=${page_size}`
    const httpHeader = this.getHttpHeaders();
    return this.http.get<any>(url, { headers: httpHeader });
  }
  CreatedPost(payload:any)
  {
    const url = this.baseUrl + `api/Post`
    const httpHeader = this.getHttpHeaders();
    return this.http.post<any>(url,payload, { headers: httpHeader });
  }
}
