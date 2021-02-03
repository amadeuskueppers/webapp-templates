import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeOverviewComponent } from './employee-overview/employee-overview.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TableExampleComponent } from './table-example/table-example.component';

// mehr zum router unter: https://www.positronx.io/angular-router-tutorial/
const routes: Routes = [
  { path: '', redirectTo: '/overview', pathMatch: 'full' },
  { path: 'overview', component: EmployeeOverviewComponent },
  { path: 'examples/table', component: TableExampleComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
