Vue.component('crearAtencion', {
    props: ['origen', 'destino'],
    mounted: function () {
        var self = this;
        $.get('./Cajadiario/ObtenerBoveda', {}, function (d) {
            self.CajaOrigen = d;
        });
        //fetch('./Cajadiario/ObtenerBoveda')
        //    .then(function (response) {
        //        return response.json();
        //    }).then(function (result) {
        //        this.CajaOrigen = result;
        //    });
    },
    data: function () {
        return {
            CajaOrigen: {},
            CajaDestino: {}
        }
    },
    template: `
                <div>
                    <div id="modalCrearAtencion" class="modal">
                      <div class="modal-content">
                          <div id="input-select" class="row">
                                <div class="col s12 ">
                                      <input type="hidden" id="PersonaId" name="PersonaId" value="0" />
                                        <i class ="mdi-action-search prefix"></i>
                                        <input id="autocompletar" type="text"
                                               data-url='./Comun/ListarPersonas'
                                               data-seleccion="PersonaId" value=""
                                         />
                                        <label class ="right" for="autocompletar" data-error="No existe la persona." data-success="Excelente!!! Persona encontrada.">Buscar Persona</label>
                                </div>
                          </div>
                          <div class ="row" >
                                <div class="input-field col s6 right" >
                                      <i class ="mdi-editor-attach-money prefix"></i>
                                      <input id="txtImporteModal" type="text" class ="validate">
                                      <label for="txtImporteModal" style="width:0">Importe</label>
                                </div>
                          </div>
                      </div>
                      <div class="modal-footer">
                        <a href="#" class="btn waves-effect waves-red btn-flat modal-action modal-close">Cerrar</a>
                        <a href="#" class="btn waves-effect waves-green btn-flat modal-action modal-close">Transferir</a>
                      </div>
                    </div>
                </div>
              `,
});