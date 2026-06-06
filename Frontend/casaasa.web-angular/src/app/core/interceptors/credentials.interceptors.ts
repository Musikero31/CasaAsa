import { HttpInterceptorFn } from "@angular/common/http";

export const credentialsInterceptor: HttpInterceptorFn = (req, next) => {
  const request = req.clone({
    withCredentials: true
  });
  return next(request);
};