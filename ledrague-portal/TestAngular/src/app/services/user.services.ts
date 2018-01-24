import { Http, Headers, RequestOptions } from '@angular/http'
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

    addUser(pUsername, pEmail, pPassword) {
        var user = {
            username: pUsername,
            email: pEmail,
            password: pPassword
        }

        console.log("Add user : " + user.username)

        var headers = new Headers();
        headers.append('Content-Type', 'application/json, application/xml, text/plain, */*');
        headers.append('Accept', 'application/json, text/plain');
        let options  = new RequestOptions( { headers: headers } );

        return this.http.post(this.BASE_URL + "/users", user, options).toPromise();
    }
}