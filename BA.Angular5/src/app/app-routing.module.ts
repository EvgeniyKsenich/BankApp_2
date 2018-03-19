import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {DepositComponent} from './components/deposit/deposit.component';
import {WithdrawComponent} from './components/withdraw/withdraw.component';
import {TransferComponent} from './components/transfer/transfer.component';
import {HomeComponent} from './components/home/home.component';
import {RegisterComponent} from './components/register/register.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginActivate } from './helpers/login.active.helper';
import { DataServis } from './servises/data.servis';
import { LoginUnactivate } from './helpers/login.unactive.helper';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent, canActivate:[LoginUnactivate] },
  { path: 'register', component: RegisterComponent, canActivate:[LoginUnactivate] },
  { path: 'home', component: HomeComponent, canActivate:[LoginActivate] },
  { path: 'deposit', component: DepositComponent, canActivate:[LoginActivate] },
  { path: 'withdraw', component: WithdrawComponent, canActivate:[LoginActivate] },
  { path: 'transfer', component: TransferComponent, canActivate:[LoginActivate] },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  declarations: [
    LoginComponent,
    DepositComponent,
    HomeComponent,
    WithdrawComponent,
    TransferComponent,
    RegisterComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  providers:[
    DataServis,
    LoginActivate,
    LoginUnactivate
  ]
})
export class AppRoutingModule { }
