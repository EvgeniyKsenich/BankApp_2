import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { FormBuilder, Validators } from '@angular/forms';
import { Component } from '@angular/core';

import { User } from './Models/User';
import { UserRegister } from './Models/UserRegister';
import { Transaction } from './Models/Transaction';
import { UserIdentity } from './Models/UserIdentity';
import { ErrorModel } from './Models/ErrorModel';
import { ModalsModels } from './Models/ModalsModels';

import { UserServises } from './Servises/UserServises';
import { TransactionServises } from './Servises/TransactionServises';
import { IdentitiServises } from './Servises/IdentitiServises';

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
  UserInfo:User = new User();
  UserRegister:UserRegister = new UserRegister();
  TransactionList:Array<Transaction> = new Array<Transaction>();
  UserTransferList:Array<User> = new Array<User>();
  private ApiServerAddress:string = "http://localhost:62733";
  private Key:string = "";
  public AmountPayment:number;
  public selectedValueforTransfer:any;
  public register:boolean = false;
  public ErrorModel: ErrorModel = new ErrorModel();
  public ModalsModels:ModalsModels = new ModalsModels();


  constructor(UserServis:UserServises, TransactionServises:TransactionServises, IdentitiServises: IdentitiServises){
    this._userServises = UserServis;
    this._transactionServises_ = TransactionServises;
    this._identitiServises = IdentitiServises;
  }

  public Login(){
    this._identitiServises.Login(this.ApiServerAddress, this.UserIdentity)
    .subscribe((data: any) => {
      if(data.access_token != null){
        this.SetKey(data.access_token);
        this.GetCurrentUser();
        this.GetTranactioList();
        this.GetUserForTransactions();
        this.SetLoginErrorState(false);
      }
      else{
             
      }
    },
    (error:any) =>
    {
      this.SetLoginErrorState(true);   
    } );
  }

  public Register(){
    this._identitiServises.Register(this.GetAddress(), this.UserRegister).subscribe((data:any) => {
      if(data){
        this.LoginReisterToggle();
        this.SetRegisterErrorState(false);
      }
      else{
        this.SetRegisterErrorState(true);
      }
      console.log(data);
    },
    (error:any)=>{
      this.SetRegisterErrorState(true);
    });;
  }

  public GetCurrentUser(){
      this._userServises.getCurrentUser(this.GetAddress(), this.GetKey()).subscribe((data:any) => {
      this.SetCurrentUserData(data);
    });;
  }

  public GetTranactioList(){
    this._transactionServises_.GetList(this.GetAddress(), this.GetKey()).subscribe((data:Array<Transaction>) => {
      this.TransactionList = data;
    });;;
  }

  public GetUserForTransactions(){
    this._userServises.GetUsersFoTransaction(this.GetAddress(), this.GetKey()).subscribe((data:Array<User>) => {
      this.UserTransferList = data;
    });;;
  }

  public Deposit(){
    this._transactionServises_.Deposit(this.GetAddress(), this.GetKey(), this.GetAmountPyment()).subscribe((data:any) => {
      if(data){
        this.setDepositModal(false);
        this.GetCurrentUser();
        this.GetTranactioList();
        this.ClearAmount();
        this.SetDepositErrorState(false);
      }
      else{
        this.SetDepositErrorState(true);
      }
    }, 
  (error =>{
    this.SetDepositErrorState(true);
  }));
  }

  public Withdraw(){
    this._transactionServises_.Withdraw(this.GetAddress(), this.GetKey(), this.GetAmountPyment()).subscribe((data:any) => {
      if(data){
        this.setWithdrawModal(false);
        this.GetCurrentUser();
        this.GetTranactioList();
        this.ClearAmount();
        this.SetWithdrawErrorState(false);
      }
      else{
        this.SetWithdrawErrorState(true);
      }
    },
    (error =>{
      console.log(error);
      this.SetWithdrawErrorState(true);
    }));
  }

  public Transfer(){
    if(this.selectedValueforTransfer == undefined){
        return -1;
    }

    this._transactionServises_.
    Transfer(this.GetAddress(), this.GetKey(), this.GetAmountPyment(), this.selectedValueforTransfer.userName).subscribe((data:any) => {
      if(data){
        this.setTransferModal(false);
        this.GetCurrentUser();
        this.GetTranactioList();
        this.ClearAmount();
        this.SetTransferErrorState(false);
      }
      else{
        this.SetTransferErrorState(true);
      }
      console.log(data);
    },
    (error =>{
      this.SetTransferErrorState(true);
    }));
  }

  public Logout(){
    this.UserInfo = new User();
    this.Key = "";

    this.UserIdentity = new UserIdentity();
    this.UserInfo = new User();
    this.UserRegister = new UserRegister();
    this.TransactionList = new Array<Transaction>();
    this.UserTransferList= new Array<User>();
    this.AmountPayment = 0;
    this.selectedValueforTransfer = "";
    this.register = false;
  }

  setDepositModal(value:boolean){
    this.ModalsModels.ModalDeposit = value;
  }

  setWithdrawModal(value:boolean){
    this.ModalsModels.ModalWithdraw = value;
  }

  setTransferModal(value:boolean){
    this.ModalsModels.ModalTransfer = value;
  }

  private GetKey():string{
    return this.Key;
  }

  private SetKey(access_token:string){
    this.Key = access_token;
  }

  private GetAddress():string{
    return this.ApiServerAddress;
  }

  private GetAmountPyment():number{
    return this.AmountPayment;
  }

  private SetCurrentUserData(user:any){
    this.UserInfo.Id = user.id;
    this.UserInfo.Name = user.name;
    this.UserInfo.Surname = user.surname;
    this.UserInfo.UserName = user.userName;
    this.UserInfo.Balance =  user.balance;
  }

  GetDate(date:Date){
    var returnDate = new Date(date.getDate(), date.getMonth(), date.getDay(), date.getHours(), date.getMinutes());
    return returnDate;
  }

  SetLoginErrorState(state:boolean){
    this.ErrorModel.LoginError = state;
  }

  SetRegisterErrorState(state:boolean){
    this.ErrorModel.RegisterError = state;
  }

  SetDepositErrorState(state:boolean){
    this.ErrorModel.DepositEror = state;
  }

  SetWithdrawErrorState(state:boolean){
    this.ErrorModel.WithdrawEror = state;
  }

  SetTransferErrorState(state:boolean){
    this.ErrorModel.TransactionEror = state;
  }

  LoginReisterToggle(){
    this.register = !this.register
    this.SetRegisterErrorState(false);
    this.SetLoginErrorState(false);
  }

  ClearAmount(){
    this.AmountPayment = 0;
  }
}
