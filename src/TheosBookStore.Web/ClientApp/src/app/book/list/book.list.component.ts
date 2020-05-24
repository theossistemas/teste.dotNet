import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book/book.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book.list',
  templateUrl: './book.list.component.html',
  styleUrls: ['./book.list.component.css']
})
export class BookListComponent implements OnInit {
  public _books: Book[];

  constructor(
    private _bookService: BookService,
    private _router: Router
  ) {
    this.gotoPage(1);
  }

  public gotoPage(page: number) {
    this._bookService.listAll(page)
      .subscribe(
        response => {
          this._books = response.data;
        },
        errors => {
          console.log(errors.error);
        });
  }

  public edit(book: Book) {
    alert('If you hire me...');
  }

  public delete(book: Book) {
    alert('If you hire me...')
  }

  ngOnInit() { }

}
