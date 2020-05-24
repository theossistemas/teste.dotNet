import { Injectable, Inject, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Observable } from "rxjs";
import { Book } from "src/app/models/book";
import { BookResponse } from "src/app/models/BookResponse";

export class BookService implements OnInit {
  public books: Book[];
  private _baseUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this._baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.books = [];
  }

  get headers(): HttpHeaders {
    return new HttpHeaders()
      .set('content-type', 'application/json');
  }

  public listAll(page: number): Observable<BookResponse> {
    return this.http.get<BookResponse>(`http://localhost:5000/api/book?page=${page}&qty=10`);
  }
}
