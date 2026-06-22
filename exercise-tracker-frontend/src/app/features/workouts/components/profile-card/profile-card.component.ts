import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserProfile } from '../../../../core/models/user.model';

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './profile-card.component.html',
})
export class ProfileCardComponent {
  profile = input.required<UserProfile>();
  logoutRequest = output<void>();

  onLogout(): void {
    this.logoutRequest.emit();
  }
}
