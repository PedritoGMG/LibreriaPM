import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { Recurso } from '../../core/models/recurso';
import { Response } from '../../core/models/response';
import { RecursosService } from '../../services/recursos.service';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-recursos',
  standalone: false,

  templateUrl: './recursos.component.html',
  styleUrl: './recursos.component.css'
})
export class RecursosComponent implements OnInit {
  recursos: Recurso[] = [];
  filteredRecursos: Recurso[] = [];

  protected nombreRecurso: string = "";
  protected articuloRecurso: string = "";
  protected pluralRecurso: string = "";

  constructor(protected service: RecursosService, public authService:AuthService) {}

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.service.get().subscribe((data) => {
      this.recursos = data;
      this.filteredRecursos = data; // Inicialmente, muestra todos los recursos
    });
  }

  filter(filteredData: Recurso[]) {
    this.filteredRecursos = filteredData;
  }

  delete(recurso: Recurso) {
    Swal.fire({
      title: "¿Estas seguro de borrar " + this.articuloRecurso + " " + this.nombreRecurso + "?",
      text: recurso.nombre,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Eliminar",
      cancelButtonText: "Cancelar",
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: "Se eliminará los libros que tengan este recurso ¿Estas seguro de borrarlo? ",
          text: recurso.nombre,
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Eliminar",
          cancelButtonText: "Cancelar",
        }).then((result) => {
          if (result.isConfirmed) {
            this.service.delete(recurso.id).subscribe((resp) => {
              Swal.fire({
                title: "Eliminado!",
                text: "Se a eliminado " + this.articuloRecurso + " " + this.nombreRecurso + " correctamente.",
                icon: "success"
              });
              this.get();
            }),
              (resp: Response) => {
                Swal.fire({
                  title: "Error al Eliminar " + this.articuloRecurso + " " + this.nombreRecurso + "",
                  text: resp.Error,
                  icon: "error"
                });
              }
          }
        });
      }
    });
  }

  put() {
    Swal.fire({
      title: "Agregando un nuevo " + this.nombreRecurso + "",
      text: "Escribe el nombre del nuevo " + this.nombreRecurso + "",
      input: "text",
      inputPlaceholder: "Nombre",
      inputAttributes: {
        autocapitalize: "off"
      },
      showCancelButton: true,
      confirmButtonText: "Confirmar",
      cancelButtonText: "Cancelar",
      showLoaderOnConfirm: true
    }).then((result) => {
      if (result.isConfirmed) {
        if (result.value == "") {
          Swal.fire("Error",
            "Campo Vacio",
            "error");
          return;
        }
        this.service.put(result.value).subscribe(
          (resp) => {
            Swal.fire({
              title: "Agregado " + this.articuloRecurso + " " + this.nombreRecurso + "!",
              text: "Se ha agregado " + this.articuloRecurso + " " + this.nombreRecurso + " correctamente.",
              icon: "success"
            });
            this.get();
          },
          (resp: Response) => {
            Swal.fire({
              title: "Error al agregar " + this.articuloRecurso + " " + this.nombreRecurso + "",
              text: resp.Error,
              icon: "error"
            });
          }
        );
      }
    });
  }

  post(recurso: Recurso) {
    Swal.fire({
      title: "Modificando " + this.articuloRecurso + " " + this.nombreRecurso + "",
      text: "" + recurso.nombre,
      input: "text",
      inputPlaceholder: "Nombre",
      inputAttributes: {
        autocapitalize: "off"
      },
      showCancelButton: true,
      confirmButtonText: "Confirmar",
      cancelButtonText: "Cancelar",
      showLoaderOnConfirm: true
    }).then((result) => {
      if (result.isConfirmed) {
        if (result.value == "") {
          Swal.fire("Error",
            "Campo Vacio",
            "error");
          return;
        }
        this.service.post(recurso.id, result.value).subscribe(
          (resp) => {
            Swal.fire({
              title: "Modificado Correctamente!",
              text: "Se ha modificado " + this.articuloRecurso + " " + this.nombreRecurso + " correctamente.",
              icon: "success"
            });
            this.get();
          },
          (resp: Response) => {
            Swal.fire({
              title: "Error al modificar " + this.articuloRecurso + " " + this.nombreRecurso + "",
              text: resp.Error,
              icon: "error"
            });
          }
        );
      }
    });
  }
}
