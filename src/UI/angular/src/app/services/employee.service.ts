import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  endpoint = 'mitarbeiter';

  constructor(private httpClient: HttpClient) {
  }

  getAll() {
    var url = `${environment.apiUrl}/${this.endpoint}`;
    return this.httpClient.get(url);
  }

  delete(id: number) {
    var url = `${environment.apiUrl}/${this.endpoint}?id=${id}`;
    return this.httpClient.delete(url);
  }
}
