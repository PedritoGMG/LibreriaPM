import { Autor } from './../../core/models/autor';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Libro } from '../../core/models/libro';
import { AutoresService } from '../../services/autores.service';
import { TemasService } from '../../services/temas.service';
import Swal from 'sweetalert2';
import { Recurso } from '../../core/models/recurso';
import { FileUploadService } from '../../services/file.upload.service';
import { EdicionesService } from '../../services/ediciones.service';
import { FormatosService } from '../../services/formatos.service';
import { LibrosService } from '../../services/libros.service';

interface City {
  name: string,
  code: string
}

@Component({
  selector: 'app-nuevo-libro',
  standalone: false,

  templateUrl: './nuevo-libro.component.html',
  styleUrl: './nuevo-libro.component.css'
})
export class NuevoLibroComponent implements OnInit{

  libroFormulario:FormGroup

  private pulsadoBtnGuardar: boolean=false

  autores:Recurso[]=[]
  temas:Recurso[]=[]
  ediciones:Recurso[]=[]
  formato:Recurso[]=[]

  libro:Libro=new Libro()

  public imgUpload:File|any;
  public imgTemp:string|ArrayBuffer|any=null

  selectedCity: City | any;
  // cities: City[];

  constructor(private fb:FormBuilder, private autoresService:AutoresService, private edicionesService:EdicionesService,
    private formatosService:FormatosService, private temasService:TemasService, private libroService: LibrosService,
    private uploadService: FileUploadService){
    this.libroFormulario=this.fb.group({})

    // this.cities = [
    //   { name: 'New York', code: 'NY' },
    //   { name: 'Rome', code: 'RM' },
    //   { name: 'London', code: 'LDN' },
    //   { name: 'Istanbul', code: 'IST' },
    //   { name: 'Paris', code: 'PRS' }
    //   ];

  }


  ngOnInit(): void {
    this.getDataRecursos();
    this.libroFormulario=this.fb.group({
        Nombre:['', Validators.required],
        Isbn: ['', [Validators.required, Validators.minLength(13)]],
        Precio: [0, Validators.required],
        Cantidad: [0, Validators.required],
        Autor: ['', Validators.required],
        Tema: ['', Validators.required],
        Edicion: ['', Validators.required],
        Formato: ['', Validators.required],
    })
  }

  isValidField(key: string) {
    return this.libroFormulario.get(key)?.invalid && this.pulsadoBtnGuardar
    }

  saveBook(){
    if (this.libroFormulario.invalid) {
      this.pulsadoBtnGuardar=true
      Swal.fire("Error",
        "Los campos no cumplen los requisitos",
        "error")
    }else{

      this.libro.nombre=this.libroFormulario.value.Nombre;
      this.libro.precio=this.libroFormulario.value.Precio;
      this.libro.isbn=this.libroFormulario.value.Isbn;
      this.libro.cantidad=this.libroFormulario.value.Cantidad;

      this.libro.autor=this.libroFormulario.value.Autor.id;
      this.libro.tema=this.libroFormulario.value.Tema.id;
      this.libro.formato=this.libroFormulario.value.Formato.id;
      this.libro.edicion=this.libroFormulario.value.Edicion.id;

      this.libroService.putLibro(this.libro).subscribe(
        (res) => {
          if (this.imgUpload) {
            this.uploadService.updatePhoto(this.imgUpload).then(() => {
              Swal.fire(
                'Éxito',
                'Libro añadido con éxito - imagen',
                'success'
              );
            }).catch((err) => {
              Swal.fire(
                'Error',
                'Error al subir la imagen',
                'error'
              );
            });
          } else {
            Swal.fire(
              'Éxito',
              'Libro añadido con éxito - sin imagen',
              'success'
            );
          }
        },
        (err) => {
          Swal.fire(
            'Error',
            err.error,
            'error'
          );
        }
      );

      // this.libro.autor=autor.id
      // this.libro.cantidad=cantidad
      // this.libro.edicion=edicion.id
      // this.libro.formato=formato.id
      // this.libro.isbn=isbn
      // this.libro.precio=precio
      // this.libro.nombre=nombre
      // this.libro.tema=tema.id
    }
  }

  changeImg(event:any){
    let file=event.target.files[0]
    this.imgUpload=file
    this.libro.imgname=this.imgUpload.name
    if (file) {
      this.imgTemp=null
      return;
    }
    const reader=new FileReader()
    reader.readAsDataURL(file)
    reader.onloadend=()=>{
      console.log();
      this.imgTemp=reader.result
    }
  }

  getDataRecursos(){
    this.autoresService.get().subscribe((data)=>{
      this.autores=data
      this.temasService.get().subscribe((data)=>{
        this.temas=data
        this.edicionesService.get().subscribe((data)=>{
          this.ediciones=data
          this.formatosService.get().subscribe((data)=>{
            this.formato=data
          })
        })
      })
    })
  }

}
