import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Application component imports.
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';

// Routes to different components go here.
const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: HomeComponent },
  { path: 'dashboard', component: DashboardComponent }
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
