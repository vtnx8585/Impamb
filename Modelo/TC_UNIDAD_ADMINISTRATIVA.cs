//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impamb.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class TC_UNIDAD_ADMINISTRATIVA
    {
        public int CODIGO_UNIDAD_ADMINISTRATIVA { get; set; }
        public string DESCRIPCION { get; set; }
        public string USUARIO_INGRESO { get; set; }
        public System.DateTime FECHA_INGRESO { get; set; }
        public string USUARIO_ACTUALIZACION { get; set; }
        public System.DateTime FECHA_ACTUALIZACION { get; set; }
        public string PROGRAMA { get; set; }
        public string ACTIVIDAD { get; set; }
        public Nullable<short> PERIODO { get; set; }
        public string TIPO { get; set; }
        public Nullable<int> CODIGO_DEPARTAMENTO { get; set; }
        public Nullable<int> CODIGO_MUNICIPIO { get; set; }
        public string PERTENECE { get; set; }
        public string HPERTENECE { get; set; }
        public string ESTATUS { get; set; }
        public Nullable<int> REGION { get; set; }
    }
}
