import { Component } from '@angular/core';
import { TransactionServis } from '../../servises/transaction.servis';
import { DataServis } from '../../servises/data.servis';
import { Router } from '@angular/router';
import { UserInfo } from '../../models/models.user.info';
import { UserServises } from '../../servises/user.servis';

@Component({
    selector: 'counter',
    templateUrl: './transfer.component.html',
    providers:[TransactionServis, UserServises]
})
export class TransferComponent {
    private _userServis: UserServises;
    private _transactionServis: TransactionServis;
    private _dataServis: DataServis;
    private _router:Router ;

    public Key: string;
    public ApiAddress: string;
    public AmountPayment:number;
    public selectedValueforTransfer:any;
    public UserTransferList:Array<UserInfo> = new Array<UserInfo>();
    public errorMessage: string = "";
    public User;

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis,userServises:UserServises) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
        this._userServis = userServises;
    }

    ngOnInit() {
        this.GetKey();
        this.GetApiAddress();
        this.GetCurrentUser();
        this.GetUsersForTransfer();
    }

    public Transfer(){
        if(this.selectedValueforTransfer == undefined){
            this.errorMessage = "Select user to transfer";
            return -1;
        }

        this._transactionServis.Transfer
        (this.ApiAddress,this.Key, this.AmountPayment, this.selectedValueforTransfer.userName).subscribe(
            data=>{
                this._router.navigate(['home']);
                console.log(data);
            },
            error =>{
                this.errorMessage = error.error;
            }
        );
    }

    GetCurrentUser() {
        this._userServis.getCurrentUser(this.ApiAddress, this.Key).subscribe( (user:any) => {
            this.User = user;
            console.log(user);
        });
    }

    GetUsersForTransfer(){
        this._userServis.GetUsersFoTransaction(this.ApiAddress, this.Key).subscribe((data:Array<UserInfo>) => {
            this.UserTransferList = data;
          });
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

    public isLoaded():boolean{
        if(this.User == null || this.User == undefined ||
           this.UserTransferList == null || this.UserTransferList == undefined)
            return false;
        
        return true;
      }

      isError():boolean{
        if(this.errorMessage == "")
            return false;

        return true;
    }
}
