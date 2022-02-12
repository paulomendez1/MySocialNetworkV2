import { Component } from '@angular/core';
import { AuthService } from './shared/auth.service';
import { faUsers, faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MySocialNetworkV2Angular';

  faUsers = faUsers;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  arrowLeft = true;
  appTitle='';
  userName!:string | undefined;
  

  constructor(private authService: AuthService, private router: Router,
    private activatedRoute: ActivatedRoute,
    public titleService: Title) { }

    ngOnInit() {
      this.router.events.pipe(
        filter(event => event instanceof NavigationEnd),
      ).subscribe(() => {
        const childRoute = this.getChild(this.activatedRoute);
        childRoute.data.subscribe((data: { title: string; }) => {
          this.titleService.setTitle(`${data.title} | MySocialNetwork`)
          this.appTitle = data.title
        });
      });
    }
    getChild(activatedRoute: ActivatedRoute) : any {
      if (activatedRoute.firstChild) {
        return this.getChild(activatedRoute.firstChild);
      }
      else {
        return activatedRoute;
      }
    }

    onFilterTextChanged(filterText: string) {
      if(filterText == ''){
        this.authService.searchQuery = '';

      } 
      else{
        this.authService.searchQuery = filterText;
      }
    }

    reloadCurrentPage(){
      window.location.reload();
    }
  
  }
