import { Http } from '@angular/http'
import { Injectable } from '@angular/core'
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ApplicationRightServices {

    BASE_URL = "https://localhost:44349/api";

    constructor(private http: Http) {

    }

    getApplicationRights() {
        return this.http.get(this.BASE_URL + '/ApplicationRights').toPromise()         
    }
}