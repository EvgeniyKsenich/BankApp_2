import { Component, Injectable } from '@angular/core';
import { UserInfo } from '../models/models.user.info';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { UserIdenity } from '../models/models.user.identity';

@Injectable()
export class DataServis {
    private _Key:string = "";
    private Key = new BehaviorSubject<string>("");

    private _ApiAddress:string = "";
    private ApiAddress = new BehaviorSubject<string>("");

    private _UserInfo:UserInfo = null;
    private UserInfo = new BehaviorSubject<UserInfo>(new UserInfo());

    constructor() { }

    public GetKeyValue(){
        this.Key.next(this._Key);
        return this.Key.asObservable();
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

    public SetKey(key:string) {
        this._Key = key;
    }

    public isLiginIn():boolean
    {
         if(this._Key)
             return true;

        return false;
    }
   
}