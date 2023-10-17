import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RegisterEntryService {

  private apiUrl = 'https://localhost:32776/api/Parking/register-entry'; 

  constructor(private http: HttpClient) {}

  registerEntry(entryData: any): Observable<any> {
    // Realiza una solicitud POST a la API para registrar la entrada
    return this.http.post(`${this.apiUrl}/registerEntry`, entryData);
  }
}
