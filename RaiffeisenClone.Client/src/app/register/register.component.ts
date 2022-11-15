import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  form: FormGroup = new FormGroup({});

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]),
      lastName: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]),
      username: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]),
      date: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null ,[Validators.required, Validators.minLength(6)]),
    });
  }

  submit(): void {
    if (this.form.invalid)
      return;

    let request = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      username: this.form.value.username,
      dateOfBirth: this.form.value.date,
      email: this.form.value.email,
      password: this.form.value.password,
    }

    this.auth.register(request).subscribe(
      response => {
        this.auth.login(response).subscribe(
          loginResponse => {
            this.router.navigate(["/"]);
          }
        )
      }
    );
  }

}
