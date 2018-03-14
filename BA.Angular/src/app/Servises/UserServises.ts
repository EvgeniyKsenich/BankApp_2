import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { User } from '../Models/User';

@Injectable()
export class UserServises {
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
     
    constructor(private http: HttpClient) {   }


    getCurrent(address:string) {
        var result = this.http.get<User>(address + '/api/Users/GetCurrentUser' );
        return result;
    }

    register(address:string, _item: User) {
        return this.http
            .post("/api/Users/register", _item, {
                headers: this.httpOptions.headers
            });
    }

    getOtherList(address:string){
        return this.http
        .post<Array<User>>("/api/Users/Get", {
            headers: this.httpOptions.headers
        });
    }
}