//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductCatalog.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int productId { get; set; }
        public string nameProduct { get; set; }
        public string shortDescriptionProduct { get; set; }
        public string categoryProduct { get; set; }
        public byte[] productImage { get; set; }
    }
}
