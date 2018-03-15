import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { User } from '../Models/User';

@Injectable()
export class UserServises {
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
     
    constructor(private http: HttpClient) {   }


    getCurrentUser(address:string, key:string){
        return this.http
            .post(address + "/api/Users/GetCurrentUser", {},
        {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

    GetUsersFoTransaction(address:string, key:string){
        return this.http
            .get(address + "/api/Users",
        {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

}