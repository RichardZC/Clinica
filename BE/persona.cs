
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BE
{

using System;
    using System.Collections.Generic;
    
public partial class persona
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public persona()
    {

        this.cajadiario = new HashSet<cajadiario>();

        this.cajamov = new HashSet<cajamov>();

        this.medico = new HashSet<medico>();

        this.usuario = new HashSet<usuario>();

        this.atencion = new HashSet<atencion>();

        this.atencion1 = new HashSet<atencion>();

        this.paciente = new HashSet<paciente>();

    }


    public int PersonaId { get; set; }

    public string Nombres { get; set; }

    public string Paterno { get; set; }

    public string Materno { get; set; }

    public string NombreCompleto { get; set; }

    public string DNI { get; set; }

    public string Celular { get; set; }

    public string Correo { get; set; }

    public string Sexo { get; set; }

    public Nullable<System.DateTime> FechaNacimiento { get; set; }

    public string Direccion { get; set; }

    public string Referencia { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<cajadiario> cajadiario { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<cajamov> cajamov { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<medico> medico { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<usuario> usuario { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<atencion> atencion { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<atencion> atencion1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<paciente> paciente { get; set; }

}

}
