import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent, MessageService } from 'primeng/api';
import { Account } from 'src/app/shared/models/account.model';
import { AccountInsert } from 'src/app/shared/models/register.model';
import { AccountService } from 'src/app/shared/services/account.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  result: Account[];
  displayedColumns: any[];
  loading: boolean = false;
  totalRecords: number;
  isAdmin: boolean = false;
  display: boolean = false;
  register: AccountInsert;
  form: FormGroup;
  submitted: boolean = false;
  roles: any[];

  constructor(
    private readonly accountService: AccountService,
    private readonly messageService: MessageService,
    private readonly fb: FormBuilder,
  ) {
  }

  ngOnInit(): void {
    this.start();
  }

  start(): void {
    this.displayedColumns = [
      {
        field: 'id',
        header: 'ID',
        pipe: null
      },
      {
        field: 'name',
        header: 'Nome',
        pipe: null
      },
      {
        field: 'email',
        header: 'E-mail',
        pipe: null
      },
      {
        field: 'role',
        header: 'Perfil',
        pipe: 'role'
      },
      {
        field: 'created',
        header: 'Dt. de Criação',
        pipe: 'humanizeDate'
      },
    ];

    this.roles = [
      {
        label: 'Admin',
        value: 0
      },
      {
        label: 'Usuário',
        value: 1
      }
    ];

    this.reset();
  }

  reset(): void {
    this.totalRecords = 0;
    this.loading = false;
    this.result = [];
    this.isAdmin = this.accountService.isADMIN();
    this.register = new AccountInsert();
    this.display = false;
    this.submitted = false;

    this.getAll();
  }

  onGridLoad(event: LazyLoadEvent): void {
    this.getAll();
  }

  getAll(): void {
    this.loading = true;

    this.accountService.getAll()
      .subscribe(
        (data) => {
          this.result = data;
        },
        (err) => {
          console.error(err);
        },
        () => {
          this.totalRecords = this.result.length;
          this.loading = false;
        }
      );

  }

  showDialog(): void {
    this.display = true;
  }

  save(): void {
    this.submitted = true;

    this.form = this.fb.group({
      name: [this.register.name, [Validators.required]],
      email: [this.register.email, [Validators.required, Validators.email]],
      confirmEmail: [this.register.confirmEmail, [Validators.required, Validators.email]],
      password: [this.register.password, [Validators.required]],
      confirmPassword: [this.register.confirmPassword, [Validators.required]],
      role: [this.register.role, [Validators.required]]
    }, { validators: [this.checkPasswords, this.checkEmails] });

    if (!this.form.invalid) {
      this.accountService.register(this.register)
        .subscribe(
          (data) => {
            this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Operação realizada com sucesso!' });
            this.reset();
          }
        );
    }
  }

  private checkPasswords(group: FormGroup): any {
    const pass = group.get('password').value;
    const confirmPass = group.get('confirmPassword').value;

    return pass === confirmPass ? null : { notSame: true };
  }

  private checkEmails(group: FormGroup): any {
    const email = group.get('email').value;
    const confirmEmail = group.get('confirmEmail').value;

    return email === confirmEmail ? null : { notSameEmail: true };
  }

  public cancel(): void {
    this.reset();
  }
}
