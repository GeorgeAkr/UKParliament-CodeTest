import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { PersonViewModel } from '../../models/person-view-model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-people-management',
  templateUrl: './people-management.component.html',
  styleUrls: ['./people-management.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatTableModule, MatSortModule, MatPaginatorModule,
    MatGridListModule, MatCardModule, MatSidenavModule, MatIconModule, MatDatepickerModule, MatButtonModule,
    FormsModule, CommonModule
  ]
})

export class PeopleManagementComponent implements AfterViewInit {
  displayedColumns: string[] = ['id', 'name', 'dateOfBirth', 'address', 'delete'];
  people!: PersonViewModel[];
  dataSource: MatTableDataSource<PersonViewModel>;
  editing: boolean = false;
  selectedPerson: PersonViewModel | null = null;
  originalPerson: PersonViewModel | null = null;
  maxDate: Date = new Date();
  minDate: Date = new Date(this.maxDate.getFullYear() - 100, this.maxDate.getMonth(), this.maxDate.getDate());

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatDrawer) drawer!: MatDrawer;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.dataSource = new MatTableDataSource();

  }

  ngAfterViewInit() {
    this.getPeople();
  }

  toggleEditing() {
    this.editing = !this.editing;
  }

  cancelEditing() {
    if (this.selectedPerson !== null && this.selectedPerson.id !== undefined && this.selectedPerson.id != 0) {
      const copy = structuredClone(this.originalPerson);
      this.selectedPerson = copy;
    }
    else {
      this.selectedPerson = null;
      this.drawer.toggle();
    }
    this.editing = false;
  }

  selectPerson(person: PersonViewModel): void {
    const copy = structuredClone(person);
    this.originalPerson = person;
    this.selectedPerson = copy;
    this.drawer.toggle();
  }

  addPerson(): void {
    this.selectedPerson = <PersonViewModel>{};
    this.editing = true;
    this.drawer.toggle();
  }

  savePerson(): void {
    if (this.selectedPerson !== null && this.selectedPerson.id !== undefined && this.selectedPerson.id != 0) {
      this.saveEditedPerson(this.selectedPerson);
    }
    else if (this.selectedPerson !== null) {
      this.saveNewPerson(this.selectedPerson);
    }
  }

  private saveEditedPerson(person: PersonViewModel): void {
    this.http.put<PersonViewModel>(this.baseUrl + `api/person/person`, person).subscribe(result => {
      if (result == null) {
        const index = this.people.indexOf(this.originalPerson!);
        this.people[index] = person;
        this.setPeople(this.people);
        this.editing = false;
      }
    }, error => console.error(error));
  }

  private saveNewPerson(person: PersonViewModel): void {
    this.http.post<PersonViewModel>(this.baseUrl + `api/person/person`, person).subscribe(result => {
      console.log(`Successfully saved new user with id:${result.id}`);
      this.people.push(this.selectedPerson!);
      this.setPeople(this.people);
      this.editing = false;
    }, error => console.error(error));
  }

  deletePerson(person: PersonViewModel) {
    this.http.delete(this.baseUrl + `api/person/${person.id}`).subscribe(result => {
      if (result === true) {
        console.log(`Successfully deleted user with id:${person.id}`);
        const index = this.people.indexOf(person, 0);
        if (index > -1) {
          this.people.splice(index, 1);
          this.setPeople(this.people);
        }
      }
    }, error => console.error(error));
  }

  closeCard(): void {
    this.selectedPerson = null;
    this.editing = false;
  }

  getPeople(): void {
    this.http.get<PersonViewModel[]>(this.baseUrl + `api/person`).subscribe(result => {
      this.setPeople(result);
    }, error => console.error(error));
  }

  private setPeople(res: PersonViewModel[]): void {
    this.people = res;
    this.dataSource = new MatTableDataSource(this.people);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
