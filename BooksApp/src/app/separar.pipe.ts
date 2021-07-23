import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'separar'
})
export class SepararPipe implements PipeTransform {

  transform(value: any, separator?: any): any {
    let c = value.split(';')
    console.log(c)
    return c
  }

}
