import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { UserProfile } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/users`;

  currentUser = signal<UserProfile | null>(null);

  getProfile(): Observable<UserProfile> {
    return this.http
      .get<UserProfile>(`${this.baseUrl}/profile`)
      .pipe(tap((profile) => this.currentUser.set(profile)));
  }
}
