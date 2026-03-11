export interface Application {
  applicationId: number;
  jobId: number;
  jobTitle: string;
  companyName: string;
  userId: number;
  applicantName: string;
  applicantEmail: string;
  applicationDate: Date;
  status: string;
}

export interface ApplicationStatusUpdate {
  status: string;
}

export interface DashboardStats {
  totalJobs: number;
  totalUsers: number;
  totalApplications: number;
  activeJobs: number;
}