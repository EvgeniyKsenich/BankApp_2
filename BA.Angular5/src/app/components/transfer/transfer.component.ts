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

    public AmountPayment:number;
    public selectedValueforTransfer:any;
    public UserTransferList:Array<UserInfo> = new Array<UserInfo>();
    public User;
    public ErrorMessage: string = "";

    constructor(dataServis: DataServis, router: Router,transactionServis:TransactionServis,userServises:UserServises) {
        this._dataServis = dataServis;
        this._router = router;
        this._transactionServis = transactionServis;
        this._userServis = userServises;
    }

    ngOnInit() {
        this.GetCurrentUser();
        this.GetUsersForTransfer();
    }

    public Transfer(){
        if(this.selectedValueforTransfer == undefined){
            return -1;
        }

        this._transactionServis.Transfer
        (this.AmountPayment, this.selectedValueforTransfer.userName).subscribe(
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

    GetUsersForTransfer(){
        this._userServis.GetUsersForTransaction().subscribe((data:Array<UserInfo>) => {
            this.UserTransferList = data;
          });
    }

    public isLoaded():boolean{
        if(this.User == null || this.User == undefined ||
           this.UserTransferList == null || this.UserTransferList == undefined)
            return false;
        
        return true;
      }

      public isError(){
        if(this.ErrorMessage != "")
            return true;

        return false;
    }
}
