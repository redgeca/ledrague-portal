import { Injectable } from "@angular/core";
import { Http } from '@angular/http';
import { HttpClient } from "@angular/common/http";

@Injectable()
export class AuthService {

    constructor(private httpClient : HttpClient) {

    }
    public login(username: string, password: string) {
        return "";
    }    
}