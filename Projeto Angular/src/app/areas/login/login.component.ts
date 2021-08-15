import { Usuario } from './models/usuario-model';
import { Component, OnInit, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'sin-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login: FormGroup;
  admin: boolean = false;

  @Output() respostaLogin = new EventEmitter();
  
  constructor(private _formBuilder: FormBuilder, private _router: Router) {     
  }

  getUrl()
{
  return "src('/image1.jpg')";
}
  ngOnInit() {
    this.createForm();
   
  }
  
  createForm() {
    this.login = this._formBuilder.group({
      matricula: ["",  Validators.required],
      senha: ["",  Validators.required]
    });
  }

  logar(){
    let usuario = new Usuario();
    usuario.estaLogado = true;
    usuario.visitante = false;
    this.respostaLogin.emit(usuario);
  }

  abrirAcervo() {
    let usuario = new Usuario();
    usuario.estaLogado = false;
    usuario.visitante = true;

    this.respostaLogin.emit(usuario);    
  }
}
