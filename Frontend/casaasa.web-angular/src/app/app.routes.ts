import { Routes } from '@angular/router';
import { Login } from './features/login/login';
import { AppAdmin } from './features/admin/app-admin';
import { AppCustomer } from './features/customer/app-customer';
import { DashboardAdmin } from './features/admin/dashboard.admin/dashboard.admin';
import { DashboardCustomer } from './features/customer/dashboard.customer/dashboard.customer';
import { Register } from './features/register/register';
import { ForgotPassword } from './features/forgot-password/forgot-password';
import { adminGuard } from './core/guards/admin.guard';
import { customerGuard } from './core/guards/customer.guard';

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
        path: "admin",
        component: AppAdmin,
        canMatch: [adminGuard],
        children: [
            { path: '', component: DashboardAdmin }
        ]
    },
    {
        path: 'customer',
        component: AppCustomer,
        canMatch: [customerGuard],
        children: [
            { path: '', component: DashboardCustomer }
        ]
    },
    {
        path: 'register',
        component: Register,
        pathMatch: 'full'
    },
    {
        path: 'forgot-password',
        component: ForgotPassword,
        pathMatch: 'full'
    }
];
