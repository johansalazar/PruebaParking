import { Component } from '@angular/core';
import { RegisterEntryService } from 'src/app/register-entry.service';

@Component({
  selector: 'app-register-entry',
  templateUrl: './register-entry.component.html',
  styleUrls: ['./register-entry.component.css'],
})
export class RegisterEntryComponent {
  entryName: string = '';
  entryDate: string = '';

  constructor(private registerEntryService: RegisterEntryService) {}

  registerEntry() {
    // Llama al método del servicio para registrar la entrada
    this.registerEntryService.registerEntry({
      name: this.entryName,
      date: this.entryDate,
      // Otros campos según la estructura de tu API y modelo de datos
    }).subscribe(response => {
      // Maneja la respuesta de la API según sea necesario
      console.log('Entrada registrada con éxito:', response);
    }, error => {
      // Maneja errores
      console.error('Error al registrar entrada:', error);
    });
  }
}
