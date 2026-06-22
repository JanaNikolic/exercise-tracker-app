import { DatePipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { PaginatedResponse, Workout } from '../../data-access/workout.models';

@Component({
  selector: 'app-workout-list',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './workout-list.component.html',
})
export class WorkoutListComponent {
  workoutData = input.required<PaginatedResponse<Workout>>();
  pageChanged = output<number>();
  deleteRequest = output<number>();
}
