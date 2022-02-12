import { Component, OnInit } from '@angular/core';
import { PostCreation } from '../post';
import { formatDate } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PostService } from '../post.service';
import { AuthService } from 'src/app/shared/auth.service';

@Component({
  selector: 'app-post-input',
  templateUrl: './post-input.component.html',
  styleUrls: ['./post-input.component.css']
})
export class PostInputComponent implements OnInit {

  post!: PostCreation;
  postForm!: FormGroup;
  IdUser = 1;

  constructor(private fb: FormBuilder,
    private postService: PostService,
    public authService : AuthService) { }

  ngOnInit(): void {
    var dateTime = new Date()
    this.postForm = this.fb.group({
      description: ['', [Validators.required, Validators.maxLength(100)]],
      date: [dateTime],
      image: [''],
      IdUser: [this.IdUser]
    })
  }

  submitPost() {
    if (this.postForm.valid) {
      if (this.postForm.dirty) {
        const p = { ...this.post, ...this.postForm.value };
        this.postService.addPost(p)
          .subscribe(
            () => {
              this.onSaveComplete()
            })
      }
    }
  }

  onSaveComplete(): void {
    this.postForm.reset();
    window.location.reload();
  }

}
