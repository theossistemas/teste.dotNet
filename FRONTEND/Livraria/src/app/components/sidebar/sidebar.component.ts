import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { SidebarModule } from 'primeng/sidebar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [SidebarModule, ButtonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  sidebarVisible: boolean = false;
  constructor(private router: Router) {}

  login() {
    this.sidebarVisible = false;
    this.router.navigate(['/login']);
  }

  logout() {
    localStorage.removeItem('token');
    this.sidebarVisible = false;
    this.router.navigate(['/login']);
  }
}
