import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Recurso } from '../../core/models/recurso';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-barra-cierre',
  standalone: false,

  templateUrl: './barra-cierre.component.html',
  styleUrl: './barra-cierre.component.css'
})
// barra-cierre.component.ts
export class BarraCierreComponent implements OnInit {
  @Input() dataReal: Recurso[] = []; // Recibe la lista completa de recursos
  @Output() dataEmit: EventEmitter<Recurso[]> = new EventEmitter<Recurso[]>(); // Emite los datos filtrados

  searchTerm: string = ''; // Término de búsqueda

  constructor(private router: Router) {}

  ngOnInit(): void {}

  navigateToMain() {
    this.router.navigateByUrl('/');
  }

  searchRecurso(e: any) {
    if (e.length >= 2) {
      const filteredData = this.dataReal.filter((recurso: Recurso) =>
        recurso.nombre.toLowerCase().includes(e.toLowerCase())
      );

      if (filteredData.length === 0) {
        Swal.fire(
          'Warning',
          'No existen recursos que coincidan con la búsqueda',
          'info'
        );
      }
      this.dataEmit.emit(filteredData); // Emitir los datos filtrados
    } else {
      this.dataEmit.emit(this.dataReal); // Si no hay término de búsqueda, emitir todos los recursos
    }
  }
}
