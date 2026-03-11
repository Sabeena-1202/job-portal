import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JobService } from '../../services/job.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  dashboard: any = null;
  name = '';
  errorMessage = '';

  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.name = this.authService.getName() || '';
    this.jobService.getDashboard().subscribe({
      next: (res) => { this.dashboard = res; },
      error: () => { this.errorMessage = 'Failed to load dashboard!'; }
    });
  }

  goToJobs() {
    this.router.navigate(['/jobs']);
  }

  logout() {
    this.authService.logout();
  }
}