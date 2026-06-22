import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { WeeklySummary } from '../../data-access/workout.models';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-workout-summary',
  standalone: true,
  imports: [DatePipe, DecimalPipe, FormsModule],
  templateUrl: './workout-summary.component.html',
})
export class WorkoutSummaryComponent {
  summaries = input.required<WeeklySummary[]>();

  monthChanged = output<string>();

  protected currentMonthSelection = this.getCurrentYearMonthString();

  onFetchRequested(): void {
    if (this.currentMonthSelection) {
      this.monthChanged.emit(this.currentMonthSelection);
    }
  }

  private getCurrentYearMonthString(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    return `${year}-${month}`;
  }
}
