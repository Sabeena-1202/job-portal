export interface Job {
  jobId: number;
  jobTitle: string;
  companyName: string;
  jobDescription: string;
  location: string;
  salaryRange: string;
  postedDate: Date;
  isActive: boolean;
  totalApplications: number;
}

export interface JobCreateRequest {
  jobTitle: string;
  companyName: string;
  jobDescription: string;
  location: string;
  salaryRange: string;
}