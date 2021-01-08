import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public router: Router, private authService: AuthService) { }

  ngOnInit(): void {
   this.isLoggedIn()
  }

  isLoggedIn(){
    return this.authService.isLoggedIn();
  }

  entrar(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login'])
  }
  logout(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login'])
  }

  getNome(){

    return sessionStorage.getItem('username')
  }
  allowed() {
    return sessionStorage.getItem('role') == "admin" ? true : false;
  }


}
