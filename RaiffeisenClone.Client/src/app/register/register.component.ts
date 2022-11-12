import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

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
