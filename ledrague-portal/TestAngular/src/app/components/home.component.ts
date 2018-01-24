import { Component } from '@angular/core';

@Component({
  selector: 'home',
  template: `
  <div layout="row" layout-align="center center">
    <mat-spinner mode="indeterminate"></mat-spinner>
  </div>  
  `
})

export class HomeComponent {
}
