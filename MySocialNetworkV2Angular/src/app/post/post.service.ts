import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { Post, PostCreation } from "./post";
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  url = environment.apiURL + '/post';

  eTag!: string | null;


  constructor(private http: HttpClient) { }

  getPosts(searchQuery: string, pageNumber: number, pageSize: number): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    var params = new HttpParams();
    params = params
    .set("PageNumber", pageNumber)
    .set("PageSize", pageSize)
    if (searchQuery) {
      params = params
        .set("searchQuery", searchQuery)
    }
    return this.http.get<any>(this.url, { headers: headers, params, observe: "response" })
      .pipe(
        tap(data => this.eTag = data.headers.get("ETag"))
      );
  }

  getPostsByUser(id : Number): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    }
    return this.http.get<Post[]>(`${this.url}/profile/${id}/posts`, { headers: headers, observe: "response" })
      .pipe(
        tap(data => this.eTag = data.headers.get("ETag"))
      );
  }

  getPost(id: number): Observable<Post> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    }
    return this.http.get<Post>(`${this.url}/${id}`, { headers })
  } 
  

  addPost(post: PostCreation): Observable<PostCreation> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }
    return this.http.post<PostCreation>(this.url, post, { headers: headers })
  }
}