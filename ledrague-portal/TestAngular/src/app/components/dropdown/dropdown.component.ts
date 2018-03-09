import { Component } from '@angular/core';
import { ApplicationRightServices } from '../../services/applicationright.services'

@Component({
  selector: 'groups',
  template: `
  <div style="margin-top: 10px;margin-left: 10px">
  <form>
  <mat-form-field>
    <mat-select placeholder="Application" [(ngModel)]="selectedApplication" name="application">
      <mat-option *ngFor="let application of applicationRights" [value]="application.applicationRights">
        {{application.name}}
      </mat-option>
    </mat-select>
  </mat-form-field>

  <p *ngFor="let right of selectedApplication"> 
    <mat-checkbox [(ngModel)]="right.IsChecked" [ngModelOptions]="{standalone: true}">
      {{right.displayName}}
    </mat-checkbox>
  </p>
</form>
</div>
  `
})

export class DropdownComponent {

  selectedApplication;

  constructor(private applicationRightsService: ApplicationRightServices) {
    
  }

  applicationRights = [];
  async ngOnInit() {

    var response = await this.applicationRightsService.getApplicationRights();

    this.applicationRights = response.json();
    console.log(response.json())

  }

}
