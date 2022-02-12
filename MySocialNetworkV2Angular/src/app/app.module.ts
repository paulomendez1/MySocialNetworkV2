import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { PostIndexComponent } from './post/post-index/post-index.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/material.module';
import { AppRoutingModule } from './app-routing.module';
import { AddAuthorizationHeaderInterceptor } from './shared/http.interceptor';
import { AuthService } from './shared/auth.service';
import { RequireAuthenticatedUserRouteGuard } from './shared/require.admin.guard';
import { HomeComponent } from './home/home.component';
import { PostInputComponent } from './post/post-input/post-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MiniProfileComponent } from './profile/mini-profile/mini-profile.component';
import { ProfileInfoComponent } from './profile/profile-info/profile-info.component';
import { PostCommentsComponent } from './post/post-comments/post-comments.component';
import { LikeComponent } from './shared/like/like.component';


@NgModule({
  declarations: [
    AppComponent,
    PostIndexComponent,
    HomeComponent,
    PostInputComponent,
    MiniProfileComponent,
    ProfileInfoComponent,
    PostCommentsComponent,
    LikeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAuthorizationHeaderInterceptor,
      multi: true
    },
    AuthService, RequireAuthenticatedUserRouteGuard, Title
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
