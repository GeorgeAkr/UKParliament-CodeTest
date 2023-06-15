import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PeopleManagementComponent } from './people-management.component';
import { PersonViewModel } from '../../models/person-view-model';

describe('PeopleManagementComponent', () => {
  let component: PeopleManagementComponent;
  let fixture: ComponentFixture<PeopleManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        FormsModule,
        MatCardModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatGridListModule,
        MatIconModule,
        MatInputModule,
        MatButtonModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
        MatSidenavModule,
        BrowserAnimationsModule
      ],
      providers: [
        { provide: 'BASE_URL', useValue: 'http://localhost:3000/' }
      ],
      declarations: [],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PeopleManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with default values', () => {
    expect(component.displayedColumns).toEqual(['id', 'name', 'dateOfBirth', 'address', 'delete']);
    expect(component.people).toBeUndefined();
    expect(component.dataSource).toBeDefined();
    expect(component.editing).toBeFalse();
    expect(component.selectedPerson).toBeNull();
    expect(component.originalPerson).toBeNull();
    expect(component.maxDate).toBeInstanceOf(Date);
    expect(component.minDate).toBeInstanceOf(Date);
  });

  it('should toggle editing mode', () => {
    component.toggleEditing();
    expect(component.editing).toBeTrue();

    component.toggleEditing();
    expect(component.editing).toBeFalse();
  });

  it('should cancel editing and restore original person', () => {
    const originalPerson: PersonViewModel = { id: 1, name: 'John Doe', dateOfBirth: new Date(), details: 'Some details', address: '123 Street' };
    const selectedPerson: PersonViewModel = { ...originalPerson };
    component.selectedPerson = selectedPerson;
    component.originalPerson = originalPerson;

    component.cancelEditing();

    expect(component.selectedPerson).toEqual(originalPerson);
    expect(component.editing).toBeFalse();
    expect(component.drawer.opened).toBeFalse();
  });


  it('should select a person and open the drawer', () => {
    const person: PersonViewModel = { id: 1, name: 'John Doe', dateOfBirth: new Date(), details: 'Some details', address: '123 Street' };

    component.selectPerson(person);

    expect(component.selectedPerson).toEqual(person);
    expect(component.drawer.opened).toBeTrue();
  });

  it('should add a new person', () => {
    component.addPerson();

    expect(component.selectedPerson).toEqual({} as PersonViewModel);
    expect(component.editing).toBeTrue();
    expect(component.drawer.opened).toBeTrue();
  });

  it('should save an edited person', () => {
    const person: PersonViewModel = { id: 1, name: 'John Doe', dateOfBirth: new Date(), details: 'Some details', address: '123 Street' };
    spyOn(component as any, 'saveEditedPerson').and.stub();

    component.selectedPerson = person;
    component.savePerson();

    expect((component as any).saveEditedPerson).toHaveBeenCalledWith(person);
  });

  it('should save a new person', () => {
    const person: PersonViewModel = { id: 0, name: 'John Doe', dateOfBirth: new Date(), details: 'Some details', address: '123 Street' };
    spyOn(component as any, 'saveNewPerson').and.stub();

    component.selectedPerson = person;
    component.savePerson();

    expect((component as any).saveNewPerson).toHaveBeenCalledWith(person);
  });

  it('should delete a person', () => {
    const person: PersonViewModel = { id: 1, name: 'John Doe', dateOfBirth: new Date(), details: 'Some details', address: '123 Street' };
    spyOn(component, 'deletePerson').and.stub();

    component.deletePerson(person);

    expect(component.deletePerson).toHaveBeenCalledWith(person);
  });

  it('should close the card and reset editing', () => {
    component.closeCard();

    expect(component.selectedPerson).toBeNull();
    expect(component.editing).toBeFalse();
  });

  //it('should apply a filter to the data source', () => {
  //  const eventMock: any = { target: { value: 'John' } };
  //  spyOn<MatTableDataSource<PersonViewModel>, any>(component.dataSource, 'filter');
  //  spyOn(component.paginator, 'firstPage').and.callThrough();

  //  component.applyFilter(eventMock);

  //  expect(component.dataSource.filter).toBe('john');
  //  expect(component.dataSource.paginator).toBeTruthy();
  //  expect(component.paginator.firstPage).toHaveBeenCalled();
  //});

});
