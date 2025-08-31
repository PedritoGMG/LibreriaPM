import { AuthService } from './../../services/auth-service.service';
import { TemasService } from './../../services/temas.service';
import { Component, OnInit } from '@angular/core';
import { Tema } from '../../core/models/tema';
import { Recurso } from '../../core/models/recurso';
import { RecursosComponent } from '../recursos/recursos.component';

@Component({
  selector: 'app-tema',
  standalone: false,

  templateUrl: './tema.component.html',
  styleUrl: './tema.component.css'
})
export class TemaComponent extends RecursosComponent{
    constructor(service:TemasService, authService:AuthService) {
      super(service, authService);
      this.nombreRecurso="Tema";
      this.articuloRecurso="el";
      this.pluralRecurso="Temas";
    }
}
