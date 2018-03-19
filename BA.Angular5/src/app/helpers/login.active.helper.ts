import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { DataServis } from '../servises/data.servis';
import { Observable } from "rxjs/Observable";

@Injectable()
export class LoginActivate implements CanActivate {
  constructor(private dataServis: DataServis, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean>|Promise<boolean>|boolean {
    
    if(!this.dataServis.isLiginIn()){
        this.router.navigate(['login']);
    }
    return true;

  }
}



