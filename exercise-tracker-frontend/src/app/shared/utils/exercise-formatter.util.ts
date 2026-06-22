import { ExerciseType } from '../../features/workouts/data-access/workout.models';

/**
 * Converts any ExerciseType enum value or string key
 * Example: ExerciseType.StrengthTraining -> 'Strength Training'
 * Example: 'Flexibility' -> 'Flexibility'
 */
export function formatExerciseName(value: ExerciseType | string | null | undefined): string {
  if (value === null || value === undefined) return '';

  const stringKey = typeof value === 'number' ? ExerciseType[value] : String(value);
  if (!stringKey) return '';

  if (stringKey === 'StrengthTraining') {
    return 'Strength Training';
  }

  return stringKey.replace(/([A-Z])/g, ' $1').trim();
}