import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CommentCreation } from './comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  url = environment.apiURL + '/post';


  constructor(private http: HttpClient) { }

  getComments(postId : number): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    }
    return this.http.get<Comment[]>(`${this.url}/${postId}/comments`, { headers: headers, observe: "response" })
  }

  getCommentsByUser(id : number): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
    }
    return this.http.get<Comment[]>(`${this.url}/${id}/comments/${id}`, { headers: headers, observe: "response" })
  }


  addComment(comment: CommentCreation): Observable<CommentCreation> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }
    return this.http.post<CommentCreation>(`${this.url}/${comment.IdPost}/comments`, comment, { headers: headers })
  }
}
