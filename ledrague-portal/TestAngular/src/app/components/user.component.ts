import { Component } from '@angular/core';
import { UserServices } from '../services/user.services'
import { FormBuilder, Validators } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'

@Component({
  moduleId: module.id,
  selector: 'user',
  templateUrl: 'user.component.html',
  styles: [`
    .error {
      background: #f08080;
  `]
})

export class UserComponent {

  applications = [
    { value: 0, viewValue: 'Contrôle des Identités et des Accès' },
    { value: 1, viewValue: 'Gestion des Contrats' },
    { value: 2, viewValue: 'Karaoké' }
  ];

  selection: number;
  userForm;
  isEditing = false

  constructor(private userServices: UserServices, formBuilder: FormBuilder, private route: ActivatedRoute) {
    this.userForm = formBuilder.group({
      username: ['', Validators.required],
      email: ['', emailValid()],
      password: [''],
      confirmPassword: [''],
      roles: [{
        name: '',
        displayValue: '',
        checked: false
      }]
    }, {
        validator: validPasswords(this.isEditing, 'password', 'confirmPassword')
      })
  }

  ngOnInit() {
    var id = this.route.snapshot.params.id
    console.log(id)

    // Case where we are adding a user
    if (typeof id === 'undefined' || id === null) {
      this.isEditing = false;
    } else {
      this.isEditing = true;
    }
  }

  onSubmit() {
    console.log(this.userForm.errors)
    console.log(this.userForm.valid)
  }

  isValid(control) {
    return this.userForm.controls[control].invalid && this.userForm.controls[control].touched
  }
}

function validPasswords(isEditing, password1, password2) {
  return form => {
    console.log("Password : " + password1 + " Edition : " + isEditing)
    if (!isEditing && form.controls[password1].value !== form.controls[password2].value &&
      form.controls[password1].value !== null) {
      return { mismatchedFields: true };
    }
  }
}

function emailValid() {
  return control => {
    var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

    return regex.test(control.value) ? null : { invalidEnail: true }
  }
}
