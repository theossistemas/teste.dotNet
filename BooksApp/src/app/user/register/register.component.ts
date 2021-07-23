import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {User} from '../../_models/user';
import {AuthService} from '../../_services/auth.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  user: User;

  constructor(public fb: FormBuilder, private authService: AuthService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.validation();
  }

  validation(){
    this.registerForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      nomeUsuario: ['', Validators.required],
      role: ['',Validators.required],
      passwords: this.fb.group({
        senha: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword:['', Validators.required]
      }, {validator: this.compararSenhas })

    })
  }

  compararSenhas(fb: FormGroup){

    const confirmarSenhaCtrl = fb.get('confirmPassword');

    if(confirmarSenhaCtrl.errors === null || 'mismatch' in confirmarSenhaCtrl.errors){
      if(fb.get('senha').value!==confirmarSenhaCtrl.value){
        confirmarSenhaCtrl.setErrors({ mismatch: true });
      }else{
        confirmarSenhaCtrl.setErrors(null)
      }
    }
  }

  cadastrarUsuario(){
    if(this.registerForm.valid){
      this.user = Object.assign({senha: this.registerForm.get('passwords.senha').value},this.registerForm.value);

      console.log(this.user)
      console.log(this.registerForm.value)
      this.authService.register(this.user).subscribe(
        //se tiver sucess
        ()=>{
          this.router.navigate(['/user/login']);
          this.toastr.success('Cadastrado realizado!')
        },
        //se não tiver sucess
        error => {

          this.toastr.error(error.error);
        }
      )
    }
  }

}
