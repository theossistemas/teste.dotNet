import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard} from './shared';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './admin/login/login.component'; 
import { HomeComponent } from './site/home/Home.component'; 
import { NgxDatatableModule } from '@swimlane/ngx-datatable'; 
import { DetalheLivroComponent } from './site/detalheLivro/detalheLivro.component';
import { LoaderModule } from './admin/components/loader/loader.module';
import { SidebarComponent } from './admin/components/sidebar/sidebar.component';
import { HeaderComponent } from './admin/components/header/header.component';
import { LivroComponent } from './admin/livro/form/livro.component';
import { ListaLivroComponent } from './admin/livro/list/listaLivro.component';
import { LayoutComponent } from './admin/components/layout/layout.component';

@NgModule({
    imports: [ 
        AppRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        BrowserAnimationsModule, 
        LoaderModule,
        NgxDatatableModule, 
    ],
    declarations: [ 
                    HomeComponent,
                    AppComponent,
                    LoginComponent,
                    SidebarComponent,
                    HeaderComponent,
                    LivroComponent,
                    ListaLivroComponent,
                    LayoutComponent,
                    DetalheLivroComponent
                  ],
    providers: [AuthGuard],
    bootstrap: [AppComponent]
})
export class AppModule { }
