import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

type LoginForm = {
  email: FormControl<string>;
  password: FormControl<string>;
};

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h2>Login</h2>
    <form [formGroup]="form" (ngSubmit)="entrar()">
      <input type="email" formControlName="email" placeholder="Email" />
      <input type="password" formControlName="password" placeholder="Senha" />
      <button type="submit" [disabled]="form.invalid || loading">Entrar</button>
      <p *ngIf="erro" style="color:crimson">{{ erro }}</p>
    </form>
  `
})
export class LoginComponent {
  loading = false;
  erro = '';
  form!: FormGroup<LoginForm>;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.form = this.fb.group({
      email: this.fb.nonNullable.control('', [Validators.required, Validators.email]),
      password: this.fb.nonNullable.control('', [Validators.required])
    });
  }

  entrar() {
    if (this.form.invalid) return;
    this.loading = true;
    this.erro = '';

    const { email, password } = this.form.getRawValue();

    this.auth.login({ email: email.trim(), password }).subscribe({
      next: (res) => {
        this.auth.saveTokenFromResponse(res);
        this.router.navigateByUrl('/livros');
      },
      error: (e) => {
        const msg = e?.error?.detail || e?.error?.title || 'Falha no login (401). Verifique email/senha.';
        this.erro = msg;
        this.loading = false;
        console.warn('Login 401:', e?.error);
      },
      complete: () => (this.loading = false)
    });
  }
}
// componente p/ login de usuário (email/senha) e salvar token JWT