# TemplateWebAuth

This project was generated using [An gular CLI](https://github.com/angular/angular-cli) version 20.3.13.

## Simple frontend app that uses JWT for login/register

---
### Settings

* base url: [app.const.ts](src/app/app.const.ts)
* anonymous requests (that dont use jwt token): [app.const.ts](src/app/app.const.ts)

### Navigation

* Login page: localhost:4200/auth/login
* Register page: localhost:4200/auth/register
* User info page: localhost:4200/user (only for auth users)

### Infrastructure / Architecture 

* Interceptor that checks jwt token and refresh it if needed: [refresh-token-interceptor.ts](src/app/core/api/refresh-token-interceptor.ts)
* auth guard: [auth.guard.ts](src/app/auth/auth.guard.ts)
* base http controller: [request-handler.ts](src/app/core/api/request-handler.ts)
* ngxs store (dashboard store is just example that we dont need to use `provide in root` for services and ets)
* [auth.api.service.ts](src/app/auth/auth.api.service.ts) uses `@Injectable({ providedIn: 'root' })` because it needed on interceptor  
* [dashboard.resolver.ts](src/app/dashboard/dashboard.resolver.ts) that save data to store before page opens
---

### TODO
* set scss as default
* add stylelint for html and css
* add angular proxy to remove CORS policy (for dev)
* tests
