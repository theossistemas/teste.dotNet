import { CoreModule } from './../../../core/core.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatGridListModule, MatIconModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { InputMaskModule } from 'src/app/helpers/diretivas/input-mask/input-mask.module';

@NgModule({
  imports: [
    CommonModule,
    LoginRoutingModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule, 
    FormsModule,
    ReactiveFormsModule, 
    BlockUIModule,
    MatGridListModule,
    MatIconModule,
    InputMaskModule
   
  ],
  declarations: [LoginComponent],
  exports: [
    LoginComponent
  ]
})
export class LoginModule { }
