import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { HiveSection } from '../models/hive-section';
import { HiveSectionListItem } from '../models/hive-section-list-item';

export interface UpdateHiveSectionRequest {
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class HiveSectionService {
  private url = environment.apiUrl + 'api/sections/';

  constructor(private http: HttpClient) { }

  getHiveSections(): Observable<Array<HiveSectionListItem>> {
    return this.http.get<Array<HiveSectionListItem>>(this.url);
  }

  getHiveSection(hiveSectionId: number): Observable<HiveSection> {
    return this.http.get<HiveSection>(`${this.url}${hiveSectionId}`);
  }

  setHiveSectionStatus(hiveSectionId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.put(`${this.url}${hiveSectionId}/status/${deletedStatus}`, null);
  }

  addHiveSection(request: UpdateHiveSectionRequest): Observable<Object> {
    return this.http.post(this.url, request);
  }

  updateHiveSection(hiveSectionId: number, request: UpdateHiveSectionRequest): Observable<Object> {
    return this.http.put(`${this.url}${hiveSectionId}`, request);
  }

  deleteHiveSection(hiveSectionId: number): Observable<Object> {
    return this.http.delete(`${this.url}${hiveSectionId}`);
  }
}
