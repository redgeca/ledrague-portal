import { Component } from '@angular/core';
import { FormControl } from '@angular/forms'

@Component({
    selector: 'autocomplete',
    template: `
  <div layout="row" layout-align="center center">
    <form class="example-form">
        <mat-form-field class="example-full-width">
            <input type="text" placeholder="search word" aria-label="Number" matInput [formControl]="searchTerm" [matAutocomplete]="auto">
            <mat-autocomplete #auto="matAutocomplete">
            <mat-option *ngFor="let item of searchResult" [value]="item">
                {{ item }}
            </mat-option>
            </mat-autocomplete>
        </mat-form-field>
    </form>
  </div>  
  `
})

export class AutocompleteComponent {

    searchTerm : FormControl = new FormControl();
    
    searchResult = [
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10"
    ];
    
}
