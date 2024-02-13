import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Product } from 'src/app/core/models/product.model';
import { ProductService } from 'src/app/core/services/product-data-service';
import { Messages } from 'src/app/core/utils/messages';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-productos',
  templateUrl: './products-index.component.html',
})
export class ProductsIndexComponent implements OnInit {
  public page = 1;
  public pageSize: number = 5;
  public collectionSize: number = 5;
  public maxSize!: number;
  productos!: Product[];
  formSearchFilters!: FormGroup;
  
  constructor(  
    private router: Router,
    private productoService: ProductService, 
    private fb: FormBuilder
    ) { 
      this.formSearchFilters = this.fb.group({
        nombre: [''],
        descripcion: [''],
        categoria: ['']
      });
    }

  ngOnInit(): void {
    this.obtenerProductos();
  }

  cambiarTamanoPagina(pageSize: any) {
    this.pageSize = pageSize;
  }

  buscar(): void {
    const nombre = this.formSearchFilters.get('nombre')?.value;
    const descripcion = this.formSearchFilters.get('descripcion')?.value;
    const categoria = this.formSearchFilters.get('categoria')?.value;

    this.productoService.getProducts(categoria,'categoria',true,this.page,this.pageSize)
    .subscribe({
      next:(data: Product[]) => {
        this.productos = data;
        this.collectionSize =  this.productos.length;
      },
      error:(error) => {
        Messages.error(error);
      }
    });
  }

  obtenerProductos(): void{
    this.productoService.getProducts('categoria','categoria',true,this.page,this.pageSize)
    .subscribe({
      next:(data: Product[]) => {
        this.productos = data;
        this.collectionSize =  this.productos.length;
      },
      error:(error) => {
        Messages.error(error);
      }
    });
  }

  editarProducto(producto: any): void {
    this.router.navigate(['/crear-producto', { id: producto.id }]);
  }

  eliminarProducto(producto:any): boolean {
    let resultQuestion: boolean = false;
    Swal.fire({
      title: 'Eliminar producto',
      customClass: {
        confirmButton: 'swalBtnColor',
        icon: 'swalIconColor'
      },
      text: 'Esta seguro de eliminar un producto',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'SI',
      cancelButtonText: 'NO',
    }).then((result) => {
      if (result.value) {
        this.eliminarProductoId(producto.id);
      }
    }, (error) => {
      
    });
    return resultQuestion;
  }

  eliminarProductoId(id: number): boolean {
    this.productoService.deleteProduct(id)
    .subscribe({
      next:(producto) => {
        Messages.succes("El producto se elimino correctamente");
      },
      error:(error) => {
        Messages.error(error);
      }
  }); 
    return true;
  }

  crearProducto(): void {
    this.router.navigate(['/crear-producto']);
  }

}
