import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";

import { Component } from '@angular/core';
import { User } from './Models/User';
import { Transaction } from './Models/Transaction';
import { UserIdentity } from './Models/UserIdentity';

import { UserServises } from './Servises/UserServises';
import { TransactionServises } from './Servises/TransactionServises';
import { IdentitiServises } from './Servises/IdentitiServises';
import { isUndefined } from "util";

@Component({
  selector: 'app-root',
  templateUrl: './template/app.component.html',
  styleUrls: ['./style/app.component.css', './style/bootstrap.min.css'],
  providers: [UserServises, TransactionServises, IdentitiServises]
})

export class AppComponent {
  private _userServises: UserServises;
  private _transactionServises_:TransactionServises;
  private _identitiServises: IdentitiServises;
  
  UserIdentity:UserIdentity = new UserIdentity();
  User:User;
  private ApiServerAddress:string = "http://localhost:62733";
  private Key:string = "";

  constructor(UserServis:UserServises, TransactionServises:TransactionServises, IdentitiServises: IdentitiServises){
    this._userServises = UserServis;
    this._transactionServises_ = TransactionServises;
    this._identitiServises = IdentitiServises;
    console.log(this.User);
  }

  public Login(){
    console.log(this.User);
    this._identitiServises.login(this.ApiServerAddress, this.UserIdentity)
    .subscribe((data: any) => {
      if(data.access_token != null){
        this.SetKey(data.access_token);
        this.GetCurrentUser();
      }
      else{
        
      }
    });
  }

  public GetCurrentUser(){
      this._identitiServises.getCurrentUser(this.ApiServerAddress, this.Key).subscribe((data:User) => {
      this.User = data;
    });;
  }

  public GetUsername(){
    if(this.User != undefined)
      return this.User.UserName;
  }

  public Logout(){
    this.User = new User();
    this.Key = "";
  }

  private GetKey(){

  }

  private SetKey(access_token:string){
    this.Key = access_token;
  }
}
