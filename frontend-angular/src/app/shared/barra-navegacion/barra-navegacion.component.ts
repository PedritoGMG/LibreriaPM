import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../services/auth-service.service';


@Component({
  selector: 'app-barra-navegacion',
  standalone: false,

  templateUrl: './barra-navegacion.component.html',
  styleUrl: './barra-navegacion.component.css'
})
export class BarraNavegacionComponent implements OnInit{
  navBar: MenuItem[]=[]
  constructor(private router:Router, private authService: AuthService ){

  }
  ngOnInit():void {
    this.navBar=[
      {label:"Libros", icon:"assets/images/book.png", command:()=>{this.router.navigateByUrl("main/libro")}},
      {label:"Temas", icon:"assets/images/topic.png", command:()=>{this.router.navigateByUrl("main/tema")}},
      {label:"Autores", icon:"assets/images/boss.png", command:()=>{this.router.navigateByUrl("main/autores")}},
      {label:"Ediciones", icon:"assets/images/edition.png", command:()=>{this.router.navigateByUrl("main/ediciones")}},
      {label:"Formatos", icon:"assets/images/format.png", command:()=>{this.router.navigateByUrl("main/formatos")}},
      {label:"Cerrar Sesion", icon:"assets/images/cerrar-sesion.png", command:()=>{this.authService.logout()}}
    ]
  }
}
