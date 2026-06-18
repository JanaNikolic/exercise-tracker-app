import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { unauthGuard } from './core/guards/unauth-guard';

export const routes: Routes = [
  {
    path: 'login',
    canActivate: [unauthGuard],
    loadComponent: () =>
      import('./features/auth/components/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'register',
    canActivate: [unauthGuard],
    loadComponent: () =>
      import('./features/auth/components/register/register.component').then((m) => m.RegisterComponent),
  },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/workouts/dashboard-overview/dashboard-overview.component').then(
            (m) => m.DashboardOverviewComponent,
          ),
      },
    ],
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' },
];
