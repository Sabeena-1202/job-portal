import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Job, JobCreateRequest } from '../models/job.model';
import { Application, ApplicationStatusUpdate, DashboardStats } from '../models/application.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private apiUrl = 'https://localhost:7001/api/admin';

  constructor(private http: HttpClient) {}

  
  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}/dashboard`);
  }

 
  getAllJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(`${this.apiUrl}/jobs`);
  }

  getJobById(jobId: number): Observable<Job> {
    return this.http.get<Job>(`${this.apiUrl}/jobs/${jobId}`);
  }

  createJob(job: JobCreateRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/jobs`, job);
  }

  updateJob(jobId: number, job: JobCreateRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/jobs/${jobId}`, job);
  }

  deleteJob(jobId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/jobs/${jobId}`);
  }


  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/users`);
  }

  getAllApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/applications`);
  }

  updateApplicationStatus(applicationId: number, status: ApplicationStatusUpdate): Observable<any> {
    return this.http.put(`${this.apiUrl}/applications/${applicationId}/status`, status);
  }
}