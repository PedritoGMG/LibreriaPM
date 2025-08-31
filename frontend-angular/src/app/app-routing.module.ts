import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { PagesRoutingModule } from './pages/pages-routing.module';
import { LoginModule } from './auth/login.module';

const routes: Routes = [
    {path:"", redirectTo:"/main/libro",pathMatch:"full"},
    {path:"main", redirectTo:"/main/libro",pathMatch:"full"},
    {path:"**", component:PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes), PagesRoutingModule, LoginModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
