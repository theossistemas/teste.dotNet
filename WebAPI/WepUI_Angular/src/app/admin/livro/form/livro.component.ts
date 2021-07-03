import { Component, ElementRef, Input , OnInit, ViewChild } from '@angular/core'; 
import { Subject } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http'; 
import { identifierModuleUrl } from '@angular/compiler'; 
import { LivroModel } from 'src/app/shared/models/livro.model';
import { LivroService } from 'src/app/shared/services/livro.service';  
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ResponseLoginModel } from 'src/app/shared/models/responseLogin.model';

@Component({ 
    selector: 'app-livro',
    templateUrl: './livro.component.html',
    styleUrls: ['./livro.component.scss']
})

export class LivroComponent implements OnInit { 
    request: LivroModel = new LivroModel(); 
    livroForm: FormGroup;  
    title: string;
    isLoadPanelVisible = false; 
    token: ResponseLoginModel;

    constructor(private livroService: LivroService,
                private activatedRoute: ActivatedRoute, 
                private formBuilder: FormBuilder,
                private router: Router, 
                private http: HttpClient,
                private autenticacaoService: AuthenticationService) { 
        }

    ngOnInit() {
        this.createForm();

       this.token = this.autenticacaoService.currentUserValue as ResponseLoginModel;

        this.activatedRoute.queryParams.subscribe(params => {
            this.request.id = params['idLivro']; 
          },
          error => {
              this.livroService.handleError(error);
              this.isLoadPanelVisible = false; 
          });
     
          if (this.request.id != undefined) {
            this.title = 'Alterar';
            this.getLivro();
          } else {
            this.title = 'Incluir';
            this.request.id = 0;
          }
      } 

      
    createForm() {
        this.livroForm = this.formBuilder.group({ 
        titulo: this.formBuilder.control(''),
        descricao: this.formBuilder.control(''),
        autor: this.formBuilder.control(''),
        imagem: this.formBuilder.control(''),
        paginas: this.formBuilder.control(''),
        edicao: this.formBuilder.control(''),
        idioma: this.formBuilder.control(''),
        editora: this.formBuilder.control(''),
        dataPublicacao: this.formBuilder.control(''),
        estoque: this.formBuilder.control('')
        });
    }

    convertForm(livroModel: LivroModel) {

        this.livroForm.controls['titulo'].setValue(livroModel.titulo);
        this.livroForm.controls['descricao'].setValue(livroModel.descricao);
        this.livroForm.controls['autor'].setValue(livroModel.autor);
        this.livroForm.controls['dataPublicacao'].setValue(livroModel.dataPublicacao);
        this.livroForm.controls['estoque'].setValue(livroModel.estoque);

    }

    convertModel() {
 
        this.request.titulo = this.livroForm.controls['titulo'].value;
        this.request.descricao = this.livroForm.controls['descricao'].value;
        this.request.autor = this.livroForm.controls['autor'].value;
        this.request.dataPublicacao = this.livroForm.controls['dataPublicacao'].value;
        this.request.estoque = this.livroForm.controls['estoque'].value; 
        
    }

    getLivro() {
        this.isLoadPanelVisible = true; 
        this.livroService.get(this.request.id).subscribe(res => {
          this.isLoadPanelVisible = false; 
          this.convertForm(res.conteudo as LivroModel);
          this.request = res.conteudo as LivroModel;
      },
      error => {
          this.isLoadPanelVisible = false; 
          this.livroService.handleError(error);
 
      });
    } 

    onFormSubmit() {
        this.isLoadPanelVisible = true;
        this.convertModel();
        this.request.idUsuario = this.token.idUsuario;
    if (this.request.id == 0) {
      this.request.ativo = true; 
        this.livroService.post(this.request).subscribe(resp => {
          this.isLoadPanelVisible = false; 
          this.router.navigate(['listaLivro']);
        },
          error => {
            this.isLoadPanelVisible = false;
            this.livroService.handleError(error); 
        });
    } else {
        this.livroService.put(this.request).subscribe(resp => {
          this.isLoadPanelVisible = false; 
          this.router.navigate(['listaLivro']);
        },
          error => {
            this.isLoadPanelVisible = false;
            this.livroService.handleError(error); 
        });
    } 
    } 
}

