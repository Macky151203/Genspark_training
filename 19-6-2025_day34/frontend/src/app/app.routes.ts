import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Login } from './login/login';
import { About } from './about/about';
import { History } from './history/history';
import { AuthGuard } from './auth-guard';

export const routes: Routes = [
    {path:"",component:Home},
    {path:"login",component:Login},
    {path:"about",component:About},
    {path:"history",component:History,canActivate:[AuthGuard]}
];
