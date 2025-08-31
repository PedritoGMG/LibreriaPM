import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TemaComponent } from './tema/tema.component';
import { LibrosComponent } from './libros/libros.component';
import { AutoresComponent } from './autores/autores.component';
import { PagesComponent } from './pages.component';
import { RouterModule, Routes } from '@angular/router';
import { NuevoLibroComponent } from './nuevo-libro/nuevo-libro.component';
import { EdicionesComponent } from './ediciones/ediciones.component';
import { FormatosComponent } from './formatos/formatos.component';
import { AuthGuard } from '../services/auth-guard.service';

const routes: Routes = [
  {
    path:"main", component: PagesComponent, canActivate: [AuthGuard], children:[
      {path:"libro", component: LibrosComponent, data:{title:"Libros"}},
      {path:"tema", component: TemaComponent, data:{title:"Temas"}},
      {path:"autores", component: AutoresComponent, data:{title:"Autores"}},
      {path:"ediciones", component: EdicionesComponent, data:{title:"Ediciones"}},
      {path:"formatos", component: FormatosComponent, data:{title:"Formatos"}},
      {path:"nuevo-libro", component: NuevoLibroComponent, data:{title:"Nuevo Libro"}}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class PagesRoutingModule { }
