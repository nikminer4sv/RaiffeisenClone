import { Component, OnInit, Input } from '@angular/core';
import { Form, FormControl, FormGroup, FormGroupName, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  submitted: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null ,[Validators.required, Validators.minLength(6)]),
    });
  }

  submit(): void {
    if (this.form.invalid)
      return;

    this.submitted = true;
  }

  getPasswordActualLength(): number {
    return this.form.get("password")?.errors!["minlength"].actualLength;
  }

  getPasswordRequiredLength(): number {
    return this.form.get("password")?.errors!["minlength"].requiredLength;
  }

  @Input() error: string | null | undefined;

}
