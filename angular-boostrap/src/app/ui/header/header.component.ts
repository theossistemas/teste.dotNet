import { GlobalService } from './../../commons/services/global.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private globalService : GlobalService
  ) { }

  ngOnInit() {
  }

  showActionLogin() {
    const valid = this.globalService.userLogged();
    return valid;
  }

  showNameUser(){
    return this.globalService.getAuthLogin().name;
  }

}
