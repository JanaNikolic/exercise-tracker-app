import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const unauthGuard: CanActivateFn = () => {
  const router = inject(Router);
  const token = localStorage.getItem('jwt_token');

  if (token) {
    router.navigate(['/dashboard']);
    return false;
  }

  return true;
};