import { Component, Injectable, Inject  } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserIdenity } from '../models/models.user.identity';
import { UserRegister } from '../models/models.user.regiser';

@Injectable()
export class IdentitiServises {
    
    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json; charset=UTF-8',
            })
    };

    constructor(private http: HttpClient) { }

    Login(address: string, userIdentity: UserIdenity) {
        let userForm = new FormData();
        userForm.append('Username', userIdentity.Username);
        userForm.append('Password', userIdentity.Password);
        console.log(userForm);

        return this.http
            .post(address + "/Identity/Token", userForm);
    }

    Register(address:string, UserRegister:UserRegister){
        return this.http
            .post(address + "/api/Users/Register", UserRegister,
            { 
                headers:this.httpOptions.headers
            }
        );
    }
 
}