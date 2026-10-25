import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { AppAdmin } from './features/admin/app-admin';
import { AppCustomer } from './features/customer/app-customer';
import { DashboardAdmin } from './features/admin/dashboard.admin/dashboard.admin';
import { DashboardCustomer } from './features/customer/dashboard.customer/dashboard.customer';
import { ForgotPassword } from './features/auth/forgot-password/forgot-password';
import { adminGuard } from './core/guards/admin.guard';
import { customerGuard } from './core/guards/customer.guard';
import { Register } from './features/auth/register/register';
import { Confirmation } from './features/auth/confirmation/confirmation';
import { ChangePassword } from './features/auth/change-password/change-password';
import { MealOfTheDayAdmin } from './features/admin/meal-of-the-day.admin/meal-of-the-day.admin';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
    {
        path: '',
        component: Login,
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: Login,
        pathMatch: 'full'
    },
    {
        path: 'register',
        component: Register,
        pathMatch: 'full'
    },
    {
        path: "confirm",
        component: Confirmation,
        pathMatch: 'full'
    },
    {
        path: "change-password",
        component: ChangePassword,
        pathMatch: 'full'
    },
    {
        path: "admin",
        component: AppAdmin,
        canActivate: [roleGuard],
        data: { roles: ['Admin'] },
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardAdmin }
        ]
    },
    {
        path: "admin/meal-of-the-day",
        component: MealOfTheDayAdmin,
        canActivate: [roleGuard],
        data: { roles: ['Admin'] }
    },
    {
        path: 'customer',
        component: AppCustomer,
        canActivate: [roleGuard],
        data: { roles: ['Customer'] },
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardCustomer }
        ]
    },
    {
        path: 'forgot-password',
        component: ForgotPassword,
        pathMatch: 'full'
    }
];
