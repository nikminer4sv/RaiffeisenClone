import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  constructor(
    private http: HttpClient
  ) { }

  getExchangeRates() {
    return this.http.get("http://localhost:5252/api/currency/getexchangerates?baseCurrency=BYN&currencies=USD,EUR,RUB");
  }
}
