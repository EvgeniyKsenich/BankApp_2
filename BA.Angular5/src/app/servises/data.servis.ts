import { Component, Injectable } from '@angular/core';
import { UserInfo } from '../models/models.user.info';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { UserIdenity } from '../models/models.user.identity';

@Injectable()
export class DataServis {
    private _Token:string = "";
    private Token = new BehaviorSubject<string>("");

    private _ApiAddress:string = "";
    private ApiAddress = new BehaviorSubject<string>("");

    private _UserInfo:UserInfo = null;
    private UserInfo = new BehaviorSubject<UserInfo>(new UserInfo());

    constructor() { }

    public GetTokenValue(){
        this.Token.next(this._Token);
        return this.Token.asObservable();
    }

    public GetApiAddressValue(){
        this.ApiAddress.next(this._ApiAddress);
        return this.ApiAddress.asObservable();
    }

    public GetUserInfo(){
        this.UserInfo.next(this._UserInfo);
        return this.UserInfo.asObservable();
    }

    public SetUserInfo(userInfo: UserInfo) {
        this._UserInfo = userInfo;
    }

    public SetApiAddress(address:string) {
        this._ApiAddress = address;
    }

    public SetToken(key:string) {
        this._Token = key;
    }

    public isLiginIn():boolean
    {
         if(this._Token)
             return true;

        return false;
    }
   
}