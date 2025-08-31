// auth.service.ts
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedIn = false;
  private userRole: string | null = null;

  constructor(private router: Router) {}

  login(role: number) {
    this.isLoggedIn = true;
    this.userRole = role==1 ? "ADMIN" : "USER";
    localStorage.setItem('isLoggedIn', 'true');
    localStorage.setItem('userRole', this.userRole);
  }

  logout() {
    this.isLoggedIn = false;
    this.userRole = null;
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('userRole');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn || localStorage.getItem('isLoggedIn') === 'true';
  }

  getUserRole(): string | null {
    return this.userRole || localStorage.getItem('userRole');
  }

  isAdmin(){
    return this.userRole==="ADMIN" || localStorage.getItem('userRole') === 'ADMIN';
  }

}
