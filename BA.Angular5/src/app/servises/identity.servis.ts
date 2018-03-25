import { Component, Injectable, Inject, OnInit  } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserIdenity } from '../models/models.user.identity';
import { UserRegister } from '../models/models.user.regiser';
import { DataServis } from './data.servis';

@Injectable()
export class IdentitiServises{
    private static Address:string = "";

    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json; charset=UTF-8',
            })
    };

    constructor(private http: HttpClient,private _dataServise:DataServis) {
        this._dataServise.GetApiAddressValue().subscribe(_address =>{
            IdentitiServises.Address = _address;
        });
     }


    Login(userIdentity: UserIdenity) {
        let userForm = new FormData();
        userForm.append('Username', userIdentity.Username);
        userForm.append('Password', userIdentity.Password);
        console.log(userForm);

        return this.http
            .post(IdentitiServises.Address + "/Identity/Token", userForm);
    }

    Register(UserRegister:UserRegister){
        return this.http
            .post(IdentitiServises.Address + "/api/Users/Register", UserRegister,
            { 
                headers:this.httpOptions.headers
            }
        );
    }
 
}