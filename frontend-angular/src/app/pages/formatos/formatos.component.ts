import { Component } from '@angular/core';
import { Recurso } from '../../core/models/recurso';
import { FormatosService } from '../../services/formatos.service';
import { RecursosComponent } from '../recursos/recursos.component';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-formatos',
  standalone: false,

  templateUrl: './formatos.component.html',
  styleUrl: './formatos.component.css'
})
export class FormatosComponent extends RecursosComponent{
    constructor(service:FormatosService, authService:AuthService) {
      super(service, authService);
      this.nombreRecurso="Formato";
      this.articuloRecurso="el";
      this.pluralRecurso="Formatos";
    }
}
