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
export class LoginComponent implements OnInit{
    private _userservis: UserServises;
    private _dataServis: DataServis;
    private _identitiServises: IdentitiServises;
    private _router: Router;

    private ApiAddress: string;
    public UserIdenity: UserIdenity = new UserIdenity();

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

    ngOnInit() {
        this.GetApiAddress();
    }

    async GetKey():Promise<string> {
        var keyValue = "";
        await this._dataServis.GetKeyValue().subscribe(key => {
            keyValue = key;
        });
        return keyValue;
    }

    async GetApiAddress():Promise<string> {
        var addressValue = "";
        await this._dataServis.GetApiAddressValue().subscribe(address => {
            addressValue =  address;
        });
        return addressValue;
    }

    async Login() {
        var address = await this.GetApiAddress();
        this._identitiServises.Login(address, this.UserIdenity)
            .subscribe(async (data: any) => {
                if (data.access_token != null) {
                    this._dataServis.SetKey(data.access_token);
                    var user = await this.GetUserData();
                    this.ReddirectToHome();
                }
                else {

                }
            }
        );
    }

    public async GetUserData():Promise<UserInfo>{
        var address = await this.GetApiAddress();
        var key = await this.GetKey();
        var userInfo:UserInfo;
        await this._userservis.getCurrentUser(address,key).subscribe( (user:any) => {
            userInfo = user;
            this._dataServis.SetUserInfo(user);
        });
        return userInfo;
    }
}
