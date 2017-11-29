import { Component } from '@angular/core';
import { UserServices } from '../services/user.services';
import { ActivatedRoute } from '@angular/router'

@Component({
  moduleId: module.id,
  selector: 'userlist',
  templateUrl: 'userlist.component.html'
})

export class UserListComponent {
  constructor (private userServices: UserServices) {
}

users = [];

async ngOnInit() {
    var response = await this.userServices.getUsers();
    var response = await this.userServices.getUsers();
    console.log(response.json());
    this.users = response.json();
  }
}
