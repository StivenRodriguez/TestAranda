import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";
import { Product } from "../models/product.model";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = environment.AMBIENTE; 

  constructor(private readonly http: HttpClient) { }

  public getProducts(search: string, sort: string, order: boolean, page: number, pageSize: number): Observable<Product[]> {
    const params = {
      search: search,
      sort: sort,
      order: order.toString(),
      page: page.toString(),
      pageSize: pageSize.toString()
    };

    return this.http.get<Product[]>(`${this.apiUrl}/api/products`, { params: params });
  }

  public getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/api/products/GetProductId/${id}`);
  }

  public deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/products/${id}`);
  }

  public updateProduct(id: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/api/products/${id}`, product);
  }

  public addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/api/products`, product);
  }
}
