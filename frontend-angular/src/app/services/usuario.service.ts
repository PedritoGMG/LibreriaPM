// usuario.service.ts
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../core/models/user';
import { map, switchMap, tap } from 'rxjs';
import { Response } from '../core/models/response';
import { AuthService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private options = {
    headers: {
      'Content-Type': 'application/json'
    },
    body: {}
  };
  private baseUrl = "https://localhost:44342/api/";
  protected loginDir = "user/login";
  protected createUserDir = "user/crete-user";
  protected insertBackDir = "user/insert-bank";

  constructor(
    private http: HttpClient,
    private authService: AuthService // Inyecta el AuthService
  ) {}

  isAdmin(email: string) {
    // Configura el cuerpo de la solicitud como un objeto JSON con el email
    const body = { Email: email };

    return this.http
      .post<number>(this.baseUrl + 'user/isAdmin', body, this.options)
      .pipe(
        map((resp) => {
          return resp;
        })
      );
  }

  logUser(user: User) {
    return this.http
      .post<any>(this.baseUrl + this.loginDir, { ...user }, this.options)
      .pipe(
        switchMap((data: Response) => {
          if (data.Error) throw data;

          // Llamar a isAdmin para verificar si el usuario es administrador
          return this.isAdmin(user['Email']).pipe(
            map((isAdmin: number) => {
              // Almacenar la información del rol en el servicio de autenticación
              this.authService.login(isAdmin);
              return data;
            })
          );
        })
      );
  }

  createUser(user: User) {
    return this.http
      .post<any>(this.baseUrl + this.createUserDir, { ...user }, this.options)
      .pipe(
        tap((data: Response) => {
          if (data.Error) throw data;
          sessionStorage.setItem('isLogedUser', '1');
          user = data.Data;
          sessionStorage.setItem('userId', `${data.Data}`);
          return data;
        })
      );
  }

  insertDataBank(user: any) {
    return this.http
      .post<any>(this.baseUrl + this.insertBackDir, { ...user }, this.options)
      .pipe(
        tap((data: Response) => {
          if (data.Error) throw data;
          return data;
        })
      );
  }
}
