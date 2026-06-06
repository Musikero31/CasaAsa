import { ApplicationConfig, inject, provideAppInitializer, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { credentialsInterceptor } from './core/interceptors/credentials.interceptors';
import { AuthApiService } from './shared/services/features/auth-api.service';
import { firstValueFrom } from 'rxjs';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([credentialsInterceptor])),
    provideAppInitializer(() => {
      const authAPI = inject(AuthApiService);
      let result =  firstValueFrom(authAPI.loadCurrentUser());
      return result;
    })
  ]
};
