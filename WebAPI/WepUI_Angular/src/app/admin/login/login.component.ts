

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';   
import { LoginModel } from 'src/app/shared/models/login.model';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
selector: 'app-login',
templateUrl: './login.component.html',
styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
loginForm: FormGroup;
loading = false;
submitted = false;
returnUrl: string;
isLoginInvalido = false;
msgErro: string;
login: LoginModel = new LoginModel();
    constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
    ) { }

    ngOnInit() {
    this.loginForm = this.formBuilder.group({
    login: ['', Validators.required],
    senha: ['', Validators.required]
    });

    }
 
get form() { return this.loginForm.controls; }

onFormSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
        return;
    }
    this.login.email = this.form.login.value;
    this.login.senha = this.form.senha.value;

    this.authenticationService.login(this.login).subscribe(res  => {
        const conteudo = res.conteudo;
        if (conteudo === null) {
            this.msgErro = 'Senha ou login inválido';
        } else { 
            this.msgErro = '';
            this.router.navigate(['/admin']);
        }
    },
    error => {
        this.authenticationService.handleError(error);
    }); 
}
    
}
