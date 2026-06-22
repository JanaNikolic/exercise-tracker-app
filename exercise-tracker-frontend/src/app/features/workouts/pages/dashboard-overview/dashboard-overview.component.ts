import { Component, inject, OnInit, signal } from '@angular/core';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { PaginatedResponse, Workout, WeeklySummary } from '../../data-access/workout.models';
import { WorkoutService } from '../../data-access/workout.service';
import { AsyncPipe } from '@angular/common';
import { ProfileCardComponent } from '../../components/profile-card/profile-card.component';
import { WorkoutListComponent } from '../../components/workout-list/workout-list.component';
import { AddWorkoutFormComponent } from '../../components/add-workout-form/add-workout-form.component';
import { UserService } from '../../../../core/services/user.service';
import { AuthService } from '../../../auth/data-access/auth.service';
import { WorkoutSummaryComponent } from '../../components/workout-summary/workout-summary.component';
import { ToastService } from '../../../../core/services/toast.service';
import { ToastContainerComponent } from '../../../../shared/components/toast-container/toast-container.component';
import { ConfirmationModalComponent } from '../../../../shared/components/modal/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-dashboard-overview',
  standalone: true,
  imports: [
    AsyncPipe,
    ProfileCardComponent,
    WorkoutListComponent,
    AddWorkoutFormComponent,
    WorkoutSummaryComponent,
    ToastContainerComponent,
    ConfirmationModalComponent,
  ],
  templateUrl: './dashboard-overview.component.html',
})
export class DashboardOverviewComponent implements OnInit {
  protected workoutService = inject(WorkoutService);
  protected userService = inject(UserService);
  private authService = inject(AuthService);
  private toastService = inject(ToastService);

  protected activeDeleteTargetId = signal<number | null>(null);
  private workoutPage$ = new BehaviorSubject<number>(1);
  private summaryPeriod$ = new BehaviorSubject<{ year: number; month: number }>({
    year: new Date().getFullYear(),
    month: new Date().getMonth() + 1,
  });

  workouts$!: Observable<PaginatedResponse<Workout>>;
  summary$!: Observable<WeeklySummary[]>;
  
  protected summaryData = signal<WeeklySummary[]>([]);

  ngOnInit(): void {
    this.userService.getProfile().subscribe();
    this.workouts$ = this.workoutPage$.pipe(
      switchMap((page) => this.workoutService.getWorkouts(page, 2)),
    );
    this.summaryPeriod$
      .pipe(switchMap((period) => this.workoutService.getMonthlySummary(period.year, period.month)))
      .subscribe({
        next: (data) => {
          this.summaryData.set(data);
        },
        error: (err) => {
          const errorMsg = err.error?.title || err.error?.message || 'Failed to load summary.';
          this.toastService.show(errorMsg, 'error');
        },
      });
  }

  onLogoutRequested(): void {
    this.authService.logout();
  }

  onMonthSelected(yearMonthStr: string): void {
    const [year, month] = yearMonthStr.split('-').map(Number);
    this.summaryPeriod$.next({ year, month });
  }

  onPageSelected(newPage: number): void {
    this.workoutPage$.next(newPage);
  }

  onWorkoutCreated(): void {
    this.workoutPage$.next(1);
    this.summaryPeriod$.next(this.summaryPeriod$.value);
  }

  onDeleteWorkout(id: number): void {
    this.activeDeleteTargetId.set(id);
  }

  onConfirmDelete(): void {
    const id = this.activeDeleteTargetId();
    if (!id) return;

    this.workoutService.deleteWorkout(id).subscribe({
      next: () => {
        this.toastService.show('Workout session removed successfully.', 'success');
        this.activeDeleteTargetId.set(null);
        this.workoutPage$.next(this.workoutPage$.value);
        this.summaryPeriod$.next(this.summaryPeriod$.value);
      },
      error: (err) => {
        const errorMsg = err.error?.message || 'Failed to delete workout session.';
        this.toastService.show(errorMsg, 'error');
        this.activeDeleteTargetId.set(null);
      },
    });
  }
}
