import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsIndexComponent } from './core/modulos/products/products-index/productos.component';
import { CreateProductComponent } from './core/modulos/products/create-product/create-product.component';

const routes: Routes = [
  {
    path: '',
    component: ProductsIndexComponent, // Cambia esto con el componente principal de ProductoModule
  },
  {
    path: 'crear-producto',
    component: CreateProductComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
