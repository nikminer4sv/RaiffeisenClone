import { Component, OnInit } from '@angular/core';
import {DepositService} from "../services/deposit.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-deposits',
  templateUrl: './deposits.component.html',
  styleUrls: ['./deposits.component.scss']
})

export class DepositsComponent implements OnInit {

  depositsList?: deposit[];

  constructor(
    public deposits: DepositService,
    public router: Router,
  ) { }

  ngOnInit(): void {
    this.deposits.getAllDeposits()?.subscribe(
      response => {
        let jsonResponse = JSON.stringify(response);
        let objects: deposit[] = JSON.parse(jsonResponse);
        this.depositsList = objects;

        for (let i = 0; i < this.depositsList.length; i++)
          this.depositsList[i].term = new Date(this.depositsList[i].term);
      }
    )
  }

  getBeatifulDate(date: Date): string {
    return date.toDateString();
  }

  reloadCurrentPage() {
    window.location.reload();
  }

  deleteByIdWrapper(id: string) {
    this.deposits.deleteById(id).subscribe(
      response => {
        this.reloadCurrentPage();
      }
    );
  }

}
interface deposit {
  id: string,
  term: Date,
  bid: number,
  currency: string,
  isReplenished: boolean,
  isWithdrawed: boolean
}
