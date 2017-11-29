import { Component } from '@angular/core';

@Component({
  selector: 'navbar',
  template: `
  <mat-toolbar>
    <img src="../assets/logopng_web.png"/>
    <button mat-button style="color:white;" routerLink="/">Contrôle des Identités et des Accès</button>
    <button mat-button style="color:white;" routerLink="/register">Register</button>
    <button mat-button style="color:white;" routerLink="/users">Utilisateurs</button>
  </mat-toolbar>
  `
})

export class NavbarComponent {
    constructor() {

    }
}
