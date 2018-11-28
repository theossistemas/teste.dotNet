import { Book } from './../models/book';
import { Component, OnInit } from '@angular/core';
import { PublicService } from './service/public.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-public',
  templateUrl: './public.component.html',
  styleUrls: ['./public.component.css']
})
export class PublicComponent implements OnInit {

  bookList: Array<Book> = [];

  constructor(
    private router: Router,
    private publicService: PublicService
  ) { }

  ngOnInit() {
    this.getAll();
  }

  getAll(){
    this.publicService.getAllBooks().subscribe(res => {
      this.bookList = res;
    })
  }

  editar(id){
    this.router.navigate(['/register', id])
  }

  remover(id){
    var r = confirm("Deseja remover");
      if (r == true) {
        this.publicService.removeBook(id).subscribe(res => {
          alert('removido com sucesso')!
          this.getAll();
        })
      } else {
          
      }
  }

}
