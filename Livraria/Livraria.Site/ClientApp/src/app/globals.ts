import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
  isAuthenticated: boolean = false;
  token: string
  public SetAuth(newToken: string) {
    this.isAuthenticated = true;
    this.token = newToken;
  }

  public Logout() {
    this.isAuthenticated = false;
    this.token = '';
  }
}
