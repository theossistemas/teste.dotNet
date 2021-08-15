import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'SimOuNao'
})
export class SimOuNaoPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return value == true ? 'Sim' : 'Não';
  }

}
