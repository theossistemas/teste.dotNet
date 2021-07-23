import { Pipe, PipeTransform } from '@angular/core';
import {CurrencyPipe} from '@angular/common'

@Pipe({
  name: 'currencyFormat'
})
export class CurrencyFormatPipe implements PipeTransform {

  transform(value: number, locale: string, currency_symbol:boolean, number_format:string = '1.2-2'): string {
    if(value){
      let currency_pipe = new CurrencyPipe(locale);
      let new_value:string;

      new_value = currency_pipe.transform(value,locale,currency_symbol,number_format);
      if(locale == 'BRL'){
        new_value = new_value.replace('.','|').replace(',','.').replace('|',',')
      }
      return new_value;
    }

  }

}
