import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import { IdentitiServises } from '../../servises/identity.servis';
import { UserIdenity } from '../../models/models.user.identity';
import { Router } from '@angular/router';
import { UserServises } from '../../servises/user.servis';
import { UserInfo } from '../../models/models.user.info';

@Component({
    selector: 'counter',
    templateUrl: './login.component.html',
    providers:[
        IdentitiServises,
        UserServises
    ]
})
export class LoginComponent{
    private _userservis: UserServises;
    private _dataServis: DataServis;
    private _identitiServises: IdentitiServises;
    private _router: Router;

    private ApiAddress: string;
    public UserIdenity: UserIdenity = new UserIdenity();
    public ErrorMessage: string = "";

    constructor(dataServis: DataServis, identitiServises: IdentitiServises, router: Router,
                userservis:UserServises) {
        this._dataServis = dataServis;
        this._identitiServises = identitiServises;
        this._userservis = userservis;
        this._router = router;
    }

    private ReddirectToHome() {
        this._router.navigate(['home']);
    }

    async Login() {
        this._identitiServises.Login(this.UserIdenity)
            .subscribe(async (data: any) => {
                if (data.access_token != null) {
                    this._dataServis.SetToken(data.access_token);
                    this.ErrorMessage = "";
                    this.ReddirectToHome();
                }
                else {

                }
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
