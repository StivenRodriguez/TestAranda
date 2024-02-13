import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class Messages {
  static _tituloGeneral: string = 'TEST';

  constructor (
  ) { }

  static info(mensaje: string) {
    Swal.fire({
      title: this._tituloGeneral,
      text: mensaje,
      icon: 'info',
      customClass: {
        confirmButton: 'swalBtnColor',
        icon: 'swalIconColor'
      },
    });
  }
  static succes(mensaje: string) {
    Swal.fire({
      title: this._tituloGeneral,
      text: mensaje,
      icon: 'success',
      customClass: {
        confirmButton: 'swalBtnColor',
        icon: 'swalIconColor'
      },
    });
  }
  static warning(mensaje: string) {
    Swal.fire({
      title: this._tituloGeneral,
      text: mensaje,
      icon: 'warning',
      customClass: {
        confirmButton: 'swalBtnColor',
        icon: 'swalIconColor'
      },
    });
  }
  static error(mensaje: string) {
    Swal.fire({
      title: this._tituloGeneral,
      text: mensaje,
      icon: 'error',
      customClass: {
        confirmButton: 'swalBtnColor',
        icon: 'swalIconColor'
      },
    });
  }
}
