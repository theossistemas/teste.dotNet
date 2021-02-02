import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'; 
import { LivroModel } from 'src/app/shared/models/livro.model'; 
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { LivroService } from 'src/app/shared/services/livro.service';
@Component({
  selector: 'app-detalhe-livro',
  templateUrl: './detalheLivro.component.html',
  styleUrls: ['./detalheLivro.component.scss']
})
export class DetalheLivroComponent implements OnInit {
  isLoadPanelVisible = false; 
  idLivro: number;
  livro: LivroModel = new LivroModel(); 
  constructor( 
    private router: Router, private activatedRoute: ActivatedRoute, private authenticationService: AuthenticationService, private livroService: LivroService ) { 
}

  ngOnInit() { 
    this.activatedRoute.queryParams.subscribe(params => {
      this.idLivro = params['idLivro']; 
      this.getLivro();
    },
    error => {
        this.livroService.handleError(error);
        this.isLoadPanelVisible = false; 
    }); 
  } 

  getLivro() {
    this.isLoadPanelVisible = true; 
    this.livroService.get(this.idLivro).subscribe(res => {
      this.isLoadPanelVisible = false;  
      this.livro = res.conteudo as LivroModel;
  },
  error => {
      this.isLoadPanelVisible = false; 
      this.livroService.handleError(error);

  });
} 


}
