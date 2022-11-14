import { Component, OnInit, Input } from '@angular/core';
import { Form, FormControl, FormGroup, FormGroupName, Validators } from '@angular/forms';
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  error: string = "";

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]),
      password: new FormControl(null ,[Validators.required, Validators.minLength(8)]),
    });
  }

  submit(): void {
    if (this.form.invalid)
      return;

    let request = {
      username: this.form.value.username,
      password: this.form.value.password
    }

    this.auth.login(request).subscribe(
      response => {
        this.form.reset();
        this.router.navigate(["/"]);
      },
      error => {
        console.log("error: " + JSON.stringify(error));
        this.error = error.error.Message;
      }
    );
  }

}
