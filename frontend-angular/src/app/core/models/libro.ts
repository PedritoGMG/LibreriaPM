export class Libro {
  id: number;
  nombre: string;
  autor: string;
  edicion: string;
  formato: string;
  tema: string;
  isbn: string;
  precio: number;
  cantidad: number;
  imgname: string;

  constructor() {
    this.id = 0;
    this.nombre = '';
    this.autor = '';
    this.edicion = '';
    this.formato = '';
    this.tema = '';
    this.isbn = '';
    this.precio = 0;
    this.cantidad = 0;
    this.imgname = '';
  }
}
