import { Component, OnInit } from '@angular/core';

import { GlobalService } from './../commons/services/global.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from './service/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private service: LoginService,
    private global: GlobalService,
    private router: Router
  ) { }

  ngOnInit() {

    this.loginForm = this.formBuilder.group({
      login: ['admin', Validators.required],
      password: ['e8d95a51f3af4a3b134bf6bb680a213a', Validators.required],
    });
  }

  onSubmit() {
    this.service.userLogin(this.loginForm.value).subscribe(response => {
        
        if(response !== undefined){
          this.global.setAuthLogin(response);
          this.router.navigate(['/'])
        }else{
          alert('Login incorreto')
        }
    })
  }
}
