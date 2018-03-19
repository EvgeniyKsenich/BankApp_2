import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../models/models.transaction';
import { TransactionServis } from '../../servises/transaction.servis';
import { DataServis } from '../../servises/data.servis';
import { UserInfo } from '../../models/models.user.info';
import { UserServises } from '../../servises/user.servis';
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    providers:[
        TransactionServis,
        UserServises
    ]
})
export class HomeComponent implements OnInit{
    private _router: Router;
    private _userservis: UserServises;
    private _transactionServis:TransactionServis;
    private _dataServis:DataServis;

    private User:UserInfo = null;
    private Key:string;
    private ApiAddress:string;

    TransactionList:Array<Transaction>;

    constructor(transactionServis:TransactionServis,
                dataServis:DataServis, 
                userservis:UserServises,
                router: Router){
        this._transactionServis = transactionServis;
        this._dataServis = dataServis;
        this._userservis = userservis;
        this._router = router;
    }

    ngOnInit(){
        this.GetKey();
        this.GetApiAddress();
        this.GetCurrentUser();
        this.GetTransactions();
    }

    GetTransactions(){
        this._transactionServis.GetList(this.ApiAddress,this.Key).subscribe(list =>{
            this.TransactionList = list;
        });
    }

    GetKey(){
        var keyValue = "";
        this._dataServis.GetKeyValue().subscribe(key => {
            this.Key = key;
        });
    }

    GetCurrentUser() {
        this._userservis.getCurrentUser(this.ApiAddress, this.Key).subscribe( (user:any) => {
            this.User = user;
            console.log(user);
        });
    }

    GetApiAddress() {
         this._dataServis.GetApiAddressValue().subscribe(address => {
            this.ApiAddress =  address;
        });
    }

    public ifLoginIn():boolean{
        if(this.User)
            return true;
        
        return false;
      } 
      
      public Logout(){
        this._dataServis.SetKey('');
        this._dataServis.SetUserInfo(null);
        this._router.navigate(['login']);
      }
}
