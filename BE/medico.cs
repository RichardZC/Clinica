
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
    
public partial class medico
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public medico()
    {

        this.programacion = new HashSet<programacion>();

    }


    public int MedicoId { get; set; }

    public int PersonaId { get; set; }

    public int EspecialidadId { get; set; }

    public string NumeroColegio { get; set; }

    public Nullable<System.DateTime> FechaColegiacion { get; set; }

    public string TituloProfesional { get; set; }

    public string Universidad { get; set; }

    public bool Estado { get; set; }



    public virtual especialidad especialidad { get; set; }

    public virtual persona persona { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<programacion> programacion { get; set; }

}

}
