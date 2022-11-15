import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from "./main-page/main-page.component";
import {MainLayoutComponent} from "./shared/main-layout/main-layout.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {DepositsComponent} from "./deposits/deposits.component";
import {NewDepositComponent} from "./new-deposit/new-deposit.component";
import {InfoComponent} from "./info/info.component";
import {CurrenciesComponent} from "./currencies/currencies.component";

const routes: Routes = [
  {path: "", component: MainLayoutComponent, children: [
      {path: "", redirectTo: "/", pathMatch: "full"},
      {path: "", component: MainPageComponent},
      {path: "login", component: LoginComponent},
      {path: "register", component: RegisterComponent},
      {path: "deposits", component: DepositsComponent},
      {path: "deposits/add", component: NewDepositComponent},
      {path: "info", component: InfoComponent},
      {path: "currencies", component: CurrenciesComponent},
      {path: "**", component: NotFoundComponent}
    ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
