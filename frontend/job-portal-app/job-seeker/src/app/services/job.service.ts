import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  private apiUrl = 'http://localhost:5152/api/seeker';

  constructor(private http: HttpClient) {}

  getAllJobs(): Observable<any> {
    return this.http.get(`${this.apiUrl}/jobs`);
  }

  getJobById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/jobs/${id}`);
  }

  applyForJob(jobId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/apply`, { jobId });
  }

  getMyApplications(): Observable<any> {
    return this.http.get(`${this.apiUrl}/applications`);
  }

  getDashboard(): Observable<any> {
    return this.http.get(`${this.apiUrl}/dashboard`);
  }
}