import { AuthService } from './../../services/auth-service.service';
import { Libro } from '../../core/models/libro';
import { LibrosService } from './../../services/libros.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-libros',
  standalone: false,

  templateUrl: './libros.component.html',
  styleUrl: './libros.component.css'
})
export class LibrosComponent implements OnInit{
  libros:Libro[] = [];
  filteredLibros: Libro[] = [];

  private baseUrl="https://localhost:44342/api/";

  constructor(private LibrosService:LibrosService, public authService:AuthService){ }

  ngOnInit(): void {
    this.LibrosService.getLibros().subscribe((libros:Libro[])=>{
      this.libros=libros
      this.filteredLibros=libros
    })
  }
  getImage(imgName:string){
    return this.baseUrl+"uploads/img?imageName="+imgName;
  }
  filter(filteredData: any[]) {
      this.filteredLibros = filteredData;
  }


}
