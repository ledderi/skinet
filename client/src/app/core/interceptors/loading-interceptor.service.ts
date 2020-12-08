import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';

import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
    providedIn: 'root'
})
export class LoadingInterceptorService implements HttpInterceptor {
    constructor(private spinnerService: NgxSpinnerService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinnerService.show(undefined, { type: 'timer' });

        return next.handle(req).pipe(
            delay(1000),
            finalize(() => {
                this.spinnerService.hide();
            })
        );
    }

}
