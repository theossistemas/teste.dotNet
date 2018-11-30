import { Book } from './../models/book';
import { Component, OnInit } from '@angular/core';
import { PublicService } from './service/public.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-public',
  templateUrl: './public.component.html',
  styleUrls: ['./public.component.css']
})
export class PublicComponent implements OnInit {

  bookList: Array<Book> = [];

  public images_url: string;

  constructor(
    private router: Router,
    private publicService: PublicService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.images_url = environment.images_url;
    this.getAll();
  }

  getAll() {
    this.spinner.show();
    this.publicService.getAllBooks().subscribe(res => {
      this.bookList = res;
      setTimeout(() => {
          this.spinner.hide();
      }, 100);
    });
  }

  editar(id) {
    this.toastr.info('Buscando informacoes');
    this.router.navigate(['/register', id]);
  }

  remover(id) {
    const r = confirm('Deseja remover');
      if (r === true) {
        this.publicService.removeBook(id).subscribe(res => {
          this.toastr.success('Removido com sucesso');
          this.getAll();
        });
      } else {

      }
  }

}
