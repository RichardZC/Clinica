Vue.component('crearPersona', {
    mounted: function () {
        $('#formValidate').validate();
    },
    data: function () {
        return {
            PersonaId: 0
        }
    },
    template: `
                <div class ="modal modalPersona">
                      <div class ="modal-content">

               
                                <div class ="row" style="margin:0px">
                                    <form id="formValidate" class ="validar" v-on:submit.prevent="CrearPersona()" >
                                        <input type="hidden" name="PersonaId" value="0" />
                                        <div class ="row">
                                            <div class ="input-field col s10">
                                                <i class ="mdi-action-search prefix"></i>
                                                <label for="DNI">DNI*</label>
                                                <input id="DNI" name="DNI" type="text" required minlength="8"  autofocus>
                                            </div>
                                            <div class ="input-field col s12  m4 l4">
                                                <i class ="mdi-action-account-circle prefix"></i>
                                                <label for="Nombres">Nombres*</label>
                                                <input id="Nombres" name="Nombres" type="text" required >
                                            </div>
                                            <div class ="input-field col s6 m4 l4">
                                                <label for="Paterno">Ape.Paterno*</label>
                                                <input id="Paterno" name="Paterno" type="text" required >

                                            </div>
                                            <div class ="input-field col s6 m4 l4">
                                                <label for="Materno">Ape.Materno*</label>
                                                <input id="Materno" name="Materno" type="text" required >
                                            </div>
                                            <div class ="input-field col s12 m6 ">
                                                <i class ="mdi-action-settings-cell prefix"></i>
                                                <label for="Celular">Celular</label>
                                                <input id="Celular" name="Celular" type="text" minlength="9" >

                                            </div>
                                            <div class ="input-field col s12 m6">
                                                <i class ="mdi-communication-email prefix"></i>
                                                <label for="Correo">Correo</label>
                                                <input id="Correo" name="Correo" type="email" >

                                            </div>
                                            <div class ="input-field col s6  ">
                                                <i class ="mdi-notification-event-note prefix"></i>
                                                <label for="nacimiento" class ="active">Nacimiento</label>
                                                <input id="nacimiento" name="FechaNacimiento" type="date"  >
                                            </div>
                                            <div class ="input-field col s6 ">
                                                <label for="sexo" class ="active">Sexo</label>
                                                <select id="Sexo" name="Sexo">
                                                    <option value="M" >Masculino</option>
                                                    <option value="F" >Femenino</option>
                                                </select>                                
                                            </div>
                            

                                            <div class ="input-field col s12 m6">
                                                <i class ="mdi-maps-pin-drop prefix"></i>
                                                <label for="txtdireccion">Direccion</label>
                                                <input id="txtdireccion" name="Direccion" type="text" >
                                            </div>
                                            <div class ="input-field col s12 m6">
                                                <i class ="mdi-maps-pin-drop prefix"></i>
                                                <label for="txtReferencia">Referencia</label>
                                                <input id="txtReferencia" name="Referencia" type="text" >
                                            </div>
                                            <div class ="input-field col s12">
                                                <button class ="btn waves-effect waves-light right" type="submit">
                                                    Guardar
                                                    <i class ="mdi-action-done right"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                        

                      </div>                     
                </div>
              `,
    methods: {
        cerrar() { $('.modalPersona').closeModal(); },
        CrearPersona() {
            console.log($('#formValidate').valid()); 
            //this.$emit('respuesta', this.PersonaId);
            //this.cerrar();
        }
    }
});