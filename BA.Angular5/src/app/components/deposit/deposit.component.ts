import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import {Router} from "@angular/router";
import { TransactionServis } from '../../servises/transaction.servis';

@Component({
    selector: 'counter',
    templateUrl: './deposit.component.html',
    providers: [TransactionServis]
})
export class DepositComponent implements OnInit {
    private _transactionServis: TransactionServis;
    private _dataServis: DataServis;
    private _router:Router ;
    public Key: string;
    public ApiAddress: string;
    public AmountPayment:number;

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
    }

    ngOnInit() {
        this.GetKey();
        this.GetApiAddress();
    }

    public Deposit(){
        this._transactionServis.Deposit(this.ApiAddress,this.Key, this.AmountPayment).subscribe(
            data=>{
                this._router.navigate(['home']);
            }
        );
    }

    GetKey() {
        this._dataServis.GetKeyValue().subscribe(Key => {
            this.Key = Key;
        });
    }

    GetApiAddress() {
        this._dataServis.GetApiAddressValue().subscribe(address => {
            this.ApiAddress = address;
        });
    }

    public ifLoginIn():boolean{
        return this._dataServis.isLiginIn();
      }
}
