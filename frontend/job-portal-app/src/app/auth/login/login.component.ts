import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    // Redirect if already logged in
    if (this.authService.isLoggedIn()) {
      if (this.authService.isAdmin()) {
        this.router.navigate(['/admin/dashboard']);
      }
    }

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  onSubmit(): void {
  if (this.loginForm.invalid) return;

  this.isLoading = true;
  this.errorMessage = '';

  this.authService.login(this.loginForm.value).subscribe({
    next: (response) => {
      this.isLoading = false;
      const role = this.authService.getRole();
      console.log('Role from storage:', role);

      if (role === 'Admin') {
        this.router.navigate(['/admin/dashboard']);
      } else if (role === 'JobSeeker') {
        // Temporary until teammate builds seeker module
        alert('Job Seeker login successful! Seeker dashboard coming soon.');
      } else {
        this.errorMessage = 'Unknown role. Please contact admin.';
      }
    },
    error: (err) => {
      this.isLoading = false;
      this.errorMessage = err.error?.message || 'Login failed. Please try again.';
    }
  });
}
}