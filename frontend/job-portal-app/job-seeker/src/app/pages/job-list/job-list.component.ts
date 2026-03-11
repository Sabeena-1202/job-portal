import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JobService } from '../../services/job.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css']
})
export class JobListComponent implements OnInit {
  jobs: any[] = [];
  errorMessage = '';

  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.jobService.getAllJobs().subscribe({
      next: (res) => { this.jobs = res; },
      error: () => { this.errorMessage = 'Failed to load jobs!'; }
    });
  }

  viewJob(id: number) {
    this.router.navigate(['/jobs', id]);
  }

  logout() {
    this.authService.logout();
  }
}