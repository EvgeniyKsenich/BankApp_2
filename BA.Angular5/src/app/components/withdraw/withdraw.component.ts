import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import { TransactionServis } from '../../servises/transaction.servis';
import { Router } from '@angular/router';

@Component({
    selector: 'counter',
    templateUrl: './withdraw.component.html',
    providers: [TransactionServis]
})
export class WithdrawComponent implements OnInit{
    private _transactionServis: TransactionServis;
    private _dataServis: DataServis;
    private _router:Router ;
    public Key: string;
    public ApiAddress: string;
    public AmountPayment:number;
    public errorMessage:string = "";

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
    }

    ngOnInit() {
        this.GetKey();
        this.GetApiAddress();
    }

    public Withdraw(){
        this._transactionServis.Withdraw(this.ApiAddress,this.Key, this.AmountPayment).subscribe(
            data=>{
                this.errorMessage = "";
                this._router.navigate(['home']);
            },
            error =>{
                this.errorMessage = error.error;
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

    isError():boolean{
        if(this.errorMessage == "")
            return false;

        return true;
    }
}