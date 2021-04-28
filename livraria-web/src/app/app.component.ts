import { Component } from '@angular/core';
import { TokenStorageService } from './_services/token-storage.service';
import {Router} from '@angular/router'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'livraria-web';

  private roles :string[] = [];
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;
  username: string = "";
  isAdmin =false;

  constructor(private tokenStorageService: TokenStorageService, private router: Router) { }

  ngOnInit(): void {
    this.isLoggedIn = !!this.tokenStorageService.getToken();

    if (this.isLoggedIn) {
      const user = this.tokenStorageService.getUser();
      this.roles = user.roles;
      this.isAdmin = this.tokenStorageService.getUser().role == "Admin" ? true : false;
      this.showAdminBoard = this.roles.includes('ROLE_ADMIN');
      this.showModeratorBoard = this.roles.includes('ROLE_MODERATOR');

      this.username = user.username;
    }else{
      console.log("sem token");
      this.reloadPage();
    }
  }

  logout(): void {
    this.tokenStorageService.signOut();
    window.location.reload();    
  }
  reloadPage(): void {
    
    this.router.navigate(['/login']);
    
  }
}
