import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/shared/models/book.model';
import { LazyLoadEvent, MessageService } from 'primeng/api';
import { BookService } from 'src/app/shared/services/book.service';
import { AccountService } from 'src/app/shared/services/account.service';
import { AppAccess } from 'src/app/helpers/constants';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import uuid from 'uuid';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  result: Book[];
  displayedColumns: any[];
  loading: boolean = false;
  totalRecords: number;
  isAdmin: boolean = false;
  display: boolean = false;
  register: Book;
  form: FormGroup;
  submitted: boolean = false;

  constructor(
    private readonly bookService: BookService,
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
        field: 'title',
        header: 'Título',
        pipe: null
      },
      {
        field: 'created',
        header: 'Dt. de Criação',
        pipe: 'humanizeDate'
      },
    ];

    this.reset();
  }

  reset(): void {
    this.totalRecords = 0;
    this.loading = false;
    this.result = [];
    this.isAdmin = this.accountService.isADMIN();
    this.register = new Book();
    this.display = false;
    this.submitted = false;

    this.getAll();
  }

  onGridLoad(event: LazyLoadEvent): void {
    this.getAll();
  }

  edit(id: uuid): void {
    this.register = this.result.find(e => e.id === id);

    this.showDialog();
  }

  delete(id: uuid): void {
    this.bookService.delete(id)
      .subscribe(
        (data) => { },
        (err) => { },
        () => {
          this.reset();
        }
      );
  }

  getAll(): void {
    this.loading = true;

    this.bookService.getAll()
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
      title: [this.register.title, [Validators.required]]
    });

    if (!this.form.invalid) {
      console.log(this.register);

      if (this.register.id) {
        this.bookService.edit(this.register.id, this.register)
          .subscribe(
            (data) => { },
            (err) => { },
            () => {
              this.reset();
            }
          );
      } else {
        this.bookService.create(this.register)
          .subscribe(
            (data) => {
              this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Operação realizada com sucesso!' });
              this.reset();
            }
          );
      }
    }
  }

  public cancel(): void {
    this.reset();
  }
}
