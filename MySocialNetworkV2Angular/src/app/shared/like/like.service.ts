import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from 'src/app/post/post';
import { environment } from 'src/environments/environment';
import { LikeCreation } from './like';

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  url = environment.apiURL + '/like';

  constructor(private http: HttpClient) { }

    addLike(idUser: number, postId : number): Observable<any> {
      const like: LikeCreation = {
        idUser: idUser,
        idPost: postId,
      };

      const headers = {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      }
      return this.http.post<any>(this.url, like, { headers: headers })
    }

    deleteLike(idUser: number, postId : number) {
      var params = new HttpParams();
      params = params
      .set("IdUser", idUser)
      .set("IdPost", postId)
      const headers = {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      }
      return this.http.delete(this.url, {headers: headers, params})
    }

    getLikedPostsByUser(id : Number): Observable<any> {
      const headers = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      }
      return this.http.get<Post[]>(`${environment.apiURL+'/post'}/profile/${id}/likes`, { headers: headers, observe: "response" })
    }
}
