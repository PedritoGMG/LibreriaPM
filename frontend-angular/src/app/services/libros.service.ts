import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Libro } from '../core/models/libro';
import { map } from 'rxjs';
import { Response } from '../core/models/response';

@Injectable({
  providedIn: 'root'
})
export class LibrosService {
  private options={
    headers:{
    'Content-Type':'application/json'
    }
  }
  private baseUrl="https://localhost:44342/api/"

  constructor(private http:HttpClient) { }
  public getLibros(){
    return this.http.get<Libro[]>(`${this.baseUrl}libros/libros-controller`,this.options)
  }
  public putLibro(libro: Libro) {
    const body = {
      Nombre:libro.nombre,
      Autor:libro.autor,
      Edicion:libro.edicion,
      Formato:libro.formato,
      Tema:libro.tema,
      Precio:libro.precio,
      Isbn:libro.isbn,
      Cantidad:libro.cantidad,
      Imgname:libro.imgname
    }
    return this.http.put<any>(`${this.baseUrl}libros/libros-controller`, body, this.options).pipe(
    map((response: Response) =>
      {
        if(response.Error) throw response
        return response.Data
      }
      )
    )
  }
}
