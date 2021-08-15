import { CpfCnpjMaskPipe } from './cpf-cnpj.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [CpfCnpjMaskPipe],
    exports: [CpfCnpjMaskPipe]
})
export class CpfCnpjPipeModule { }
