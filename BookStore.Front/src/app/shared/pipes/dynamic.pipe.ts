import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';
import { Role } from '../models/access.model';

@Pipe({
  name: 'dynamic'
})
export class DynamicPipe implements PipeTransform {

  transform(value: string, modifier: string): any {
    if (!modifier) {
      return value;
    }
    // tslint:disable-next-line: no-eval
    return eval('this.' + modifier + '(\'' + value + '\')');
  }

  statusFromBoolean(value: string): string {
    let response = '-';

    switch (value) {
      case 'true':
      case '1':
        response = 'enabled';
      default:
        response = 'disabled';
    }

    return response;
  }

  humanizeDate(value: string): string {
    if (moment().diff(moment(value), 'days') < 8) {
      return moment(value).fromNow();
    }
    return moment(value).format('MMMM Do YYYY');
  }

  formatLocalMoney(value?: number): string {
    let response = '-';
    if (value) {
      response = value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    }
    return response;
  }

  role(value: string): string {
    let response = '-';

    switch (value) {
      case '0':
        response = 'ADMIN';
      case '1':
        response = 'USER';
    }

    return response;
  }
}

export const DYNAMIC_PIPES = [DynamicPipe];
