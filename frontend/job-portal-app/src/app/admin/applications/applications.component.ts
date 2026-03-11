import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin.service';
import { Application } from '../../core/models/application.model';

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.css']
})
export class ApplicationsComponent implements OnInit {

  applications: Application[] = [];
  filteredApplications: Application[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';
  searchTerm: string = '';
  selectedStatus: string = '';

  statusOptions = ['Applied', 'Under Review', 'Selected', 'Rejected'];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    this.isLoading = true;
    this.adminService.getAllApplications().subscribe({
      next: (data) => {
        this.applications = data;
        this.filteredApplications = data;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load applications.';
        this.isLoading = false;
      }
    });
  }

  onSearch(): void {
    this.applyFilters();
  }

  onFilterStatus(): void {
    this.applyFilters();
  }

  applyFilters(): void {
    let result = this.applications;

    if (this.searchTerm) {
      const term = this.searchTerm.toLowerCase();
      result = result.filter(a =>
        a.applicantName.toLowerCase().includes(term) ||
        a.jobTitle.toLowerCase().includes(term) ||
        a.companyName.toLowerCase().includes(term)
      );
    }

    if (this.selectedStatus) {
      result = result.filter(a => a.status === this.selectedStatus);
    }

    this.filteredApplications = result;
  }

  updateStatus(applicationId: number, status: string): void {
    this.adminService.updateApplicationStatus(applicationId, { status }).subscribe({
      next: () => {
        this.successMessage = 'Application status updated successfully!';
        // Update locally without reloading
        const app = this.applications.find(a => a.applicationId === applicationId);
        if (app) app.status = status;
        this.applyFilters();
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: () => {
        this.errorMessage = 'Failed to update status.';
        setTimeout(() => this.errorMessage = '', 3000);
      }
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Applied': return 'badge-applied';
      case 'Under Review': return 'badge-review';
      case 'Selected': return 'badge-selected';
      case 'Rejected': return 'badge-rejected';
      default: return '';
    }
  }
}