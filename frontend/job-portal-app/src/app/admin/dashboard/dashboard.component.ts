import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin.service';
import { DashboardStats } from '../../core/models/application.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  stats: DashboardStats = {
    totalJobs: 0,
    totalUsers: 0,
    totalApplications: 0,
    activeJobs: 0
  };

  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadStats();
  }

  loadStats(): void {
    this.isLoading = true;
    this.adminService.getDashboardStats().subscribe({
      next: (data) => {
        this.stats = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load dashboard stats.';
        this.isLoading = false;
      }
    });
  }
}