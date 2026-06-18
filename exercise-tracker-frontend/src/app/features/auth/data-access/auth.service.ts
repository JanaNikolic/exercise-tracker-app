// src/app/features/auth/auth.service.ts
import { inject, Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { RegisterData, AuthResponse, LoginCredentials } from '../auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private baseUrl = `${environment.apiUrl}/users`;

  private tokenSignal = signal<string | null>(localStorage.getItem('jwt_token'));
  
  public isAuthenticated = computed(() => !!this.tokenSignal());

  register(data: RegisterData) {
    return this.http.post<AuthResponse>(`${this.baseUrl}`, data);
  }

  login(credentials: LoginCredentials) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, credentials).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('jwt_token', response.token);
          
          this.tokenSignal.set(response.token);
          
          this.router.navigate(['/dashboard']);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('jwt_token');
    this.tokenSignal.set(null);
    this.router.navigate(['/login']);
  }
}