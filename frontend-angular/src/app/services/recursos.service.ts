import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Recurso } from '../core/models/recurso';
import { map } from 'rxjs';
import { Response } from '../core/models/response';

@Injectable({
  providedIn: 'root'
})
export abstract class RecursosService {
  private options={
    headers:{
    'Content-Type':'application/json'
    },
    body: {}
  }
  private baseUrl="https://localhost:44342/api/"
  protected rescursoDir = "";

  constructor(private http:HttpClient) {}

  public get(){
    return this.http.get<Recurso[]>(this.baseUrl+this.rescursoDir,this.options)
  }
  public delete(id:number){
    this.options.body={Id:id}
    return this.http.delete<Response>(this.baseUrl+this.rescursoDir,this.options).pipe(map(
      (resp)=>{
        if(resp.Error) throw resp
        return resp
      }
    ))
  }
  public post(id:number, nombre:string){
    const body = {
      Id:id,
      Nombre:nombre
    }
    console.log(this.options.body)
    return this.http.post<Response>(this.baseUrl+this.rescursoDir, body, this.options).pipe(map(
      (resp)=>{
        if(resp.Error) throw resp
        return resp
      }
    ))
  }
  public put(nombre: string) {
    const body = {
      Nombre:nombre
    }
    return this.http.put<any>(this.baseUrl+this.rescursoDir, body, this.options).pipe(
    map((response: Response) =>
      {
        if(response.Error) throw response
        return response.Data
      }
      )
    )
  }
}
