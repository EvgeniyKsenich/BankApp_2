import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Transaction } from '../Models/Transaction';

@Injectable()
export class TransactionServises {
    httpOptions = {
        ContentType: 'application/json; charset=UTF-8'
    };
     
    constructor(private http: HttpClient) {   }

    GetList(address:string, key:string){
        return this.http
        .post<Array<Transaction>>(address + "/api/Users/Transactions", {}, {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

    Deposit(address:string, key:string, amount:number){
        let form = new FormData();
        form.append('amount', amount.toString());
        return this.http
        .post(address + "/api/Transaction/Deposit", form , {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

    Withdraw(address:string, key:string, amount:number){
        let form = new FormData();
        form.append('amount', amount.toString());
        return this.http
        .post(address + "/api/Transaction/Withdraw", form , {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

    Transfer(address:string, key:string, amount:number, UserReceiverName:string){
        let form = new FormData();
        form.append('amount', amount.toString());
        form.append('UserReceiverName', UserReceiverName);
        return this.http
        .post(address + "/api/Transaction/Transfer", form , {
            headers:{
                Authorization:"Bearer " + key
            }
        });
    }

}