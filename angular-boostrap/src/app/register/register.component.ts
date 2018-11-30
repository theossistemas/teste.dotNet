import { Book } from './../models/book';
import { GlobalService } from './../commons/services/global.service';
import { Component, OnInit, DebugElement, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from './service/register.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private globalService: GlobalService,
    private service: RegisterService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.submitted = true;

    this.registerForm = this.formBuilder.group({
      id: [0],
      name: [null, Validators.required],
      description_short: [null],
      description_long: [null],
      photo: [null, Validators.required],
      price: [null, Validators.required],
      author: [null, Validators.required],
      year: [null, Validators.required],
      language: [null],
      publishing: [null, Validators.required],
      weight: [null],
      quantity_pages: [null]
    });

    const idEdit = this.route.snapshot.paramMap.get('id');

    if ( idEdit !== null) {
      this.editBook(idEdit);
    }
  }

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      const file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.registerForm.get('photo').setValue({
          filename: file.name,
          filetype: file.type,
          value: reader.result.toString().split(',')[1]
        });
      };
    }
  }


  editBook(id) {
    this.spinner.show();

    this.service.getBookById<Book>(id).subscribe(resp => {
      this.registerForm.setValue({
        id: resp.id,
        name: resp.name,
        description_short: resp.description_short,
        description_long: resp.description_short,
        photo: '',
        price: resp.price,
        author: resp.author,
        year: resp.year,
        language: resp.language,
        publishing: resp.publishing,
        weight: resp.weight,
        quantity_pages: resp.quantity_pages
      });

      setTimeout(() => {
          this.spinner.hide();
      }, 1000);

    });
  }

  onSubmit() {

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      this.toastr.error('Todos os campos sao obrigatorios');
        return;
    }

    this.spinner.show();

    if (this.registerForm.value.id) {
      this.service.updateBook(this.registerForm.value.id, this.registerForm.value).subscribe(resp => {
        this.router.navigate(['/']);
        this.toastr.success('Atualizado com sucesso');
      });
    } else {
      this.service.saveBook(this.registerForm.value).subscribe(resp => {
        this.router.navigate(['/']);
        this.toastr.success('Cadastrado com sucesso');
      });
    }
  }
}

