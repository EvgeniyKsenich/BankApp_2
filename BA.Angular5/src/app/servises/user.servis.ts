import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { DataServis } from './data.servis';


@Injectable()
export class UserServises {
    private static Address:String = "";
    private static Token:string = "";

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
     
    constructor(private http: HttpClient, private _dataServis:DataServis) {  
        this._dataServis.GetApiAddressValue().subscribe(_address =>{
            UserServises.Address = _address;
        });
        this._dataServis.GetTokenValue().subscribe(_token =>{
            UserServises.Token = _token;
        });
     }


    getCurrentUser(){
        return this.http
            .post(UserServises.Address + "/api/Users/GetCurrentUser", {},
        {
            headers:{
                Authorization:"Bearer " + UserServises.Token
            }
        });
    }

    GetUsersForTransaction(){
        return this.http
            .get(UserServises.Address + "/api/Users",
        {
            headers:{
                Authorization:"Bearer " + UserServises.Token
            }
        });
    }

}