import { LoginService } from './services/login.service';
import { Component } from '@angular/core';
import { Usuario } from './models/usuario';
import { SharedService } from './services/shared.service';

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  usuario: any = new Usuario();
  loginError: any;

  constructor(private _loginService: LoginService, private sharedService: SharedService) {

    const userStrg = localStorage.getItem('usuario');
    if (userStrg)
    {
      //para garantir a renovação do token
      this.usuario = JSON.parse(userStrg);
      this.confirmLogin();
    }
  }

  login() {
    $('#modalLogin').modal('show');
    $('#modalLogin').on('hidden.bs.modal', () => {
      this.loginError = '';
    });
  }

  confirmLogin() {
    this.loginError = '';
    this._loginService.login(this.usuario).subscribe(data => {

      debugger
      if (data.authenticated) {
        debugger
        this.usuario.authenticated = data.authenticated;
        this.usuario.accessToken = data.accessToken;
        localStorage.setItem('usuario', JSON.stringify(this.usuario));
        $('#modalLogin').modal('hide');
        this.sharedService.callEventLogin.next(true);
      }
      else
      {
        this.loginError = data.message;
        this.usuario = {};
        this.sharedService.callEventLogin.next(false);
      }

    },error=>{
      debugger
      this.usuario = {};
      this.sharedService.callEventLogin.next(false);
    })
  }

  sair() {
    localStorage.removeItem('usuario');
    this.usuario = {};
    this.sharedService.callEventLogin.next(false);
  }
}
