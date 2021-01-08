import {Component, OnInit} from '@angular/core';
import {BsModalService} from 'ngx-bootstrap/modal';
import {Router} from '@angular/router';
import {LivroService} from '../../_services/livro.service';
import {ToastrService} from 'ngx-toastr';
import {AuthService} from '../../_services/auth.service';
import {Livro} from '../../_models/livro';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  modoSalvar = 'post';
  registerForm: FormGroup;
  bodyDeleteLivro = '';
  livros: Livro[];
  livro: Livro;

  constructor(private modalService: BsModalService,
              public router: Router,
              private livroService: LivroService,
              private toastr: ToastrService,
              private fb: FormBuilder,
              private authService: AuthService,) {
  }

  ngOnInit(): void {

    //verifica se está logado e a role é admin
    if (!this.isLoggedIn()) {
      this.router.navigate(['/user/login']);
      return;
    }
    this.validation();
    this.carregarLivro();
  }


  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  newLivro(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editLivro(template: any, livroEditado: Livro) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.livro = Object.assign({}, livroEditado);
    this.registerForm.patchValue(this.livro);
  }

  excluirLivro(livro: Livro, template: any) {
    this.openModal(template);
    this.livro = livro;
    this.bodyDeleteLivro = `Tem certeza que deseja excluir o Livro: ${this.livro.titulo}, Código: ${this.livro.id}`;
  }

  confirmeDelete(template: any) {
    this.livroService.removeLivro(this.livro.id).subscribe(
      () => {
        template.hide();
        this.carregarLivro();
        this.toastr.success('Livro excluído com sucesso!');
      }, error => {

        this.toastr.error('Erro ao tentar deletar!');
      }
    );
  }

  carregarLivro() {
    this.livroService.obterLivros().subscribe(
      _livros => {

        this.livros = _livros;
      },
      error => console.log(error)
    );
  }
  validation(){
    this.registerForm = new FormGroup({
      titulo: new FormControl('',
        [Validators.required,
          Validators.minLength(6),Validators.maxLength(80)]
      ),
      isbn: new FormControl('',
        [Validators.required,
          Validators.minLength(4),Validators.maxLength(20)]
      ),
      autor: new FormControl('',
        [Validators.required,
          Validators.minLength(3),Validators.maxLength(80)]
      ),
      totalPagina: new FormControl('',
        [Validators.required, Validators.min(25),Validators.max(1000)]
      ),
      valor: new FormControl('', Validators.required),
      valorPromocao: new FormControl('', [Validators.required,Validators.minLength(0)]
      ),
      promocao: new FormControl('',Validators.required),
      resumo: new FormControl('' ,Validators.maxLength(200)),
    })
  }

  salvarAlteracao(template: any){
    /* formulario está valido */
    if(this.registerForm.valid){

      if(this.modoSalvar==='post'){

        this.livro = Object.assign({}, this.registerForm.value);

        this.livroService.postLivro(this.livro).subscribe(
          (novoLivro:Livro) =>{

            template.hide();
            this.carregarLivro();
            this.toastr.success('Produto adicionado com sucesso!');
          }, error => {

            this.toastr.error(error.error);
          }
        );
      }else{
        this.livro = Object.assign({id: this.livro.id}, this.registerForm.value);

        this.livroService.putLivro(this.livro).subscribe(
          () =>{
            template.hide();
            this.carregarLivro();
            this.toastr.success('Livro atualizado com sucesso!');
          }, error => {

            this.toastr.error('Erro ao tentar atualizar o Livro!');
          }
        );
      }
    }
  }

}

