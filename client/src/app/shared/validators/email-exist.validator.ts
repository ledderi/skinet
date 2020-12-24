import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { AccountService } from 'src/app/account/account.service';

@Injectable({
    providedIn: 'root'
})
export class EmailExistValidator implements AsyncValidator {
    constructor(private accountService: AccountService) {}

    validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
        const email = control.value;
        return this.accountService.checkEmailExist(email)
            .pipe(
                map(isEmailExist => isEmailExist ? { emailExist: true} : null ),
                catchError(() => of({ emailExist: true }))
            );
    }
    registerOnValidatorChange?(fn: () => void): void {
        throw new Error("Method not implemented.");
    }

}