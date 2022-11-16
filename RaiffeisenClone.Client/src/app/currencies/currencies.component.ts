import { Component, OnInit } from '@angular/core';
import {CurrencyService} from "../services/currency.service";

@Component({
  selector: 'app-currencies',
  templateUrl: './currencies.component.html',
  styleUrls: ['./currencies.component.scss']
})
export class CurrenciesComponent implements OnInit {

  exchangeRates: any;

  constructor(
    private currency: CurrencyService
  ) { }

  ngOnInit(): void {
    this.getExchangeRates();
  }

  getExchangeRates() {
    this.currency.getExchangeRates().subscribe(response => this.exchangeRates = JSON.parse(JSON.stringify(response)));
  }

  formatNumber(input: number, dot: number) {
    return input.toFixed(dot);
  }

}
