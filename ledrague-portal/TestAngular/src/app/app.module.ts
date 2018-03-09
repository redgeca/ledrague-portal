import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorIntl,
         MatButtonModule,
         MatInputModule,
         MatSnackBarModule,
         MatToolbarModule,
         MatCardModule, 
         MatIconModule,
         MatTableModule,
         MatPaginatorModule,
         MatSelectModule,
         MatCheckboxModule,
         MatAutocompleteModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar.component'
import { RegisterComponent } from './components/register.component'
import { HomeComponent } from './components/home.component'
import { UserListComponent } from './components/userlist.component'
import { UserServices } from './services/user.services';
import { ApplicationRightServices } from './services/applicationright.services'
import { Http } from '@angular/http';
import { HttpModule } from '@angular/http';
import { MatExpansionModule } from '@angular/material/expansion';
import { UserComponent } from './components/user.component';
import { Routes } from '@angular/router/src/config';
import { LoginComponent} from './components/login/login.component' 
import { DropdownComponent} from './components/dropdown/dropdown.component' ;
import { AutocompleteComponent } from './components/autocomplete/autocomplete.component';

const routes:Routes = [
  {
    path: '',
    component: HomeComponent
  }, 
  {
    path: 'register',
    component: RegisterComponent
  }, 
  {
    path: 'users',
    component: UserListComponent
  }, 
  {
    path: 'user/:id',
    component: UserComponent
  },
  {
    path: 'user',
    component: UserComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'autocomplete',
    component: AutocompleteComponent
  },
  {
    path: 'dropdown',
    component: DropdownComponent
  },
  {
    path: '**',
    component: HomeComponent
  } 
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    UserListComponent,
    UserComponent,
    LoginComponent,
    DropdownComponent,
    AutocompleteComponent
  ],
  imports: [
    BrowserModule, 
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatIconModule,
    HttpModule,
    MatCardModule, 
    MatExpansionModule, 
    MatTableModule,
    MatSelectModule,
    FormsModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MatAutocompleteModule,
    MatCheckboxModule
  ],
  providers: [{ provide: MatPaginatorIntl, useClass: UserListComponent}, UserServices, ApplicationRightServices ],
  bootstrap: [AppComponent]
})
export class AppModule { }
