import { Component, OnInit } from '@angular/core';
import { DataServis } from '../../servises/data.servis';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[
    
  ]
})

export class AppComponent implements OnInit {
  _router: any;
  private _dataServis:DataServis;

  constructor(dataServis:DataServis, router: Router){
      this._dataServis = dataServis;
      this._router = router;
  }

  ngOnInit(){
    this._dataServis.SetApiAddress("http://localhost:62733");
  }

  public ifLoginIn():boolean{
    return this._dataServis.isLiginIn();
  }

  Logount(){
    this._dataServis.SetKey('');
    this._dataServis.SetUserInfo(null);
    this._router.navigate(['login']);
  }
}
