import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Transaction } from '../models/models.transaction';
import { DataServis } from './data.servis';

@Injectable()
export class TransactionServis {
    private static Address:string = "";
    private static Token:string = "";

    httpOptions = {
        ContentType: 'application/json; charset=UTF-8'
    };
     
    constructor(private http: HttpClient,private _dataServise:DataServis) {
        this._dataServise.GetApiAddressValue().subscribe(_address =>{
            TransactionServis.Address = _address;
        });
        this._dataServise.GetTokenValue().subscribe(_token =>{
            TransactionServis.Token = _token;
        });
    }

    GetList(){
        return this.http
        .post<Array<Transaction>>(TransactionServis.Address + "/api/Users/Transactions", {}, {
            headers:{
                Authorization:"Bearer " + TransactionServis.Token
            }
        });
    }

    Deposit(amount:number){
        let form = new FormData();
        form.append('amount', amount.toString());
        return this.http
        .post(TransactionServis.Address  + "/api/Transaction/Deposit", form , {
            headers:{
                Authorization:"Bearer " + TransactionServis.Token 
            }
        });
    }

    Withdraw(amount:number){
        let form = new FormData();
        form.append('amount', amount.toString());
        return this.http
        .post(TransactionServis.Address  + "/api/Transaction/Withdraw", form , {
            headers:{
                Authorization:"Bearer " + TransactionServis.Token 
            }
        });
    }

    Transfer(amount:number, UserReceiverName:string){
        let form = new FormData();
        form.append('amount', amount.toString());
        form.append('UserReceiverName', UserReceiverName);
        return this.http
        .post(TransactionServis.Address  + "/api/Transaction/Transfer", form , {
            headers:{
                Authorization:"Bearer " + TransactionServis.Token 
            }
        });
    }

}