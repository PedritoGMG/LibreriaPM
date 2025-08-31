import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: false,

  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  userRegisterForm:FormGroup;
  constructor(private router: Router, private userService:UsuarioService, private fb:FormBuilder) {}
  ngOnInit(): void {
    this.userRegisterForm = this.fb.group({
      Nombre: ['', [Validators.required]],
      Apellidos: ['', [Validators.required]],
      CP: ['', [Validators.required]],
      Direccion: ['', [Validators.required]],
      Poblacion: ['', [Validators.required]],
      DNI: ['', [Validators. required]],
      Email: ['', [Validators.required, Validators.email]],
      PW: ['', [Validators. required]],
      Pw2: ['', [Validators. required]],
      NombreTitular: ['', [Validators.required]],
      NumberCard: ['', [Validators. required]],
      CVV: ['', [Validators.required]],
    });
  }
  loginUserBtn() {
    this.router.navigateByUrl('login');
  }
  registerUserBtn() {
    console.log(this.userRegisterForm.value)
    if(this.userRegisterForm.get("PW").value!=this.userRegisterForm.get("Pw2").value){
      Swal.fire(
        "Contraseñas Incorrectas",
        'Las contraseñas no son iguales',
        "error"
      );
      return;
    }
    if (this.userRegisterForm.valid) {  // Verificar si el formulario es válido
        this.userService.createUser(this.userRegisterForm.value).subscribe(
          (resp) => {
            this.userService.insertDataBank(this.userRegisterForm.value).subscribe(
              (resp) => {
                this.router.navigateByUrl('main'); // Redirigir al inicio si el login es exitoso
              },
              (err) => {
                Swal.fire(
                  "Error al insertar los datos bancarios",
                  'Problema al insertar datos bancarios',
                  "error"
                );
              }
            );
          },
          (err) => {
            Swal.fire(
              "Error al hacer el registro",
              'Hubo un error inesperado',
              "error"
            );
          }
        );
      } else {
        Swal.fire(
          "Formulario invalido",
          'Complete el formulairo y vuelva a intentarlo',
          "error"
        );
      }
  }
}
