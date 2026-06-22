import { Component, inject, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { WorkoutService } from '../../data-access/workout.service';
import { ExerciseType, WorkoutForCreation } from '../../data-access/workout.models';
import { ToastService } from '../../../../core/services/toast.service';
import { formatExerciseName } from '../../../../shared/utils/exercise-formatter.util';

@Component({
  selector: 'app-add-workout-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-workout-form.component.html',
})
export class AddWorkoutFormComponent {
  private workoutService = inject(WorkoutService);
  private toastService = inject(ToastService);

  protected exerciseOptions = Object.keys(ExerciseType)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: key,
      displayName: formatExerciseName(key),
    }));

  workoutCreated = output<void>();

  isSubmitting = signal(false);

  formModel: WorkoutForCreation = {
    type: 'Cardio',
    durationInMinutes: 30,
    caloriesBurned: 200,
    intensityLevel: 5,
    fatigueLevel: 5,
    trainingDateTime: this.getLocalDateTimeString(),
    notes: '',
  };

  onSubmit() {
    this.isSubmitting.set(true);

    const payload = {
      ...this.formModel,
      trainingDateTime: new Date(this.formModel.trainingDateTime).toISOString(),
    };

    this.workoutService.createWorkout(payload).subscribe({
      next: () => {
        this.isSubmitting.set(false);
        this.resetForm();

        this.toastService.show('Workout session saved successfully.', 'success');

        this.workoutCreated.emit();
      },
      error: (err) => {
        this.isSubmitting.set(false);
        const apiErrorText =
          err.error?.error || err.error?.message || 'An unexpected validation error occurred.';
        this.toastService.show(apiErrorText, 'error');
      },
    });
  }

  private resetForm() {
    this.formModel = {
      type: 'Cardio',
      durationInMinutes: 30,
      caloriesBurned: 200,
      intensityLevel: 5,
      fatigueLevel: 5,
      trainingDateTime: this.getLocalDateTimeString(),
      notes: '',
    };
  }

  private getLocalDateTimeString(): string {
    const now = new Date();
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    return now.toISOString().slice(0, 16);
  }
}
