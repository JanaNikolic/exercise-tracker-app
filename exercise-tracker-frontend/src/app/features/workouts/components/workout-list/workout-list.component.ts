import { DatePipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { PaginatedResponse, Workout } from '../../data-access/workout.models';
import { formatExerciseName } from '../../../../shared/utils/exercise-formatter.util';

@Component({
  selector: 'app-workout-list',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './workout-list.component.html',
})
export class WorkoutListComponent {
  protected formatExerciseName = formatExerciseName;
  workoutData = input.required<PaginatedResponse<Workout>>();
  pageChanged = output<number>();
  deleteRequest = output<number>();
}
