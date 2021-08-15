import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { MatInputModule, MatButtonModule, MatCardModule, MatSelectModule, MatDividerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,    
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatDividerModule,     
    FormsModule,
    ReactiveFormsModule,    
    BlockUIModule
  ]
})
export class DashboardModule { }
