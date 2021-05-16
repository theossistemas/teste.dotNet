import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { GridComponent } from './components/grid/grid.component';
import { DynamicPipe } from './pipes/dynamic.pipe';
import { ChartRadarComponent } from './components/chart-radar/chart-radar.component';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TabMenuModule } from 'primeng/tabmenu';
import { ChartModule } from 'primeng/chart';
import { TableModule } from 'primeng/table';
import { AccountService } from './services/account.service';
import { BookService } from './services/book.service';
import { HttpClientModule } from '@angular/common/http';
import { TooltipModule } from 'primeng/tooltip';

@NgModule({
  declarations: [
    MenuComponent,
    GridComponent,
    DynamicPipe,
    ChartRadarComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    ToastModule,
    ConfirmDialogModule,
    TabMenuModule,
    ChartModule,
    TableModule,
    TooltipModule
  ],
  exports: [
    ToastModule,
    ConfirmDialogModule,
    MenuComponent,
    GridComponent,
    ChartRadarComponent,
  ],
  providers: [
    DynamicPipe,
    AccountService,
    BookService
  ]
})
export class SharedModule { }
