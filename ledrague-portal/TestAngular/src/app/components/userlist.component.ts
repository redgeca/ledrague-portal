import { Component } from '@angular/core';
import { UserServices } from '../services/user.services';
import { ActivatedRoute } from '@angular/router'
import { MatPaginatorIntl} from '@angular/material';
import { MatTableDataSource} from '@angular/material';

@Component({
  moduleId: module.id,
  selector: 'userlist',
  templateUrl: 'userlist.component.html'
})

export class UserListComponent extends MatPaginatorIntl{
  constructor (private userServices: UserServices) {
    super();
}

itemsPerPageLabel = 'Utilisateurs par page';
nextPageLabel     = 'Page suivante';
previousPageLabel = 'Pagre précédente';

users:Element [];

dataSource = new MatTableDataSource<Element>(this.users);

async ngOnInit() {
    var response = await this.userServices.getUsers();
    console.log(response.json());
    this.users = response.json();
  }
}
