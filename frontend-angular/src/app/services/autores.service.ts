import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RecursosService } from './recursos.service';

@Injectable({
  providedIn: 'root'
})
export class AutoresService extends RecursosService{
  constructor(http:HttpClient) {
    super(http);
    this.rescursoDir = "autores/autores-controller";
  }
}
