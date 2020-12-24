import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { AccountService } from '../account/account.service';

@Injectable({
    providedIn: 'root'
})
export class IsUserAuthorized implements CanActivate {
    constructor(private router: Router, private accountService: AccountService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
        boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.accountService.currentUser$
            .pipe(map(user => {
                if (user === null) {
                    this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
                    return false;
                } else {
                    return true;
                }
            }));
    }

}
