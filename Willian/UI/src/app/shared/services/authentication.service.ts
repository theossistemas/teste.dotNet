import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/login.model';
 
@Injectable({ providedIn: 'root' })
export class AuthenticationService {
private currentUserSubject: BehaviorSubject<any>;
public currentUser: Observable<any>; 

constructor(private http: HttpClient) {
this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(localStorage.getItem('currentUser')));
this.currentUser = this.currentUserSubject.asObservable();
}

public get currentUserValue(): any {
return this.currentUserSubject.value;
}

headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data');

// Http Headers
httpOptions = { headers: new HttpHeaders({'Content-Type': 'application/json'})};

 


login(input: LoginModel): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}/Usuario/login`, input, this.httpOptions).pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user.conteudo));
        this.currentUserSubject.next(user.conteudo);
        return user;
    }));
}
  
logout() {
// remove user data from local storage for log out
localStorage.removeItem('currentUser');
this.currentUserSubject.next(null);
}

handleError(error: any) {
    const errMsg = (error.message) ? error.message :
        error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg);
    return Observable.throw(errMsg);
}
}
