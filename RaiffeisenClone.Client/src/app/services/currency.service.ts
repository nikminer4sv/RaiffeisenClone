import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SortModelItem} from "ag-grid-community/dist/lib/sortController";
import {Sort} from "@angular/material/sort";

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  headerDict = {
    'Content-Type':"application/json"
  };
  requestOptions = {
    headers: new HttpHeaders(this.headerDict)
  };

  constructor(
    private http: HttpClient
  ) { }

  getExchangeRates() {
    return this.http.get("http://localhost:5252/api/currency/getlast");
  }

  getExchangeRatesHistory(start: number, count: number, sort: SortModelItem[], filter: any) {
    let basePart = `http://localhost:5252/api/currency/get?start=${start}&count=${count}`;
    let responseViewModel = this.getResponseViewModel(sort, filter);
    return this.http.post(`http://localhost:5252/api/currency/get?start=${start}&count=${count}`, JSON.stringify(responseViewModel), this.requestOptions);
  }

  private getFilterViewModel(filter: any): any {
    if (JSON.stringify(filter) == "{}")
      return null;

    let filterViewModel: any[] = [];
    let keys = Object.keys(filter);
    for (let i = 0; i < keys.length; i++) {
      let filterPart = {
        ColId: keys[i],
        FilterType: filter[keys[i]].filterType,
        Type: filter[keys[i]].type != undefined ? filter[keys[i]].type : null,
        Filter: filter[keys[i]].filter != undefined ? filter[keys[i]].filter : null,
        Operator: filter[keys[i]].operator != undefined ? filter[keys[i]].operator : null,
        Condition1: filter[keys[i]].condition1 != undefined ? filter[keys[i]].condition1 : null,
        Condition2: filter[keys[i]].condition2 != undefined ? filter[keys[i]].condition2 : null,
      }
      filterViewModel.push(filterPart);
    }
    return filterViewModel
  }

  private getSortModel(sort: any): any {
    if (sort.length == 0)
      return null;

    let sortViewModel = {
      sort: sort[0].sort,
      colId: sort[0].colId
    }
    return sortViewModel
  }

  private getResponseViewModel(sort: any, filter: any) {
    return {
      SortViewModel: this.getSortModel(sort),
      FilterViewModel: this.getFilterViewModel(filter)
    }
  }
}
