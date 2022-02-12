import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/shared/auth.service';
import { Post } from '../post';
import { PostService } from '../post.service';
import { Comment, CommentCreation } from './comment';
import { CommentService } from './comment.service';

@Component({
  selector: 'app-post-comments',
  templateUrl: './post-comments.component.html',
  styleUrls: ['./post-comments.component.css']
})
export class PostCommentsComponent implements OnInit {

  post!: Post;
  comment!: CommentCreation;
  comments!: Comment[] | null;
  commentForm!: FormGroup;
  id!: number;
  IdUser = Number(this.authService.user.profile.sub)
  IdPost = Number(this.router.url.split('/')[3]);

  constructor(private postService: PostService,
              private commentService: CommentService,
              public authService: AuthService,
              private route: ActivatedRoute,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    var dateTime = new Date()
    this.commentForm = this.fb.group({
      description: ['', [Validators.required, Validators.maxLength(100)]],
      date: [dateTime],
      IdUser: [this.IdUser],
      IdPost: [this.IdPost]
    })

    this.route.paramMap.subscribe(
      params => {
        this.id = Number(this.route.snapshot.paramMap.get('id'));
      }
    );

    this.getData(this.id);
    this.getComments(this.id);
  }

  getData(id : number) {
    this.postService.getPost(id).subscribe(
      (response: any) => {
        this.post = response;
      })
  }

  getComments(id: number) {
    this.commentService.getComments(id).subscribe(
      (response: any) => {
        this.comments = response.body;
        console.log(response.body)
      })
  }

  submitComment(){
    if (this.commentForm.valid) {
      if (this.commentForm.dirty) {
        const p = { ...this.comment, ...this.commentForm.value };
        this.commentService.addComment(p)
          .subscribe(
            () => {
              this.onSaveComplete()
            })
      }
    }
  }

  onSaveComplete(): void {
    this.commentForm.reset();
  }

}
