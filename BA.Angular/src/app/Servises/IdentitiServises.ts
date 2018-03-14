import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Transaction } from '../Models/Transaction';
import { UserIdentity } from '../Models/UserIdentity';

@Injectable()
export class IdentitiServises {
    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
            })
    };
     
    constructor(private http: HttpClient) {   }

    login(address:string, userIdentity:UserIdentity){

        let userForm = new FormData();
        userForm.append('Username', userIdentity.Username);
        userForm.append('Password', userIdentity.Password);
        console.log(userForm);

        return this.http
            .post(address + "/Identity/Token", userForm);
    }

    getCurrentUser(address:string, key:string){
        return this.http
            .post(address + "/api/Users/GetCurrentUser", {},
        {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }
}