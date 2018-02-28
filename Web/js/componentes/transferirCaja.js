Vue.component('tranferirCaja', {
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
                    <div id="modalTransferenciaCaja" class="modal">
                      <div class="modal-content">
                          <div id="input-select" class="row">
                                <div class="col s12 ">
                                      <div class="input-field col s12 m6 l6">
                                        <label>Caja Origen</label>
                                        <select>
                                          <option value="" disabled selected>Choose your option</option>
                                          <option value="1">Option 1</option>
                                          <option value="3">Option 3</option>
                                        </select>
    {{this.CajaOrigen}}
                                      </div>
                                      <div class="input-field col s12 m6 l6">
                                        <label>Caja Destino</label>
                                        <select>
                                          <option value="" disabled selected>Choose your option</option>
                                          <option value="1">Option 1</option>
                                          <option value="3">Option 3</option>
                                        </select>
                                      </div>
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