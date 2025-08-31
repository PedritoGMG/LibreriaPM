import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Recurso } from '../core/models/recurso';
import { RecursosService } from './recursos.service';

@Injectable({
  providedIn: 'root'
})
export class FormatosService extends RecursosService{
  constructor(http:HttpClient) {
    super(http);
    this.rescursoDir = "formatos/formatos-controller";
  }
}
