import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RequireAuthenticatedUserRouteGuard } from './shared/require.admin.guard';
import { SigninOidcComponent } from './shared/signin-oidc/signin-oidc.component';
import { HomeComponent } from './home/home.component';
import { ProfileInfoComponent } from './profile/profile-info/profile-info.component';
import { SignoutOidcComponent } from './shared/signout-oidc/signout-oidc.component';
import { PostCommentsComponent } from './post/post-comments/post-comments.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent,  data : {title:'Home'}, canActivate: [RequireAuthenticatedUserRouteGuard]},
  {path: 'profile', component: ProfileInfoComponent,  data : {title:'Profile'}, canActivate: [RequireAuthenticatedUserRouteGuard]},
  {path: 'home/post/:id', component: PostCommentsComponent,  data : {title:'Comments'}, canActivate: [RequireAuthenticatedUserRouteGuard]},
  {path: 'signin-oidc', component: SigninOidcComponent},
  {path: 'signout-oidc', component: SignoutOidcComponent},

  {path: '**', redirectTo: 'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
