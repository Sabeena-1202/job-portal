import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobService } from '../../services/job.service';

@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.css']
})
export class JobDetailComponent implements OnInit {
  job: any = null;
  errorMessage = '';
  successMessage = '';
  alreadyApplied = false;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private router: Router
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.jobService.getJobById(Number(id)).subscribe({
      next: (res) => { this.job = res; },
      error: () => { this.errorMessage = 'Job not found!'; }
    });
  }

  applyForJob() {
    this.jobService.applyForJob(this.job.jobId).subscribe({
      next: () => {
        this.successMessage = 'Applied successfully!';
        this.alreadyApplied = true;
      },
      error: () => {
        this.errorMessage = 'Already applied for this job!';
      }
    });
  }

  goBack() {
    this.router.navigate(['/jobs']);
  }
}