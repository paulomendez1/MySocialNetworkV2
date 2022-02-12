import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.css']
})
export class SigninOidcComponent implements OnInit {

  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.authService.userLoaded$.subscribe((userLoaded) => {
      if (userLoaded) {
        this.router.navigate(['/'])
      }
      else {
        if (!environment.production) {
          console.log('An error happened, user was not loaded');
        }
      }
    })
    this.authService.handleCallback();
  }
}


