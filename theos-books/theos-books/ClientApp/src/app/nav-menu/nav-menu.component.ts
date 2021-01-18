import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public isAuthenticated: Observable<boolean>;
  public _baseUrl = '';
  constructor(private authorizeService: AuthorizeService, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
