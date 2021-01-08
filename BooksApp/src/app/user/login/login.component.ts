import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from '../../_services/auth.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};
  constructor(public router: Router, private authService: AuthService, private toastr: ToastrService) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') !== null ){
      this.router.navigate(['/home']);
    }
  }

  login(){
    this.authService.login(this.model).subscribe(
      ()=>{
        this.router.navigate(['/home']);
      },
      error => {
        this.toastr.error('Usuário ou senha incorretos!');
      }
    )
  }
}
