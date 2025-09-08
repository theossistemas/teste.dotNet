import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import "primeicons/primeicons.css";
import { LivrariaListComponent } from "./components/livraria-list/livraria-list.component";
import { Router } from '@angular/router';
import { SidebarComponent } from "./components/sidebar/sidebar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, LivrariaListComponent, SidebarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Livraria';

  constructor(private router: Router) {}

  isLoginRoute(): boolean {
    return this.router.url === '/login';
  }
}
