import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/post/post';
import { Comment } from 'src/app/post/post-comments/comment';
import { CommentService } from 'src/app/post/post-comments/comment.service';
import { PostService } from 'src/app/post/post.service';
import { AuthService } from 'src/app/shared/auth.service';
import { LikeService } from 'src/app/shared/like/like.service';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.css']
})
export class ProfileInfoComponent implements OnInit {

  posts!: Post[] | null;
  comments!: Comment[] | null;
  likes!: Post[] | null;
  idUser!: number;
  constructor(public authService: AuthService,
    private postService: PostService,
    private commentService: CommentService,
    private likeService : LikeService) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.idUser = Number(this.authService.user.profile.sub);
    this.postService.getPostsByUser(this.idUser).subscribe(
      (response: any) => {
        this.posts = response.body;
        console.log(response.body)
      })
    this.commentService.getCommentsByUser(this.idUser).subscribe(
      (response: any) => {
        this.comments = response.body;
        console.log(response.body)
      })
    this.likeService.getLikedPostsByUser(this.idUser).subscribe(
      (response: any) => {
        this.likes = response.body;
        console.log(response.body)
      })
  }

}
