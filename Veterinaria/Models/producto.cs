//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Veterinaria.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class producto
    {
        public producto()
        {
            this.venta = new HashSet<venta>();
        }
    
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
    
        public virtual ICollection<venta> venta { get; set; }
    }
}
