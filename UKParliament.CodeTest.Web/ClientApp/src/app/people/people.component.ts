import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { PersonViewModel } from "../../models/person-view-model";

import { MatTableDataSource, MatSort } from '@angular/material';
import { DataSource } from '@angular/cdk/table';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})
export class PeopleComponent implements OnInit {

  people!: PersonViewModel[];
  dataSource: MatTableDataSource<PersonViewModel> | undefined;
  displayedColumns: string[] = ['position', 'name'];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getPersonById(1);
    //this.getPeople();
  }

  ngOnInit(): void {
    this.getPeople();
  }

  getPersonById(id: number): void {
    this.http.get<PersonViewModel[]>(this.baseUrl + `api/person/${id}`).subscribe(result => {
    }, error => console.error(error));
  }

  getPeople(): void {
    this.http.get<PersonViewModel[]>(this.baseUrl + `api/person/getall`).subscribe(result => {
      this.setPeople(result);
    }, error => console.error(error));
  }

  private setPeople(res: PersonViewModel[]): void {
    this.people = res;
    this.dataSource = new MatTableDataSource(this.people);
  }
}
