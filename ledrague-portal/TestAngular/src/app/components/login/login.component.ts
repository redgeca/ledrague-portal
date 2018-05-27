import { Component } from '@angular/core';
import { UserServices } from '../../services/user.services';
import { FormBuilder, Validators } from '@angular/forms'
import { ActivatedRoute } from '@angular/router'
import { AuthService } from '../../services/auth.service';

@Component({
    moduleId: module.id,
    selector: 'login',
    templateUrl: 'login.component.html', 
    styles: [`
    .error {
      background: #f08080;
  `]

})


export class LoginComponent {
    
    credentialsForm;

    constructor(private authService: AuthService, private formBuilder: FormBuilder, private route: ActivatedRoute) {
        this.credentialsForm = formBuilder.group( {
            username: ['', Validators.required],
            password: ['', Validators.required],
        })
    }

    onSubmit() {
        console.log(this.credentialsForm.value);
    }

    isValid(control) {
        return this.credentialsForm.controls[control].invalid && this.credentialsForm.controls[control].touched
      }
    
}


