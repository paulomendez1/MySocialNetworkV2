import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared/auth.service';
import { LikeService } from 'src/app/shared/like/like.service';
import { Post } from '../post';
import { CommentService } from '../post-comments/comment.service';
import { PostService } from '../post.service';
import { Pagination } from 'src/app/shared/pagination';
import { PageEvent } from '@angular/material/paginator';


@Component({
  selector: 'app-post-index',
  templateUrl: './post-index.component.html',
  styleUrls: ['./post-index.component.css']
})
export class PostIndexComponent implements OnInit {

  posts!: Post[];
  commentsCount!: number;
  pagination!: Pagination;
  currentPage = 1;
  pageSize = 5;
  idUser!: number;
  totalAmountOfRecords! : number | null;
  
  constructor(private postService: PostService,
    private authService: AuthService,
    public commentService: CommentService,
    private likeService: LikeService) { 
    }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.postService.getPosts(this.authService.searchQuery, this.currentPage, this.pageSize).subscribe(
      (response: any) => {
        this.posts = response.body;
        this.pagination = JSON.parse(response.headers.get("x-pagination") || '{}');
        this.totalAmountOfRecords = this.pagination.totalCount
        console.log(response.body)
      })
  }

  onLiking(postId: number) {
    this.idUser = Number(this.authService.user.profile.sub);
    this.likeService.addLike(this.idUser, postId).subscribe(res => {
      if(res==false){
        this.likeService.deleteLike(this.idUser,postId).subscribe(() => {
          alert('You deleted the like from this post');
          window.location.reload();
        })
      }
      else{
        alert('Your like has been received');
        window.location.reload();
      }
    });
  }

  updatePagination(event: PageEvent){
    this.currentPage= event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getData();
  }
  
}
