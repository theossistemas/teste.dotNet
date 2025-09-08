import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'localeDate', standalone: true })
export class LocaleDatePipe implements PipeTransform {
  transform(value: any, locale: string = 'pt-BR'): string {
    if (!value) return '';
    const date = new Date(value);
    // ajusta para UTC-3 (Brasília)
    date.setHours(date.getHours() - 3);
    return date.toLocaleString(locale);
  }
}
