import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { PersonViewModel } from "../../models/person-view-model";

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})

export class PeopleComponent implements OnInit {

  people!: PersonViewModel[];
  editing: boolean = false;
  selectedPerson: PersonViewModel | null = null;

  toggleEditing() {
    this.editing = !this.editing;
  }

  selectPerson(person: PersonViewModel): void {
    this.selectedPerson = person;
  }

  savePerson(): void {
    // Save the updated person data to the backend or perform necessary actions
    console.log('Saved:', this.selectedPerson);
    this.editing = false;
    
  }

  closeCard(): void {
    this.selectedPerson = null;
    this.editing = false;
  }

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
  }
}
