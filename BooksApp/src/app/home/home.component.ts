import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  public logado: boolean;
  ngOnInit(): void {

    // this.logado = localStorage.getItem('token') !== null
    // console.log(this.logado)
  }

}
