import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../core/services/admin.service';
import { Job, JobCreateRequest } from '../../core/models/job.model';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {

  jobs: Job[] = [];
  jobForm: FormGroup;
  isLoading: boolean = false;
  isFormLoading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';
  showModal: boolean = false;
  isEditMode: boolean = false;
  selectedJobId: number | null = null;

  constructor(
    private adminService: AdminService,
    private fb: FormBuilder
  ) {
    this.jobForm = this.fb.group({
      jobTitle: ['', Validators.required],
      companyName: ['', Validators.required],
      jobDescription: ['', Validators.required],
      location: ['', Validators.required],
      salaryRange: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.isLoading = true;
    this.adminService.getAllJobs().subscribe({
      next: (data) => {
        this.jobs = data;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load jobs.';
        this.isLoading = false;
      }
    });
  }

  openAddModal(): void {
    this.isEditMode = false;
    this.selectedJobId = null;
    this.jobForm.reset();
    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  openEditModal(job: Job): void {
    this.isEditMode = true;
    this.selectedJobId = job.jobId;
    this.jobForm.patchValue({
      jobTitle: job.jobTitle,
      companyName: job.companyName,
      jobDescription: job.jobDescription,
      location: job.location,
      salaryRange: job.salaryRange
    });
    this.errorMessage = '';
    this.successMessage = '';
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.jobForm.reset();
  }

  onSubmit(): void {
    if (this.jobForm.invalid) return;

    this.isFormLoading = true;
    const jobData: JobCreateRequest = this.jobForm.value;

    if (this.isEditMode && this.selectedJobId) {
      this.adminService.updateJob(this.selectedJobId, jobData).subscribe({
        next: () => {
          this.successMessage = 'Job updated successfully!';
          this.isFormLoading = false;
          this.closeModal();
          this.loadJobs();
        },
        error: () => {
          this.errorMessage = 'Failed to update job.';
          this.isFormLoading = false;
        }
      });
    } else {
      this.adminService.createJob(jobData).subscribe({
        next: () => {
          this.successMessage = 'Job created successfully!';
          this.isFormLoading = false;
          this.closeModal();
          this.loadJobs();
        },
        error: () => {
          this.errorMessage = 'Failed to create job.';
          this.isFormLoading = false;
        }
      });
    }
  }

  deleteJob(jobId: number): void {
    if (!confirm('Are you sure you want to delete this job?')) return;

    this.adminService.deleteJob(jobId).subscribe({
      next: () => {
        this.successMessage = 'Job deleted successfully!';
        this.loadJobs();
      },
      error: () => {
        this.errorMessage = 'Failed to delete job.';
      }
    });
  }
}