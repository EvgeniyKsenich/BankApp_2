import { Component, OnInit } from '@angular/core';
import { UserRegister } from '../../models/models.user.regiser';
import { Router } from '@angular/router';
import { UserServises } from '../../servises/user.servis';
import { DataServis } from '../../servises/data.servis';
import { IdentitiServises } from '../../servises/identity.servis';


@Component({
    selector: 'counter',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    providers:[IdentitiServises]
})
export class RegisterComponent implements OnInit{
    private UserRegister: UserRegister = new UserRegister();
    private _router: Router;
    private _identitiServises: IdentitiServises;
    private _dataServis:DataServis;
    public ErrorMessage:string = "";

    constructor(identitiServises: IdentitiServises,
                router: Router,
                dataServis:DataServis){
        this._identitiServises = identitiServises;
        this._router = router;
        this._dataServis = dataServis;
    }

    ngOnInit(){
    }

    Register(){
        this._identitiServises.Register(this.UserRegister).subscribe(
            data => {
                this.ErrorMessage = "";
                this._router.navigate(['login']);
            },
            error => {
                this.ErrorMessage = error.error;
            }
        );
    }

   public isError(){
    if(this.ErrorMessage != "")
        return true;

    return false;
    }
}
