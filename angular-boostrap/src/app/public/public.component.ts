import { Book } from './../models/book';
import { Component, OnInit } from '@angular/core';
import { PublicService } from './service/public.service';
import { debug } from 'util';

@Component({
  selector: 'app-public',
  templateUrl: './public.component.html',
  styleUrls: ['./public.component.css']
})
export class PublicComponent implements OnInit {

  //bookList: Array<Book> = [];
  bookList: Array<any> = [];

  constructor(
    private publicService: PublicService
  ) { }

  ngOnInit() {
    this.publicService.getAllBooks().subscribe(res => {
      console.log(res);
    })
  }
}
