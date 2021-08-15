import { Usuario } from './areas/login/models/usuario-model';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PrototipoWebAngular';
  usuario: Usuario;  
  estaLogado: boolean = false;

  constructor(private _toastr: ToastrService,
    private _router: Router) {
      
    //this.receiverLogin(false);
  }

  receiverLogin(usuarioLogado) {
    console.log(usuarioLogado);
    this.usuario = usuarioLogado;
    this.estaLogado = this.usuario.estaLogado;
    
    if(this.usuario.visitante) {
      this.estaLogado = true;
    }

    if (this.estaLogado === false) {
      this._toastr.warning('Login inválido');
    } else {
      this._router.navigate(['dashboard']);
    }
  }
}