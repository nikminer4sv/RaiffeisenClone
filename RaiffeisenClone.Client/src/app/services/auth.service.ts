import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(user: any) {
    return this.http.post('https://localhost:7173/auth/login', user)
      .pipe(
        tap(this.setToken)
      )
  }

  register(user: any) {
    return this.http.post("https://localhost:7173/auth/login", user);
  }

  private setToken(response: any) {
    if (response) {
      localStorage.setItem("jwt-token", response.accessToken)
      //localStorage.setItem("refresh-token", response.refreshToken)
    } else {
      localStorage.clear();
    }
  }

  get token() {
    //const tokenExp = new Date(localStorage.getItem("fb-token-exp")!);
    /*if (new Date() > tokenExp) {
      this.logout();
      return null;
    }*/
    return localStorage.getItem("jwt-token");
  }

  logout() {
    this.setToken(null);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

}
