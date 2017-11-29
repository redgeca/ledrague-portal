import { Http} from '@angular/http'
import { Injectable } from '@angular/core'
import 'rxjs/add/operator/toPromise';

@Injectable()
export class UserServices {

    BASE_URL = "https://localhost:44349/api";

    constructor(private http: Http) {

    }

    getUsers() {
        return this.http.get(this.BASE_URL + "/users").toPromise();        
    }

    addUser() {
//        return this.http.get(this.BASE_URL + "/users").toPromise();        
    }
}