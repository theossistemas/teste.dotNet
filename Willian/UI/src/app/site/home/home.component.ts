import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; 
import { LivroModel } from 'src/app/shared/models/livro.model';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { LivroService } from 'src/app/shared/services/livro.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  isLoadPanelVisible = false;
  listLivro: LivroModel[] = [];
  
  constructor( 
    private router: Router, private authenticationService: AuthenticationService, private livroService: LivroService ) { 
}

  ngOnInit() { 
    this.carregaLista();
  } 
  carregaLista() {
      this.isLoadPanelVisible = true; 
      this.livroService.getAll().subscribe(res => {
      this.isLoadPanelVisible = false; 
      const data = res.conteudo as LivroModel[];
      this.listLivro = data.filter(x => x.ativo == true); 
      },
      error => {
      this.isLoadPanelVisible = false; 
      this.livroService.handleError(error);

      });
  } 
  viewLivro(data) {
    const idLivro: number =  data.id;
    this.router.navigate(['detalheLivro'], { queryParams: { 'idLivro': data.id } } );
}

}
