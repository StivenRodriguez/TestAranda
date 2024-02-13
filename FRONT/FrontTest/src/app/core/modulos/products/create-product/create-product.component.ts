import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/core/models/product.model';
import { ProductService } from 'src/app/core/services/product-data-service';
import { Messages } from 'src/app/core/utils/messages';

@Component({
  selector: 'app-crear-producto',
  templateUrl: './create-product.component.html'
})
export class CreateProductComponent implements OnInit {
  productoForm!: FormGroup;
  paramSubscribe: any;
  productoId: number;
  
  constructor(
    private fb: FormBuilder,
    private productoService: ProductService, 
    private router: Router,
    private routerActive: ActivatedRoute,
  ) {
    this.productoId = 0;
   }

  ngOnInit(): void {
    this.productoForm = this.fb.group({
      nombre: ['', Validators.required],
      descripcion: ['', Validators.required],
      categoria: ['', [Validators.required]],
      imagenURL: ['', [Validators.required]]
    });
    this.validatParams()
  }

  validatParams() {
    this.paramSubscribe = this.routerActive.params.subscribe(params => {
      this.productoId = params['id'];
    });
    if(this.productoId != 0){
      this.obtenerProductoId(this.productoId);
    }
  }

  obtenerProductoId(id: number) {
    this.productoService.getProductById(id)
        .subscribe({
          next:(producto) => {
            this.productoForm.setValue({
              nombre: producto.nombre,
              descripcion:  producto.descripcion,
              categoria: producto.categoria,
              imagen: producto.imagen
            });
          },
          error:(error) => {
            Messages.error(error);
          }
      }); 
  }

  guardarProducto(){
    if (this.productoForm.valid) {
      const producto: Product = {
        nombre: this.productoForm.value.nombre,
        descripcion: this.productoForm.value.descripcion,
        categoria: this.productoForm.value.categoria,
        imagen: this.productoForm.value.categoria,
        id: this.productoId
      };

      if(this.productoId == 0){
          this.productoService.addProduct(producto)
          .subscribe({
            next:(producto) => {
              Messages.succes("Producto creado con exito");
              this.regresar();
            },
            error:(error) => {
              Messages.error(error);
            }
        });
      } else {
          this.productoService.updateProduct(this.productoId,producto)
          .subscribe({
            next:(producto) => {
              Messages.succes("Producto actualizado con exito");
              this.regresar()
            },
            error:(error) => {
              Messages.error(error);
            }
         });
      }
    }
  }

  regresar() {
    this.router.navigate(['']);
  }

}
