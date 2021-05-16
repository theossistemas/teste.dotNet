import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  items: MenuItem[];
  activeItem: MenuItem;
  @Input() index: number;

  constructor() { }

  ngOnInit(): void {
    this.items = [
      { label: '', icon: 'pi pi-fw pi-home', routerLink: ['/main/home'] },
      { label: 'Usuários', icon: 'pi pi-fw pi-users', routerLink: ['/main/users'] },
    ];

    this.activeItem = this.items[this.index];
  }

}
