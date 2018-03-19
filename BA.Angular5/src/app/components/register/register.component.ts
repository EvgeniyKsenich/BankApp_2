import { Component, OnInit } from '@angular/core';
import { UserRegister } from '../../models/models.user.regiser';
import { Router } from '@angular/router';
import { UserServises } from '../../servises/user.servis';
import { DataServis } from '../../servises/data.servis';
import { IdentitiServises } from '../../servises/identity.servis';


@Component({
    selector: 'counter',
    templateUrl: './register.component.html',
    providers:[IdentitiServises]
})
export class RegisterComponent implements OnInit{
    private UserRegister: UserRegister = new UserRegister();
    private _router: Router;
    private _identitiServises: IdentitiServises;
    private _dataServis:DataServis;
    private ApiAddress:string;

    constructor(identitiServises: IdentitiServises,
                router: Router,
                dataServis:DataServis){
        this._identitiServises = identitiServises;
        this._router = router;
        this._dataServis = dataServis;
    }

    ngOnInit(){
        this.GetApiAddress();
    }

    Register(){
        this._identitiServises.Register(this.ApiAddress, this.UserRegister).subscribe(
            data => {
                console.log(data);
                this._router.navigate(['login']);
            }
        );
    }

    GetApiAddress() {
        this._dataServis.GetApiAddressValue().subscribe(address => {
           this.ApiAddress =  address;
       });
   }
}
