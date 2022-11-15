import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {DepositService} from "../services/deposit.service";

@Component({
  selector: 'app-new-deposit',
  templateUrl: './new-deposit.component.html',
  styleUrls: ['./new-deposit.component.scss']
})
export class NewDepositComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  sliderValue = 0;

  constructor(
    private deposits: DepositService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      currency: new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(3)]),
      bid: new FormControl(null, [Validators.required]),
      term: new FormControl(null, [Validators.required]),
      isReplenished: new FormControl(false, [Validators.required]),
      isWithdrawed: new FormControl(false, [Validators.required]),
    });
  }

  submit() {
    if (this.form.invalid)
      return;

    let request = {
      currency: this.form.value.currency,
      bid: this.form.value.bid,
      term: this.form.value.term,
      isReplenished: this.form.value.isReplenished,
      isWithdrawed: this.form.value.isWithdrawed,
    }

    console.log(request)

    this.deposits.add(request).subscribe(
      response => {
        this.router.navigate(["/deposits"]);
      }
    );
  }

}
