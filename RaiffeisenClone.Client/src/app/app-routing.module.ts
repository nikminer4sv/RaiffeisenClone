import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainPageComponent} from "./main-page/main-page.component";
import {MainLayoutComponent} from "./shared/main-layout/main-layout.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {LoginComponent} from "./login/login.component";

const routes: Routes = [
  {path: "", component: MainLayoutComponent, children: [
      {path: "", redirectTo: "/", pathMatch: "full"},
      {path: "", component: MainPageComponent},
      {path: "login", component: LoginComponent},
      {path: "register", component: LoginComponent},
      {path: "**", component: NotFoundComponent}
    ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
