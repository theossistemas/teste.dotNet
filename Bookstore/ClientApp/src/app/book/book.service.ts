import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class BookService {

  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  getData() {

    return this.http.get('/Books'); 
  }

  postData(formData) {
    if (formData.id === null)
      formData.id = 0;

    return this.http.post('/Books', formData);
  }

  putData(id, formData) {
    return this.http.put('/Books/' + id, formData);
  }

  deleteData(id) {
    return this.http.delete('/Books/' + id);
  }

}  
