import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import {
  ColDef,
  GridApi,
  GridOptions,
  GridReadyEvent,
  IGetRowsParams,
  IServerSideDatasource, PaginationChangedEvent,
  RowModelType
} from 'ag-grid-community';
import {HttpClient} from "@angular/common/http";
import {AgGridCommon} from "ag-grid-community/dist/lib/interfaces/iCommon";
import {CurrencyService} from "../services/currency.service";

@Component({
  selector: 'app-rate-history',
  templateUrl: './rate-history.component.html',
  styleUrls: ['./rate-history.component.scss']
})
export class RateHistoryComponent implements OnInit {

  public gridApi!: GridApi<IRow>;
  public gridOptions: Partial<GridOptions>;
  // @ts-ignore
  public gridApi;
  // @ts-ignore
  public gridColumnApi;
  public columnDefs;
  public defaultColumnDefs;
  public cacheOverflowSize;
  public maxConcurrentDatasourceRequests;
  public infiniteInitialRowCount;

  public rowData!: IRow[];


  constructor(private httpClient: HttpClient, private currencyService: CurrencyService) {
    this.cacheOverflowSize = 2;
    this.maxConcurrentDatasourceRequests = 2;
    this.infiniteInitialRowCount = 2;

    this.gridOptions = {
      headerHeight: 45,
      rowHeight: 35,
      cacheBlockSize: 15,
      paginationPageSize: 15,
      rowModelType: 'infinite',
    }

    this.defaultColumnDefs = {
      sortable: true,
      filter: true
    }

    this.columnDefs = [
      { field: 'usd', headerName: "USD" },
      { field: 'eur', headerName: "EUR" },
      { field: 'rub', headerName: "RUB" },
      { field: 'timestamp', },
    ];
  }

  ngOnInit() {
  }

  onGridReady(params: GridReadyEvent<IRow>) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;

    var datasource = {
      getRows: (params: IGetRowsParams) => {
        this.currencyService.getExchangeRatesHistory(params.startRow, params.endRow - params.startRow, params.sortModel, params.filterModel)
          .subscribe(data => {
            // @ts-ignore
            params.successCallback(data.currencies, data.lastElement)
          });
      }
    }

    this.gridApi.setDatasource(datasource);

    this.sizeToFit();

  }

  sizeToFit() {
    this.gridApi.sizeColumnsToFit({
      defaultMinWidth: 100,
    });
  }

  onPaginationChanged($event: PaginationChangedEvent<any>) {

  }
}

interface IRow {
  usd: string,
  eur: string,
  rub: string,
  timestamp: string;
}
