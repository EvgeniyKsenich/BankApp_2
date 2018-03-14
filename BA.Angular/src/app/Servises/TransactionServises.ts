import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Transaction } from '../Models/Transaction';

@Injectable()
export class TransactionServises {
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
     
    constructor(private http: HttpClient) {   }

    getList(address:string){
        return this.http
        .post<Array<Transaction>>("/api/Users/Transactions", {
            headers: this.httpOptions.headers
        });
    }
}