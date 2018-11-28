import { GlobalService } from './../commons/services/global.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from './service/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;
 
  constructor(
    private formBuilder: FormBuilder,
    private globalService: GlobalService,
    private service: RegisterService
  ) { }

  ngOnInit() {

    this.submitted = true;

    this.registerForm = this.formBuilder.group({
      id: [null],
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
  }

  onSubmit() {

    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return;
    }

    if (this.registerForm.value.id) {
      this.service.updateBook(this.registerForm.value.id, this.registerForm.value).subscribe(resp => {
        console.log(resp);
      })
    } else {
      this.service.saveBook(this.registerForm.value).subscribe(resp => {
        console.log(resp);
      })
    }
  }
}

