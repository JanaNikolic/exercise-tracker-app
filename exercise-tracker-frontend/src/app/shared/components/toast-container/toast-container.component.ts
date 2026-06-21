import { Component, inject } from '@angular/core';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  templateUrl: './toast-container.component.html',
})
export class ToastContainerComponent {
  protected toastService = inject(ToastService);

  getToastClasses(type: string): string {
    switch (type) {
      case 'success':
        return 'bg-emerald-50 border-emerald-200 text-emerald-800';
      case 'error':
        return 'bg-rose-50 border-rose-200 text-rose-800';
      default:
        return 'bg-slate-50 border-slate-200 text-slate-800';
    }
  }
}
