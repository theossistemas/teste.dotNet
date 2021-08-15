import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { timer } from 'rxjs';
import { map, share } from 'rxjs/operators';

@Injectable()
export class RelogioService {

  private clock: Observable<Date>;

  constructor() {
    this.clock = timer(0, 1000).pipe(map((() => new Date()), share()));
  }

  obterHora(): Observable<Date> {
    return this.clock;
  }
}
