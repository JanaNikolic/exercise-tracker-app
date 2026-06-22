import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResponse, Workout, WeeklySummary, WorkoutForCreation } from './workout.models';
import { environment } from '../../../../environments/environment.development';
import { UserProfile } from '../../../core/models/user.model';

@Injectable({ providedIn: 'root' })
export class WorkoutService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/workouts`;

  profileSignal = signal<UserProfile>({ name: 'John Doe', email: 'john@example.com' });

  createWorkout(workout: WorkoutForCreation): Observable<Workout> {
    return this.http.post<Workout>(this.baseUrl, workout);
  }

  getWorkouts(page: number, size: number): Observable<PaginatedResponse<Workout>> {
    const params = new HttpParams().set('page', page).set('size', size);
    return this.http.get<PaginatedResponse<Workout>>(this.baseUrl, { params });
  }

  getMonthlySummary(year: number, month: number): Observable<WeeklySummary[]> {
    const params = new HttpParams().set('year', year).set('month', month);
    return this.http.get<WeeklySummary[]>(`${this.baseUrl}/monthly-summary`, { params });
  }

  deleteWorkout(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  updateProfile(profile: UserProfile): void {
    this.profileSignal.set(profile);
  }
}
