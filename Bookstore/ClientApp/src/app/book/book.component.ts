import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import * as bookService from './book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  constructor(private BookService: bookService.BookService) {
  }

  data: any;  
  BookForm: FormGroup;
  EventValue = "Save";
  submitted = false;


  ngOnInit() {
    this.getdata();

    this.BookForm = new FormGroup({
      id: new FormControl(null),
      name: new FormControl("", [Validators.required]),
      isbn: new FormControl("", [Validators.required]),      
    })
  }

  getdata() {
    this.BookService.getData().subscribe((data: any[]) => {
      this.data = data;
    })
  } 

  Save() {
    this.submitted = true;

    if (this.BookForm.invalid) {
      return;
    }

    this.BookService.postData(this.BookForm.value).subscribe((data: any[]) => {
      this.data = data;
      this.resetFrom();

    })
  }

  deleteData(id) {
    this.BookService.deleteData(id).subscribe((data: any[]) => {
      this.data = data;
      this.getdata();
    })
  }

  Update() {
    this.submitted = true;

    if (this.BookForm.invalid) {
      return;
    }
    this.BookService.putData(this.BookForm.value.id, this.BookForm.value).subscribe((data: any[]) => {
      this.data = data;
      this.resetFrom();
    })
  }

  EditData(Data) {
    this.BookForm.controls["id"].setValue(Data.id);
    this.BookForm.controls["name"].setValue(Data.name);
    this.BookForm.controls["isbn"].setValue(Data.isbn);
    this.EventValue = "Update";
  }

  resetFrom() {
    this.getdata();
    this.BookForm.reset();
    this.EventValue = "Save";
    this.submitted = false;
  } 
}
