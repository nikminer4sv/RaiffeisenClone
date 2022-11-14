import { Injectable } from '@angular/core';
import {AuthService} from "./auth.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DepositService {

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  getAllDeposits() {
    if (!this.auth.isAuthenticated())
      return;

    const headerDict = {
      'Authorization':"Bearer " + this.auth.token
    };
    const requestOptions = {
      headers: new HttpHeaders(headerDict)
    };
    return this.http.get("http://localhost:8000/api/deposit", requestOptions);
  }

  deleteById(id: string) {
    const headerDict = {
      'Authorization':"Bearer " + this.auth.token
    };
    const requestOptions = {
      headers: new HttpHeaders(headerDict)
    };
    console.log(id);
    return this.http.delete("http://localhost:8000/api/deposit/" + id, requestOptions);
  }

  add(deposit: any) {
    const headerDict = {
      'Authorization':"Bearer " + this.auth.token
    };
    const requestOptions = {
      headers: new HttpHeaders(headerDict)
    };
    return this.http.post("http://localhost:8000/api/deposit", deposit, requestOptions);
  }
}
