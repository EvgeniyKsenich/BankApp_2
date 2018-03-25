import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import { TransactionServis } from '../../servises/transaction.servis';
import { Router } from '@angular/router';
import { UserServises } from '../../servises/user.servis';
import { UserInfo } from '../../models/models.user.info';

@Component({
    selector: 'counter',
    templateUrl: './withdraw.component.html',
    providers: [TransactionServis, UserServises]
})
export class WithdrawComponent implements OnInit{
    private _userServis: UserServises;
    private _transactionServis: TransactionServis;
    private _dataServis: DataServis;
    private _router:Router ;
    public Key: string;
    public ApiAddress: string;
    public AmountPayment:number;
    public ErrorMessage:string = "";
    public User: UserInfo;

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis,
        userServises:UserServises) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
        this._userServis = userServises;
    }

    ngOnInit(): void {
        this.GetCurrentUser();
    }

    public Withdraw(){
        this._transactionServis.Withdraw(this.AmountPayment).subscribe(
            data=>{
                this.ErrorMessage = "";
                this._router.navigate(['home']);
            },
            error => {
                console.log(error);
                this.ErrorMessage = error.error;
            }
        );
    }

    GetCurrentUser() {
        this._userServis.getCurrentUser().subscribe( (user:any) => {
            this.User = user;
        });
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