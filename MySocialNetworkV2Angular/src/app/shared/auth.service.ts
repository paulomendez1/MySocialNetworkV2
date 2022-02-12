import { Injectable } from '@angular/core';
import { SignoutResponse, User, UserManager } from 'oidc-client';
import { ReplaySubject, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthService {

  private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
  private currentUser!: User | null;
  private loginChangedSubject = new Subject<boolean>();
  public searchQuery!:string;
  loginChanged = this.loginChangedSubject.asObservable();
  userLoaded$ = new ReplaySubject<boolean>(1);
  redirectTo!: string;

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  userName!: string | undefined;

  get user(): User | any{
    if (this.currentUser != null) {
      return this.currentUser;
    }
  }

  


  constructor() {
    this.userManager.clearStaleState();
    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded', user);
        this.userName = user.profile.given_name;
      }
      this.currentUser = user;
      this.userLoaded$.next(true);
    })

    this.userManager.events.addUserUnloaded(() => {
      if (!environment.production) {
        console.log('User Unloaded');
      }
      this.currentUser = null;
      this.userLoaded$.next(false);
    })
  }

  isLoggedIn(): Promise<boolean> {
    return this.userManager.getUser().then(user => {
      const userCurrent = !!user && !!user.expired;
      if (this.currentUser !== user) {
        this.loginChangedSubject.next(userCurrent)
      }
      this.currentUser = user;
      return userCurrent;
    })
  }

  triggerSignIn() {
    this.userManager.signinRedirect().then(function () {
      if (!environment.production) {
        console.log('Redirection to signin triggered.');
      }
    });

  }

  handleCallback() {
    this.userManager.signinRedirectCallback(window.location.href).then(function (user) {
      if (!environment.production) {
        console.log('Callback after signin handled.', user);
      }
    });
  }
  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(function (user) {
      if (!environment.production) {
        console.log('Callback after signin.', user)
      }
    })
  }

  logout() {
    this.userManager.signoutRedirect().then(function (resp) {
      if (!environment.production) {
        console.log('Redirection to sign out triggered', resp)
      }
    })
  }

  completeLogout(): Promise<any>{

    return this.userManager.signoutRedirectCallback()
        .then((user) =>
        {                  
            return this.userManager.clearStaleState();
       });                 
}

  getAccessToken() {
    return this.userManager.getUser().then(user => {
      if (!!user && !user.expired) {
        return user.access_token;
      }
      else {
        return null;
      }
    })
  }

}
