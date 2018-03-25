import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import {Router} from "@angular/router";
import { TransactionServis } from '../../servises/transaction.servis';
import { UserServises } from '../../servises/user.servis';
import { UserInfo } from '../../models/models.user.info';

@Component({
    selector: 'counter',
    templateUrl: './deposit.component.html',
    providers: [TransactionServis, UserServises]
})
export class DepositComponent implements OnInit {
    private _transactionServis: TransactionServis;
    private _userServis: UserServises;
    private _dataServis: DataServis;
    private _router:Router ;
    public Key: string;
    public ApiAddress: string;
    public AmountPayment:number;
    public User: UserInfo;
    public ErrorMessage: string = "";

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis,
        userServises:UserServises) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
        this._userServis = userServises;
    }

    ngOnInit() {
        this.GetCurrentUser();
    }

    public Deposit(){
        this._transactionServis.Deposit(this.AmountPayment).subscribe(
            data=>{
                this.ErrorMessage = "";
                this._router.navigate(['home']);
            },
            error => {
                this.ErrorMessage = error.error;
            }
        );
    }

    GetCurrentUser() {
        this._userServis.getCurrentUser().subscribe( (user:any) => {
            this.User = user;
        });
    }

    public ifLoginIn():boolean{
        return this._dataServis.isLiginIn();
      }

      public isLoaded():boolean{
        if(this.User == null || this.User == undefined )
            return false;
        
        return true;
      }

      public isError(){
        if(this.ErrorMessage != "")
            return true;

        return false;
    }
}
