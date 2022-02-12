import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "./auth.service";

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }
    
    intercept(request: HttpRequest<any>, next: HttpHandler):
        Observable<HttpEvent<any>> {
        if (this.authService.user != null) {
            let tokenType = this.authService.user!.token_type;
            let accessToken = this.authService.user!.access_token;
            // add the access token as bearer token
            request = request.clone(
                {
                    setHeaders: {
                        Authorization: this.authService.user!.token_type
                            + " " + this.authService.user!.access_token
                    }
                });
            return next.handle(request);
        }
        else {
            request = request.clone({ });
            return next.handle(request);
        }
    }
}