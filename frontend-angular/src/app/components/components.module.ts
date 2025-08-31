import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BarraCierreComponent } from './barra-cierre/barra-cierre.component';
import { ToolbarModule } from 'primeng/toolbar';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';



@NgModule({
  declarations: [
    BarraCierreComponent
  ],
  imports: [
    CommonModule,
    ToolbarModule,
    InputTextModule,
    ButtonModule
  ],
  exports:[
    BarraCierreComponent
  ]
})
export class ComponentsModule { }
