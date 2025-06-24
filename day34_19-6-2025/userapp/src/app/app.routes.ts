import { Routes } from '@angular/router';
import { Comp1 } from './comp1/comp1';
import { Comp2 } from './comp2/comp2';
import { Login } from './login/login';
import { Maincomp } from './maincomp/maincomp';

export const routes: Routes = [
    // {path:"comp1",component:Comp1},
    // {path:"comp2",component:Comp2}
    {path:"login",component:Login},
    {path:"maincomp",component:Maincomp}

];
