import { AutoresService } from './../../services/autores.service';
import { Component, OnInit } from '@angular/core';
import { Autor } from '../../core/models/autor';
import { Response } from '../../core/models/response';
import { Recurso } from '../../core/models/recurso';
import Swal from 'sweetalert2';
import { RecursosComponent } from '../recursos/recursos.component';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-autores',
  standalone: false,

  templateUrl: './autores.component.html',
  styleUrl: './autores.component.css'
})
export class AutoresComponent extends RecursosComponent{
      constructor(service:AutoresService, authService:AuthService) {
        super(service, authService);
        this.nombreRecurso="Autor";
        this.articuloRecurso="el";
        this.pluralRecurso="Autores";
      }
}
