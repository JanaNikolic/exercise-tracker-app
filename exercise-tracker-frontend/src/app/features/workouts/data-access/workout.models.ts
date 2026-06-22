export enum ExerciseType {
  Cardio,
  StrengthTraining,
  Flexibility,
  Aerobic,
  Yoga,
  Other,
}

export interface Workout {
  id: number;
  type: ExerciseType;
  durationInMinutes: number;
  caloriesBurned: number;
  intensityLevel: number;
  fatigueLevel: number;
  trainingDateTime: string;
  notes: string;
  createdAt: string;
}

export interface WorkoutForCreation {
  type: string;
  durationInMinutes: number;
  caloriesBurned: number;
  intensityLevel: number;
  fatigueLevel: number;
  trainingDateTime: string;
  notes: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  currentPage: number;
  pageSize: number;
  hasNextPage: boolean;
}

export interface WeeklySummary {
  weekNumber: number;
  weekStart: string;
  weekEnd: string;
  totalDurationInMinutes: number;
  totalNumberOfTrainings: number;
  averageIntensity: number;
  averageFatigue: number;
}