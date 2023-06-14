import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PeopleComponent } from './people/people.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PeopleManagementComponent } from './people-management/people-management.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatDrawer } from '@angular/material/sidenav';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  { path: '', component: PeopleComponent },
  { path: 'exercise', component: HomeComponent },
  { path: 'try', component: PeopleManagementComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    PeopleComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    PeopleManagementComponent,
    MatGridListModule,
    MatCardModule,
    MatNativeDateModule,
    MatIconModule
  ],
  providers: [MatDrawer],
  bootstrap: [AppComponent]
})
export class AppModule { }
