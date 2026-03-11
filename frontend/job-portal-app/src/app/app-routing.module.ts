import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { JobsComponent } from './admin/jobs/jobs.component';
import { UsersComponent } from './admin/users/users.component';
import { ApplicationsComponent } from './admin/applications/applications.component';
import { AuthGuard } from './core/guards/auth.guard';
import { AdminGuard } from './core/guards/admin.guard';

const routes: Routes = [

  // Default route → redirect to login
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'seeker', redirectTo: 'login', pathMatch: 'full' },
{ path: 'seeker/dashboard', redirectTo: 'login', pathMatch: 'full' },

  // Auth routes → public (no guard)
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  // Admin routes → protected by AdminGuard
  {
    path: 'admin',
    component: AdminLayoutComponent,
    canActivate: [AdminGuard],
    children: [
      // Default admin route → dashboard
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent, canActivate: [AdminGuard] },
      { path: 'jobs', component: JobsComponent, canActivate: [AdminGuard] },
      { path: 'users', component: UsersComponent, canActivate: [AdminGuard] },
      { path: 'applications', component: ApplicationsComponent, canActivate: [AdminGuard] }
    ]
  },

  // Wildcard → any unknown route → redirect to login
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }