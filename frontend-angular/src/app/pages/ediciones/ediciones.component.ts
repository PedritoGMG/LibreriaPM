import { Component } from '@angular/core';
import { Recurso } from '../../core/models/recurso';
import { EdicionesService } from '../../services/ediciones.service';
import { Response } from '../../core/models/response';
import Swal from 'sweetalert2';
import { RecursosComponent } from '../recursos/recursos.component';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-ediciones',
  standalone: false,

  templateUrl: './ediciones.component.html',
  styleUrl: './ediciones.component.css'
})
export class EdicionesComponent extends RecursosComponent{
    constructor(service:EdicionesService, authService:AuthService) {
      super(service, authService);
      this.nombreRecurso="Edicion";
      this.articuloRecurso="la";
      this.pluralRecurso="Ediciones";
    }
}
