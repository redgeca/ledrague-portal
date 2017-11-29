import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule,
         MatInputModule,
         MatSnackBarModule,
         MatToolbarModule,
         MatCardModule, 
         MatIconModule,
        MatSelectModule  } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar.component'
import { RegisterComponent } from './components/register.component'
import { HomeComponent } from './components/home.component'
import { UserListComponent } from './components/userlist.component'
import { UserServices } from './services/user.services';
import { Http } from '@angular/http';
import { HttpModule } from '@angular/http';
import { MatExpansionModule } from '@angular/material/expansion';
import { UserComponent } from './components/user.component';

var routes = [
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
  } 
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    UserListComponent,
    UserComponent
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
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [ UserServices ],
  bootstrap: [AppComponent]
})
export class AppModule { }
