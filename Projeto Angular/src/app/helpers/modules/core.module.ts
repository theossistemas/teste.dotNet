import { SimOuNaoPipe } from 'src/app/helpers/pipes/sim-ou-nao.pipe';
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OverlayModule } from '../diretivas/overlay/overlay.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { NgxCurrencyModule } from 'ngx-currency';
import { GlobalErrorHandler } from '../erros/global-error-handler';
import { NgxMaskConfig } from './ngxMaskConfig';

@NgModule({
    declarations: [
        SimOuNaoPipe
    ],
    imports: [
        CommonModule,
        OverlayModule,
        FormsModule,
        ReactiveFormsModule,
        BlockUIModule.forRoot({
            message: 'Aguarde...'
        }),
        NgxCurrencyModule.forRoot(NgxMaskConfig)
    ],
    exports: [
        CommonModule,
        SimOuNaoPipe,
        OverlayModule,
        FormsModule,
        ReactiveFormsModule,
        BlockUIModule,
        NgxCurrencyModule
    ],
    providers: []
})
export class CoreModule { }
