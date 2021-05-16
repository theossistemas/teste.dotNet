import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  form: FormGroup;
  submitted: boolean = false;

  constructor(
    private readonly accountService: AccountService,
    private readonly router: Router,
    private readonly fb: FormBuilder,
  ) { }

  ngOnInit(): void {
  }

  public login(): void {
    this.submitted = true;

    this.form = this.fb.group({
      username: [this.username, [Validators.required, Validators.email]],
      password: [this.password, [Validators.required]]
    });

    if (!this.form.invalid) {
      this.accountService.login(this.username, this.password)
        .subscribe(
          (data) => {
            this.router.navigate(['/main/home']);
          }
        );
    }
  }

}
