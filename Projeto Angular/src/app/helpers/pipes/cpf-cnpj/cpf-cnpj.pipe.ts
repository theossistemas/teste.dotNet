import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'maskCpfCnpj'
})
export class CpfCnpjMaskPipe implements PipeTransform {

    transform(value: string) {
        if (value) {
            if (value.length == 11) {
                value = value.replace(/\D/g, "")
                value = value.replace(/(\d{3})(\d)/, "$1.$2")
                value = value.replace(/(\d{3})(\d)/, "$1.$2")
                value = value.replace(/(\d{3})(\d{1,2})$/, "$1-$2")
                return value;
            } else if (value.length == 14) {
                value = value.replace(/\D/g, "");
                value = value.replace(/^(\d{2})(\d)/, "$1.$2");
                value = value.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
                value = value.replace(/\.(\d{3})(\d)/, ".$1/$2");
                value = value.replace(/(\d{4})(\d)/, "$1-$2");
                return value;
            }
        }

        return value;
    }
}
