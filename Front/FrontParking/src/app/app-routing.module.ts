import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterEntryComponent } from './register-entry/register-entry.component';


const routes: Routes = [
  { path: 'register-Entry', component: RegisterEntryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
