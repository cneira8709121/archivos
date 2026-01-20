
namespace Ruv.Data.Orfeo.ServiceImplementation.OrfeoCode {

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "Orfeo ExcelBinding", Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ObjectDatAnexArray))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ObjectRefUno))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ObjectRefDest))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ListaRadicados2))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ListaRadicados))]
    public partial class OrfeoCodeReference : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback loginOperationCompleted;
        
        private System.Threading.SendOrPostCallback usuarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback genera_secuenciaOperationCompleted;
        
        private System.Threading.SendOrPostCallback buscar_radicadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_dignatarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_dignatario2OperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_radicadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_radicado2OperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_radicado3OperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_direccionOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_anexoOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_eventhistOperationCompleted;
        
        private System.Threading.SendOrPostCallback recuperar_radicadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback recuperar_radicado2OperationCompleted;
        
        private System.Threading.SendOrPostCallback destinatariosOperationCompleted;
        
        private System.Threading.SendOrPostCallback seriesOperationCompleted;
        
        private System.Threading.SendOrPostCallback subseriesOperationCompleted;
        
        private System.Threading.SendOrPostCallback tipodocumentosOperationCompleted;
        
        private System.Threading.SendOrPostCallback insertar_tipificarOperationCompleted;
        
        private System.Threading.SendOrPostCallback radpathOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_radpathOperationCompleted;
        
        private System.Threading.SendOrPostCallback reasignarRadicadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback datosExpedienteOperationCompleted;
        
        private System.Threading.SendOrPostCallback datosExpediente2OperationCompleted;
        
        private System.Threading.SendOrPostCallback datosAnexoOperationCompleted;
        
        private System.Threading.SendOrPostCallback respuestaRadicadoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public OrfeoCodeReference() {
            this.Url = global::Ruv.Data.Orfeo.Properties.Settings.Default.CodeReferenceUrl;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) && (this.useDefaultCredentialsSetExplicitly == false))  && (this.IsLocalFileSystemWebService(value) == false))) { 
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event loginCompletedEventHandler loginCompleted;
        
        /// <remarks/>
        public event usuarioCompletedEventHandler usuarioCompleted;
        
        /// <remarks/>
        public event genera_secuenciaCompletedEventHandler genera_secuenciaCompleted;
        
        /// <remarks/>
        public event buscar_radicadoCompletedEventHandler buscar_radicadoCompleted;
        
        /// <remarks/>
        public event insert_dignatarioCompletedEventHandler insert_dignatarioCompleted;
        
        /// <remarks/>
        public event insert_dignatario2CompletedEventHandler insert_dignatario2Completed;
        
        /// <remarks/>
        public event insert_radicadoCompletedEventHandler insert_radicadoCompleted;
        
        /// <remarks/>
        public event insert_radicado2CompletedEventHandler insert_radicado2Completed;
        
        /// <remarks/>
        public event insert_radicado3CompletedEventHandler insert_radicado3Completed;
        
        /// <remarks/>
        public event insert_direccionCompletedEventHandler insert_direccionCompleted;
        
        /// <remarks/>
        public event insert_anexoCompletedEventHandler insert_anexoCompleted;
        
        /// <remarks/>
        public event insert_eventhistCompletedEventHandler insert_eventhistCompleted;
        
        /// <remarks/>
        public event recuperar_radicadoCompletedEventHandler recuperar_radicadoCompleted;
        
        /// <remarks/>
        public event recuperar_radicado2CompletedEventHandler recuperar_radicado2Completed;
        
        /// <remarks/>
        public event destinatariosCompletedEventHandler destinatariosCompleted;
        
        /// <remarks/>
        public event seriesCompletedEventHandler seriesCompleted;
        
        /// <remarks/>
        public event subseriesCompletedEventHandler subseriesCompleted;
        
        /// <remarks/>
        public event tipodocumentosCompletedEventHandler tipodocumentosCompleted;
        
        /// <remarks/>
        public event insertar_tipificarCompletedEventHandler insertar_tipificarCompleted;
        
        /// <remarks/>
        public event radpathCompletedEventHandler radpathCompleted;
        
        /// <remarks/>
        public event insert_radpathCompletedEventHandler insert_radpathCompleted;
        
        /// <remarks/>
        public event reasignarRadicadoCompletedEventHandler reasignarRadicadoCompleted;
        
        /// <remarks/>
        public event datosExpedienteCompletedEventHandler datosExpedienteCompleted;
        
        /// <remarks/>
        public event datosExpediente2CompletedEventHandler datosExpediente2Completed;
        
        /// <remarks/>
        public event datosAnexoCompletedEventHandler datosAnexoCompleted;
        
        /// <remarks/>
        public event respuestaRadicadoCompletedEventHandler respuestaRadicadoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public Usuario login(string username, string password) {
            object[] results = this.Invoke("login", new object[] {
                        username,
                        password});
            return ((Usuario)(results[0]));
        }
        
        /// <remarks/>
        public void loginAsync(string username, string password) {
            this.loginAsync(username, password, null);
        }
        
        /// <remarks/>
        public void loginAsync(string username, string password, object userState) {
            if ((this.loginOperationCompleted == null)) {
                this.loginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnloginOperationCompleted);
            }
            this.InvokeAsync("login", new object[] {
                        username,
                        password}, this.loginOperationCompleted, userState);
        }
        
        private void OnloginOperationCompleted(object arg) {
            if ((this.loginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public Usuario usuario(string username) {
            object[] results = this.Invoke("usuario", new object[] {
                        username});
            return ((Usuario)(results[0]));
        }
        
        /// <remarks/>
        public void usuarioAsync(string username) {
            this.usuarioAsync(username, null);
        }
        
        /// <remarks/>
        public void usuarioAsync(string username, object userState) {
            if ((this.usuarioOperationCompleted == null)) {
                this.usuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnusuarioOperationCompleted);
            }
            this.InvokeAsync("usuario", new object[] {
                        username}, this.usuarioOperationCompleted, userState);
        }
        
        private void OnusuarioOperationCompleted(object arg) {
            if ((this.usuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.usuarioCompleted(this, new usuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#genera_secuencia", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia genera_secuencia(string secuencia) {
            object[] results = this.Invoke("genera_secuencia", new object[] {
                        secuencia});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void genera_secuenciaAsync(string secuencia) {
            this.genera_secuenciaAsync(secuencia, null);
        }
        
        /// <remarks/>
        public void genera_secuenciaAsync(string secuencia, object userState) {
            if ((this.genera_secuenciaOperationCompleted == null)) {
                this.genera_secuenciaOperationCompleted = new System.Threading.SendOrPostCallback(this.Ongenera_secuenciaOperationCompleted);
            }
            this.InvokeAsync("genera_secuencia", new object[] {
                        secuencia}, this.genera_secuenciaOperationCompleted, userState);
        }
        
        private void Ongenera_secuenciaOperationCompleted(object arg) {
            if ((this.genera_secuenciaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.genera_secuenciaCompleted(this, new genera_secuenciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#genera_secuencia", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia buscar_radicado(string nroradicado) {
            object[] results = this.Invoke("buscar_radicado", new object[] {
                        nroradicado});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void buscar_radicadoAsync(string nroradicado) {
            this.buscar_radicadoAsync(nroradicado, null);
        }
        
        /// <remarks/>
        public void buscar_radicadoAsync(string nroradicado, object userState) {
            if ((this.buscar_radicadoOperationCompleted == null)) {
                this.buscar_radicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.Onbuscar_radicadoOperationCompleted);
            }
            this.InvokeAsync("buscar_radicado", new object[] {
                        nroradicado}, this.buscar_radicadoOperationCompleted, userState);
        }
        
        private void Onbuscar_radicadoOperationCompleted(object arg) {
            if ((this.buscar_radicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.buscar_radicadoCompleted(this, new buscar_radicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_dignatario", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_dignatario([System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string dpto, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string mpio) {
            object[] results = this.Invoke("insert_dignatario", new object[] {
                        tipdesrem,
                        nombre,
                        primerapell,
                        sgundoapell,
                        cedula,
                        direccion,
                        telefono,
                        nomentidad,
                        dpto,
                        mpio});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_dignatarioAsync(string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, string dpto, string mpio) {
            this.insert_dignatarioAsync(tipdesrem, nombre, primerapell, sgundoapell, cedula, direccion, telefono, nomentidad, dpto, mpio, null);
        }
        
        /// <remarks/>
        public void insert_dignatarioAsync(string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, string dpto, string mpio, object userState) {
            if ((this.insert_dignatarioOperationCompleted == null)) {
                this.insert_dignatarioOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_dignatarioOperationCompleted);
            }
            this.InvokeAsync("insert_dignatario", new object[] {
                        tipdesrem,
                        nombre,
                        primerapell,
                        sgundoapell,
                        cedula,
                        direccion,
                        telefono,
                        nomentidad,
                        dpto,
                        mpio}, this.insert_dignatarioOperationCompleted, userState);
        }
        
        private void Oninsert_dignatarioOperationCompleted(object arg) {
            if ((this.insert_dignatarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_dignatarioCompleted(this, new insert_dignatarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_dignatario", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_dignatario2([System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string dpto, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string mpio, string email) {
            object[] results = this.Invoke("insert_dignatario2", new object[] {
                        tipdesrem,
                        nombre,
                        primerapell,
                        sgundoapell,
                        cedula,
                        direccion,
                        telefono,
                        nomentidad,
                        dpto,
                        mpio,
                        email});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_dignatario2Async(string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, string dpto, string mpio, string email) {
            this.insert_dignatario2Async(tipdesrem, nombre, primerapell, sgundoapell, cedula, direccion, telefono, nomentidad, dpto, mpio, email, null);
        }
        
        /// <remarks/>
        public void insert_dignatario2Async(string tipdesrem, string nombre, string primerapell, string sgundoapell, string cedula, string direccion, string telefono, string nomentidad, string dpto, string mpio, string email, object userState) {
            if ((this.insert_dignatario2OperationCompleted == null)) {
                this.insert_dignatario2OperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_dignatario2OperationCompleted);
            }
            this.InvokeAsync("insert_dignatario2", new object[] {
                        tipdesrem,
                        nombre,
                        primerapell,
                        sgundoapell,
                        cedula,
                        direccion,
                        telefono,
                        nomentidad,
                        dpto,
                        mpio,
                        email}, this.insert_dignatario2OperationCompleted, userState);
        }
        
        private void Oninsert_dignatario2OperationCompleted(object arg) {
            if ((this.insert_dignatario2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_dignatario2Completed(this, new insert_dignatario2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_radicado", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_radicado(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe) {
            object[] results = this.Invoke("insert_radicado", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_radicadoAsync(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe) {
            this.insert_radicadoAsync(tiporad, deprad, depdest, codiusu, radifechofic, radentrada, descanex, asunto, nroofic, radpath, expe, null);
        }
        
        /// <remarks/>
        public void insert_radicadoAsync(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, object userState) {
            if ((this.insert_radicadoOperationCompleted == null)) {
                this.insert_radicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_radicadoOperationCompleted);
            }
            this.InvokeAsync("insert_radicado", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe}, this.insert_radicadoOperationCompleted, userState);
        }
        
        private void Oninsert_radicadoOperationCompleted(object arg) {
            if ((this.insert_radicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_radicadoCompleted(this, new insert_radicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_radicado2", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_radicado2(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, string radi) {
            object[] results = this.Invoke("insert_radicado2", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe,
                        radi});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_radicado2Async(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, string radi) {
            this.insert_radicado2Async(tiporad, deprad, depdest, codiusu, radifechofic, radentrada, descanex, asunto, nroofic, radpath, expe, radi, null);
        }
        
        /// <remarks/>
        public void insert_radicado2Async(int tiporad, int deprad, int depdest, int codiusu, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, string radi, object userState) {
            if ((this.insert_radicado2OperationCompleted == null)) {
                this.insert_radicado2OperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_radicado2OperationCompleted);
            }
            this.InvokeAsync("insert_radicado2", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe,
                        radi}, this.insert_radicado2OperationCompleted, userState);
        }
        
        private void Oninsert_radicado2OperationCompleted(object arg) {
            if ((this.insert_radicado2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_radicado2Completed(this, new insert_radicado2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_radicado", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_radicado3(int tiporad, int deprad, int depdest, int codiusu, int codiusudest, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, int medr) {
            object[] results = this.Invoke("insert_radicado3", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        codiusudest,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe,
                        medr});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_radicado3Async(int tiporad, int deprad, int depdest, int codiusu, int codiusudest, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, int medr) {
            this.insert_radicado3Async(tiporad, deprad, depdest, codiusu, codiusudest, radifechofic, radentrada, descanex, asunto, nroofic, radpath, expe, medr, null);
        }
        
        /// <remarks/>
        public void insert_radicado3Async(int tiporad, int deprad, int depdest, int codiusu, int codiusudest, string radifechofic, string radentrada, string descanex, string asunto, string nroofic, string radpath, string expe, int medr, object userState) {
            if ((this.insert_radicado3OperationCompleted == null)) {
                this.insert_radicado3OperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_radicado3OperationCompleted);
            }
            this.InvokeAsync("insert_radicado3", new object[] {
                        tiporad,
                        deprad,
                        depdest,
                        codiusu,
                        codiusudest,
                        radifechofic,
                        radentrada,
                        descanex,
                        asunto,
                        nroofic,
                        radpath,
                        expe,
                        medr}, this.insert_radicado3OperationCompleted, userState);
        }
        
        private void Oninsert_radicado3OperationCompleted(object arg) {
            if ((this.insert_radicado3Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_radicado3Completed(this, new insert_radicado3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_direccion", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_direccion([System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string tipdesrem, string coddir, string numradicado, string direccion, string dirtelefono, string dirnombre, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string coddpto, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string codmpio) {
            object[] results = this.Invoke("insert_direccion", new object[] {
                        tipdesrem,
                        coddir,
                        numradicado,
                        direccion,
                        dirtelefono,
                        dirnombre,
                        coddpto,
                        codmpio});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_direccionAsync(string tipdesrem, string coddir, string numradicado, string direccion, string dirtelefono, string dirnombre, string coddpto, string codmpio) {
            this.insert_direccionAsync(tipdesrem, coddir, numradicado, direccion, dirtelefono, dirnombre, coddpto, codmpio, null);
        }
        
        /// <remarks/>
        public void insert_direccionAsync(string tipdesrem, string coddir, string numradicado, string direccion, string dirtelefono, string dirnombre, string coddpto, string codmpio, object userState) {
            if ((this.insert_direccionOperationCompleted == null)) {
                this.insert_direccionOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_direccionOperationCompleted);
            }
            this.InvokeAsync("insert_direccion", new object[] {
                        tipdesrem,
                        coddir,
                        numradicado,
                        direccion,
                        dirtelefono,
                        dirnombre,
                        coddpto,
                        codmpio}, this.insert_direccionOperationCompleted, userState);
        }
        
        private void Oninsert_direccionOperationCompleted(object arg) {
            if ((this.insert_direccionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_direccionCompleted(this, new insert_direccionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_anexo", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_anexo(int tiporad, string numradicado, string tamanoarch, string creador, int deprad, string nombrearch, string desc) {
            object[] results = this.Invoke("insert_anexo", new object[] {
                        tiporad,
                        numradicado,
                        tamanoarch,
                        creador,
                        deprad,
                        nombrearch,
                        desc});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_anexoAsync(int tiporad, string numradicado, string tamanoarch, string creador, int deprad, string nombrearch, string desc) {
            this.insert_anexoAsync(tiporad, numradicado, tamanoarch, creador, deprad, nombrearch, desc, null);
        }
        
        /// <remarks/>
        public void insert_anexoAsync(int tiporad, string numradicado, string tamanoarch, string creador, int deprad, string nombrearch, string desc, object userState) {
            if ((this.insert_anexoOperationCompleted == null)) {
                this.insert_anexoOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_anexoOperationCompleted);
            }
            this.InvokeAsync("insert_anexo", new object[] {
                        tiporad,
                        numradicado,
                        tamanoarch,
                        creador,
                        deprad,
                        nombrearch,
                        desc}, this.insert_anexoOperationCompleted, userState);
        }
        
        private void Oninsert_anexoOperationCompleted(object arg) {
            if ((this.insert_anexoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_anexoCompleted(this, new insert_anexoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#insert_eventhist", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_eventhist(int tiporad, string numradicado, int deprad, int codiusu, int ttrcodi) {
            object[] results = this.Invoke("insert_eventhist", new object[] {
                        tiporad,
                        numradicado,
                        deprad,
                        codiusu,
                        ttrcodi});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_eventhistAsync(int tiporad, string numradicado, int deprad, int codiusu, int ttrcodi) {
            this.insert_eventhistAsync(tiporad, numradicado, deprad, codiusu, ttrcodi, null);
        }
        
        /// <remarks/>
        public void insert_eventhistAsync(int tiporad, string numradicado, int deprad, int codiusu, int ttrcodi, object userState) {
            if ((this.insert_eventhistOperationCompleted == null)) {
                this.insert_eventhistOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_eventhistOperationCompleted);
            }
            this.InvokeAsync("insert_eventhist", new object[] {
                        tiporad,
                        numradicado,
                        deprad,
                        codiusu,
                        ttrcodi}, this.insert_eventhistOperationCompleted, userState);
        }
        
        private void Oninsert_eventhistOperationCompleted(object arg) {
            if ((this.insert_eventhistCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_eventhistCompleted(this, new insert_eventhistCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#recuperar_radicado", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ListaRadicados[] recuperar_radicado(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad) {
            object[] results = this.Invoke("recuperar_radicado", new object[] {
                        fech_ini,
                        fech_fin,
                        depcod,
                        codiusu,
                        tiporad});
            return ((ListaRadicados[])(results[0]));
        }
        
        /// <remarks/>
        public void recuperar_radicadoAsync(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad) {
            this.recuperar_radicadoAsync(fech_ini, fech_fin, depcod, codiusu, tiporad, null);
        }
        
        /// <remarks/>
        public void recuperar_radicadoAsync(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad, object userState) {
            if ((this.recuperar_radicadoOperationCompleted == null)) {
                this.recuperar_radicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.Onrecuperar_radicadoOperationCompleted);
            }
            this.InvokeAsync("recuperar_radicado", new object[] {
                        fech_ini,
                        fech_fin,
                        depcod,
                        codiusu,
                        tiporad}, this.recuperar_radicadoOperationCompleted, userState);
        }
        
        private void Onrecuperar_radicadoOperationCompleted(object arg) {
            if ((this.recuperar_radicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.recuperar_radicadoCompleted(this, new recuperar_radicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#recuperar_radicado2", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ListaRadicados2[] recuperar_radicado2(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad) {
            object[] results = this.Invoke("recuperar_radicado2", new object[] {
                        fech_ini,
                        fech_fin,
                        depcod,
                        codiusu,
                        tiporad});
            return ((ListaRadicados2[])(results[0]));
        }
        
        /// <remarks/>
        public void recuperar_radicado2Async(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad) {
            this.recuperar_radicado2Async(fech_ini, fech_fin, depcod, codiusu, tiporad, null);
        }
        
        /// <remarks/>
        public void recuperar_radicado2Async(string fech_ini, string fech_fin, int depcod, int codiusu, int tiporad, object userState) {
            if ((this.recuperar_radicado2OperationCompleted == null)) {
                this.recuperar_radicado2OperationCompleted = new System.Threading.SendOrPostCallback(this.Onrecuperar_radicado2OperationCompleted);
            }
            this.InvokeAsync("recuperar_radicado2", new object[] {
                        fech_ini,
                        fech_fin,
                        depcod,
                        codiusu,
                        tiporad}, this.recuperar_radicado2OperationCompleted, userState);
        }
        
        private void Onrecuperar_radicado2OperationCompleted(object arg) {
            if ((this.recuperar_radicado2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.recuperar_radicado2Completed(this, new recuperar_radicado2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/destinatarios", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefDest[] destinatarios(int idpto, int idmpio, string criterio, int tipodestinario) {
            object[] results = this.Invoke("destinatarios", new object[] {
                        idpto,
                        idmpio,
                        criterio,
                        tipodestinario});
            return ((ObjectRefDest[])(results[0]));
        }
        
        /// <remarks/>
        public void destinatariosAsync(int idpto, int idmpio, string criterio, int tipodestinario) {
            this.destinatariosAsync(idpto, idmpio, criterio, tipodestinario, null);
        }
        
        /// <remarks/>
        public void destinatariosAsync(int idpto, int idmpio, string criterio, int tipodestinario, object userState) {
            if ((this.destinatariosOperationCompleted == null)) {
                this.destinatariosOperationCompleted = new System.Threading.SendOrPostCallback(this.OndestinatariosOperationCompleted);
            }
            this.InvokeAsync("destinatarios", new object[] {
                        idpto,
                        idmpio,
                        criterio,
                        tipodestinario}, this.destinatariosOperationCompleted, userState);
        }
        
        private void OndestinatariosOperationCompleted(object arg) {
            if ((this.destinatariosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.destinatariosCompleted(this, new destinatariosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/series", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefUno[] series(int codidepe) {
            object[] results = this.Invoke("series", new object[] {
                        codidepe});
            return ((ObjectRefUno[])(results[0]));
        }
        
        /// <remarks/>
        public void seriesAsync(int codidepe) {
            this.seriesAsync(codidepe, null);
        }
        
        /// <remarks/>
        public void seriesAsync(int codidepe, object userState) {
            if ((this.seriesOperationCompleted == null)) {
                this.seriesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnseriesOperationCompleted);
            }
            this.InvokeAsync("series", new object[] {
                        codidepe}, this.seriesOperationCompleted, userState);
        }
        
        private void OnseriesOperationCompleted(object arg) {
            if ((this.seriesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.seriesCompleted(this, new seriesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/subseries", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefUno[] subseries(int codidepe, int codiserie) {
            object[] results = this.Invoke("subseries", new object[] {
                        codidepe,
                        codiserie});
            return ((ObjectRefUno[])(results[0]));
        }
        
        /// <remarks/>
        public void subseriesAsync(int codidepe, int codiserie) {
            this.subseriesAsync(codidepe, codiserie, null);
        }
        
        /// <remarks/>
        public void subseriesAsync(int codidepe, int codiserie, object userState) {
            if ((this.subseriesOperationCompleted == null)) {
                this.subseriesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsubseriesOperationCompleted);
            }
            this.InvokeAsync("subseries", new object[] {
                        codidepe,
                        codiserie}, this.subseriesOperationCompleted, userState);
        }
        
        private void OnsubseriesOperationCompleted(object arg) {
            if ((this.subseriesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.subseriesCompleted(this, new subseriesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/tipodocumentos", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefUno[] tipodocumentos(int codidepe, int codiserie, int codisubserie) {
            object[] results = this.Invoke("tipodocumentos", new object[] {
                        codidepe,
                        codiserie,
                        codisubserie});
            return ((ObjectRefUno[])(results[0]));
        }
        
        /// <remarks/>
        public void tipodocumentosAsync(int codidepe, int codiserie, int codisubserie) {
            this.tipodocumentosAsync(codidepe, codiserie, codisubserie, null);
        }
        
        /// <remarks/>
        public void tipodocumentosAsync(int codidepe, int codiserie, int codisubserie, object userState) {
            if ((this.tipodocumentosOperationCompleted == null)) {
                this.tipodocumentosOperationCompleted = new System.Threading.SendOrPostCallback(this.OntipodocumentosOperationCompleted);
            }
            this.InvokeAsync("tipodocumentos", new object[] {
                        codidepe,
                        codiserie,
                        codisubserie}, this.tipodocumentosOperationCompleted, userState);
        }
        
        private void OntipodocumentosOperationCompleted(object arg) {
            if ((this.tipodocumentosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tipodocumentosCompleted(this, new tipodocumentosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/insertar_tipific" +
            "ar", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insertar_tipificar(string nroradicado, int usuacod, string usuadoc, int coddep, int codiserie, int codisubserie, int tipodoc) {
            object[] results = this.Invoke("insertar_tipificar", new object[] {
                        nroradicado,
                        usuacod,
                        usuadoc,
                        coddep,
                        codiserie,
                        codisubserie,
                        tipodoc});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insertar_tipificarAsync(string nroradicado, int usuacod, string usuadoc, int coddep, int codiserie, int codisubserie, int tipodoc) {
            this.insertar_tipificarAsync(nroradicado, usuacod, usuadoc, coddep, codiserie, codisubserie, tipodoc, null);
        }
        
        /// <remarks/>
        public void insertar_tipificarAsync(string nroradicado, int usuacod, string usuadoc, int coddep, int codiserie, int codisubserie, int tipodoc, object userState) {
            if ((this.insertar_tipificarOperationCompleted == null)) {
                this.insertar_tipificarOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsertar_tipificarOperationCompleted);
            }
            this.InvokeAsync("insertar_tipificar", new object[] {
                        nroradicado,
                        usuacod,
                        usuadoc,
                        coddep,
                        codiserie,
                        codisubserie,
                        tipodoc}, this.insertar_tipificarOperationCompleted, userState);
        }
        
        private void Oninsertar_tipificarOperationCompleted(object arg) {
            if ((this.insertar_tipificarCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insertar_tipificarCompleted(this, new insertar_tipificarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia radpath(string nroradicado) {
            object[] results = this.Invoke("radpath", new object[] {
                        nroradicado});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void radpathAsync(string nroradicado) {
            this.radpathAsync(nroradicado, null);
        }
        
        /// <remarks/>
        public void radpathAsync(string nroradicado, object userState) {
            if ((this.radpathOperationCompleted == null)) {
                this.radpathOperationCompleted = new System.Threading.SendOrPostCallback(this.OnradpathOperationCompleted);
            }
            this.InvokeAsync("radpath", new object[] {
                        nroradicado}, this.radpathOperationCompleted, userState);
        }
        
        private void OnradpathOperationCompleted(object arg) {
            if ((this.radpathCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.radpathCompleted(this, new radpathCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia insert_radpath(string path, string nroradicado) {
            object[] results = this.Invoke("insert_radpath", new object[] {
                        path,
                        nroradicado});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void insert_radpathAsync(string path, string nroradicado) {
            this.insert_radpathAsync(path, nroradicado, null);
        }
        
        /// <remarks/>
        public void insert_radpathAsync(string path, string nroradicado, object userState) {
            if ((this.insert_radpathOperationCompleted == null)) {
                this.insert_radpathOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_radpathOperationCompleted);
            }
            this.InvokeAsync("insert_radpath", new object[] {
                        path,
                        nroradicado}, this.insert_radpathOperationCompleted, userState);
        }
        
        private void Oninsert_radpathOperationCompleted(object arg) {
            if ((this.insert_radpathCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_radpathCompleted(this, new insert_radpathCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia reasignarRadicado(string numeroRadicado, string usuarioOrigen, string usuarioDestino, string comentario) {
            object[] results = this.Invoke("reasignarRadicado", new object[] {
                        numeroRadicado,
                        usuarioOrigen,
                        usuarioDestino,
                        comentario});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void reasignarRadicadoAsync(string numeroRadicado, string usuarioOrigen, string usuarioDestino, string comentario) {
            this.reasignarRadicadoAsync(numeroRadicado, usuarioOrigen, usuarioDestino, comentario, null);
        }
        
        /// <remarks/>
        public void reasignarRadicadoAsync(string numeroRadicado, string usuarioOrigen, string usuarioDestino, string comentario, object userState) {
            if ((this.reasignarRadicadoOperationCompleted == null)) {
                this.reasignarRadicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnreasignarRadicadoOperationCompleted);
            }
            this.InvokeAsync("reasignarRadicado", new object[] {
                        numeroRadicado,
                        usuarioOrigen,
                        usuarioDestino,
                        comentario}, this.reasignarRadicadoOperationCompleted, userState);
        }
        
        private void OnreasignarRadicadoOperationCompleted(object arg) {
            if ((this.reasignarRadicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.reasignarRadicadoCompleted(this, new reasignarRadicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectDatExpArray datosExpediente(string expediente) {
            object[] results = this.Invoke("datosExpediente", new object[] {
                        expediente});
            return ((ObjectDatExpArray)(results[0]));
        }
        
        /// <remarks/>
        public void datosExpedienteAsync(string expediente) {
            this.datosExpedienteAsync(expediente, null);
        }
        
        /// <remarks/>
        public void datosExpedienteAsync(string expediente, object userState) {
            if ((this.datosExpedienteOperationCompleted == null)) {
                this.datosExpedienteOperationCompleted = new System.Threading.SendOrPostCallback(this.OndatosExpedienteOperationCompleted);
            }
            this.InvokeAsync("datosExpediente", new object[] {
                        expediente}, this.datosExpedienteOperationCompleted, userState);
        }
        
        private void OndatosExpedienteOperationCompleted(object arg) {
            if ((this.datosExpedienteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.datosExpedienteCompleted(this, new datosExpedienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/datosExpediente2" +
            "", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectDatExpArray[] datosExpediente2(string expediente) {
            object[] results = this.Invoke("datosExpediente2", new object[] {
                        expediente});
            return ((ObjectDatExpArray[])(results[0]));
        }
        
        /// <remarks/>
        public void datosExpediente2Async(string expediente) {
            this.datosExpediente2Async(expediente, null);
        }
        
        /// <remarks/>
        public void datosExpediente2Async(string expediente, object userState) {
            if ((this.datosExpediente2OperationCompleted == null)) {
                this.datosExpediente2OperationCompleted = new System.Threading.SendOrPostCallback(this.OndatosExpediente2OperationCompleted);
            }
            this.InvokeAsync("datosExpediente2", new object[] {
                        expediente}, this.datosExpediente2OperationCompleted, userState);
        }
        
        private void OndatosExpediente2OperationCompleted(object arg) {
            if ((this.datosExpediente2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.datosExpediente2Completed(this, new datosExpediente2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/datosAnexo", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectDatAnexArray[] datosAnexo(string radicado) {
            object[] results = this.Invoke("datosAnexo", new object[] {
                        radicado});
            return ((ObjectDatAnexArray[])(results[0]));
        }
        
        /// <remarks/>
        public void datosAnexoAsync(string radicado) {
            this.datosAnexoAsync(radicado, null);
        }
        
        /// <remarks/>
        public void datosAnexoAsync(string radicado, object userState) {
            if ((this.datosAnexoOperationCompleted == null)) {
                this.datosAnexoOperationCompleted = new System.Threading.SendOrPostCallback(this.OndatosAnexoOperationCompleted);
            }
            this.InvokeAsync("datosAnexo", new object[] {
                        radicado}, this.datosAnexoOperationCompleted, userState);
        }
        
        private void OndatosAnexoOperationCompleted(object arg) {
            if ((this.datosAnexoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.datosAnexoCompleted(this, new datosAnexoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/masivaconnect3.php/respuestaRadicad" +
            "o", RequestNamespace="http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace="http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public numsecuencia respuestaRadicado(string nroradicado) {
            object[] results = this.Invoke("respuestaRadicado", new object[] {
                        nroradicado});
            return ((numsecuencia)(results[0]));
        }
        
        /// <remarks/>
        public void respuestaRadicadoAsync(string nroradicado) {
            this.respuestaRadicadoAsync(nroradicado, null);
        }
        
        /// <remarks/>
        public void respuestaRadicadoAsync(string nroradicado, object userState) {
            if ((this.respuestaRadicadoOperationCompleted == null)) {
                this.respuestaRadicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrespuestaRadicadoOperationCompleted);
            }
            this.InvokeAsync("respuestaRadicado", new object[] {
                        nroradicado}, this.respuestaRadicadoOperationCompleted, userState);
        }
        
        private void OnrespuestaRadicadoOperationCompleted(object arg) {
            if ((this.respuestaRadicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.respuestaRadicadoCompleted(this, new respuestaRadicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class Usuario
    {

        private string usuarioNombreField;

        private string usuarioLoginField;

        private string docIdentField;

        private int usaCodigoField;

        private int depCodigoField;

        private int perRadField;

        private int radmasivaField;

        /// <remarks/>
        public string UsuarioNombre
        {
            get
            {
                return this.usuarioNombreField;
            }
            set
            {
                this.usuarioNombreField = value;
            }
        }

        /// <remarks/>
        public string UsuarioLogin
        {
            get
            {
                return this.usuarioLoginField;
            }
            set
            {
                this.usuarioLoginField = value;
            }
        }

        /// <remarks/>
        public string DocIdent
        {
            get
            {
                return this.docIdentField;
            }
            set
            {
                this.docIdentField = value;
            }
        }

        /// <remarks/>
        public int UsaCodigo
        {
            get
            {
                return this.usaCodigoField;
            }
            set
            {
                this.usaCodigoField = value;
            }
        }

        /// <remarks/>
        public int DepCodigo
        {
            get
            {
                return this.depCodigoField;
            }
            set
            {
                this.depCodigoField = value;
            }
        }

        /// <remarks/>
        public int PerRad
        {
            get
            {
                return this.perRadField;
            }
            set
            {
                this.perRadField = value;
            }
        }

        /// <remarks/>
        public int radmasiva
        {
            get
            {
                return this.radmasivaField;
            }
            set
            {
                this.radmasivaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectDatAnexArray
    {

        private string anex_numeroField;

        private string path_anexoField;

        private string fecha_anexoField;

        private string tipo_documentoField;

        private string asuntoField;

        /// <remarks/>
        public string anex_numero
        {
            get
            {
                return this.anex_numeroField;
            }
            set
            {
                this.anex_numeroField = value;
            }
        }

        /// <remarks/>
        public string path_anexo
        {
            get
            {
                return this.path_anexoField;
            }
            set
            {
                this.path_anexoField = value;
            }
        }

        /// <remarks/>
        public string fecha_anexo
        {
            get
            {
                return this.fecha_anexoField;
            }
            set
            {
                this.fecha_anexoField = value;
            }
        }

        /// <remarks/>
        public string tipo_documento
        {
            get
            {
                return this.tipo_documentoField;
            }
            set
            {
                this.tipo_documentoField = value;
            }
        }

        /// <remarks/>
        public string asunto
        {
            get
            {
                return this.asuntoField;
            }
            set
            {
                this.asuntoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectDatExpArray
    {

        private string fecha_expField;

        private string usuario_creadorField;

        private string estadoField;

        private string serieField;

        private string subserieField;

        private string etiqueta1Field;

        private string etiqueta2Field;

        private string etiqueta3Field;

        /// <remarks/>
        public string fecha_exp
        {
            get
            {
                return this.fecha_expField;
            }
            set
            {
                this.fecha_expField = value;
            }
        }

        /// <remarks/>
        public string usuario_creador
        {
            get
            {
                return this.usuario_creadorField;
            }
            set
            {
                this.usuario_creadorField = value;
            }
        }

        /// <remarks/>
        public string estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = value;
            }
        }

        /// <remarks/>
        public string serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        public string subserie
        {
            get
            {
                return this.subserieField;
            }
            set
            {
                this.subserieField = value;
            }
        }

        /// <remarks/>
        public string etiqueta1
        {
            get
            {
                return this.etiqueta1Field;
            }
            set
            {
                this.etiqueta1Field = value;
            }
        }

        /// <remarks/>
        public string etiqueta2
        {
            get
            {
                return this.etiqueta2Field;
            }
            set
            {
                this.etiqueta2Field = value;
            }
        }

        /// <remarks/>
        public string etiqueta3
        {
            get
            {
                return this.etiqueta3Field;
            }
            set
            {
                this.etiqueta3Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectRefUno
    {

        private string idField;

        private string detalleField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(DataType = "integer")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string detalle
        {
            get
            {
                return this.detalleField;
            }
            set
            {
                this.detalleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectRefDest
    {

        private string departamentoField;

        private string municipioField;

        private string nombreField;

        private string apell1Field;

        private string apell2Field;

        private string nroducumentoField;

        private string entidadField;

        private string direccionField;

        private string telefonoField;

        private string tipodesField;

        /// <remarks/>
        public string departamento
        {
            get
            {
                return this.departamentoField;
            }
            set
            {
                this.departamentoField = value;
            }
        }

        /// <remarks/>
        public string municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string apell1
        {
            get
            {
                return this.apell1Field;
            }
            set
            {
                this.apell1Field = value;
            }
        }

        /// <remarks/>
        public string apell2
        {
            get
            {
                return this.apell2Field;
            }
            set
            {
                this.apell2Field = value;
            }
        }

        /// <remarks/>
        public string nroducumento
        {
            get
            {
                return this.nroducumentoField;
            }
            set
            {
                this.nroducumentoField = value;
            }
        }

        /// <remarks/>
        public string entidad
        {
            get
            {
                return this.entidadField;
            }
            set
            {
                this.entidadField = value;
            }
        }

        /// <remarks/>
        public string direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }

        /// <remarks/>
        public string telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }

        /// <remarks/>
        public string tipodes
        {
            get
            {
                return this.tipodesField;
            }
            set
            {
                this.tipodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ListaRadicados2
    {

        private string tiporadField;

        private string nroradicadoField;

        private string fecharadField;

        private string asuntoField;

        private string direccionField;

        private string telefonoField;

        private string tipodestField;

        private string nfolioField;

        private string dignatarioField;

        private string pathField;

        private ListaDest destinatarioField;

        /// <remarks/>
        public string tiporad
        {
            get
            {
                return this.tiporadField;
            }
            set
            {
                this.tiporadField = value;
            }
        }

        /// <remarks/>
        public string nroradicado
        {
            get
            {
                return this.nroradicadoField;
            }
            set
            {
                this.nroradicadoField = value;
            }
        }

        /// <remarks/>
        public string fecharad
        {
            get
            {
                return this.fecharadField;
            }
            set
            {
                this.fecharadField = value;
            }
        }

        /// <remarks/>
        public string asunto
        {
            get
            {
                return this.asuntoField;
            }
            set
            {
                this.asuntoField = value;
            }
        }

        /// <remarks/>
        public string direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }

        /// <remarks/>
        public string telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }

        /// <remarks/>
        public string tipodest
        {
            get
            {
                return this.tipodestField;
            }
            set
            {
                this.tipodestField = value;
            }
        }

        /// <remarks/>
        public string nfolio
        {
            get
            {
                return this.nfolioField;
            }
            set
            {
                this.nfolioField = value;
            }
        }

        /// <remarks/>
        public string dignatario
        {
            get
            {
                return this.dignatarioField;
            }
            set
            {
                this.dignatarioField = value;
            }
        }

        /// <remarks/>
        public string path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }

        /// <remarks/>
        public ListaDest destinatario
        {
            get
            {
                return this.destinatarioField;
            }
            set
            {
                this.destinatarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ListaDest
    {

        private string ciucedulaField;

        private string ciunombreField;

        private string ciuapell1Field;

        private string ciuapell2Field;

        private string departamentoField;

        private string municipioField;

        private string entidadField;

        /// <remarks/>
        public string ciucedula
        {
            get
            {
                return this.ciucedulaField;
            }
            set
            {
                this.ciucedulaField = value;
            }
        }

        /// <remarks/>
        public string ciunombre
        {
            get
            {
                return this.ciunombreField;
            }
            set
            {
                this.ciunombreField = value;
            }
        }

        /// <remarks/>
        public string ciuapell1
        {
            get
            {
                return this.ciuapell1Field;
            }
            set
            {
                this.ciuapell1Field = value;
            }
        }

        /// <remarks/>
        public string ciuapell2
        {
            get
            {
                return this.ciuapell2Field;
            }
            set
            {
                this.ciuapell2Field = value;
            }
        }

        /// <remarks/>
        public string departamento
        {
            get
            {
                return this.departamentoField;
            }
            set
            {
                this.departamentoField = value;
            }
        }

        /// <remarks/>
        public string municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }

        /// <remarks/>
        public string entidad
        {
            get
            {
                return this.entidadField;
            }
            set
            {
                this.entidadField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ListaRadicados
    {

        private string tiporadField;

        private string nroradicadoField;

        private string asuntoField;

        private string direccionField;

        private string telefonoField;

        private string tipodestField;

        private ListaDest destinatarioField;

        /// <remarks/>
        public string tiporad
        {
            get
            {
                return this.tiporadField;
            }
            set
            {
                this.tiporadField = value;
            }
        }

        /// <remarks/>
        public string nroradicado
        {
            get
            {
                return this.nroradicadoField;
            }
            set
            {
                this.nroradicadoField = value;
            }
        }

        /// <remarks/>
        public string asunto
        {
            get
            {
                return this.asuntoField;
            }
            set
            {
                this.asuntoField = value;
            }
        }

        /// <remarks/>
        public string direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }

        /// <remarks/>
        public string telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }

        /// <remarks/>
        public string tipodest
        {
            get
            {
                return this.tipodestField;
            }
            set
            {
                this.tipodestField = value;
            }
        }

        /// <remarks/>
        public ListaDest destinatario
        {
            get
            {
                return this.destinatarioField;
            }
            set
            {
                this.destinatarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class numsecuencia
    {

        private string secuenciaField;

        private string estadoField;

        /// <remarks/>
        public string secuencia
        {
            get
            {
                return this.secuenciaField;
            }
            set
            {
                this.secuenciaField = value;
            }
        }

        /// <remarks/>
        public string estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void loginCompletedEventHandler(object sender, loginCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class loginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal loginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Usuario Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Usuario)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void usuarioCompletedEventHandler(object sender, usuarioCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class usuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal usuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Usuario Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Usuario)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void genera_secuenciaCompletedEventHandler(object sender, genera_secuenciaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class genera_secuenciaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal genera_secuenciaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void buscar_radicadoCompletedEventHandler(object sender, buscar_radicadoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class buscar_radicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal buscar_radicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_dignatarioCompletedEventHandler(object sender, insert_dignatarioCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_dignatarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_dignatarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_dignatario2CompletedEventHandler(object sender, insert_dignatario2CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_dignatario2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_dignatario2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_radicadoCompletedEventHandler(object sender, insert_radicadoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_radicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_radicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_radicado2CompletedEventHandler(object sender, insert_radicado2CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_radicado2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_radicado2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_radicado3CompletedEventHandler(object sender, insert_radicado3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_radicado3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_radicado3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_direccionCompletedEventHandler(object sender, insert_direccionCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_direccionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_direccionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_anexoCompletedEventHandler(object sender, insert_anexoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_anexoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_anexoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_eventhistCompletedEventHandler(object sender, insert_eventhistCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_eventhistCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_eventhistCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void recuperar_radicadoCompletedEventHandler(object sender, recuperar_radicadoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class recuperar_radicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal recuperar_radicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ListaRadicados[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaRadicados[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void recuperar_radicado2CompletedEventHandler(object sender, recuperar_radicado2CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class recuperar_radicado2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal recuperar_radicado2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ListaRadicados2[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaRadicados2[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void destinatariosCompletedEventHandler(object sender, destinatariosCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class destinatariosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal destinatariosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectRefDest[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefDest[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void seriesCompletedEventHandler(object sender, seriesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class seriesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal seriesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectRefUno[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefUno[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void subseriesCompletedEventHandler(object sender, subseriesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class subseriesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal subseriesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectRefUno[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefUno[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void tipodocumentosCompletedEventHandler(object sender, tipodocumentosCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tipodocumentosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal tipodocumentosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectRefUno[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefUno[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insertar_tipificarCompletedEventHandler(object sender, insertar_tipificarCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insertar_tipificarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insertar_tipificarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void radpathCompletedEventHandler(object sender, radpathCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class radpathCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal radpathCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void insert_radpathCompletedEventHandler(object sender, insert_radpathCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_radpathCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insert_radpathCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void reasignarRadicadoCompletedEventHandler(object sender, reasignarRadicadoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class reasignarRadicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal reasignarRadicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void datosExpedienteCompletedEventHandler(object sender, datosExpedienteCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class datosExpedienteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal datosExpedienteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectDatExpArray Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectDatExpArray)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void datosExpediente2CompletedEventHandler(object sender, datosExpediente2CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class datosExpediente2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal datosExpediente2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectDatExpArray[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectDatExpArray[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void datosAnexoCompletedEventHandler(object sender, datosAnexoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class datosAnexoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal datosAnexoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectDatAnexArray[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectDatAnexArray[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void respuestaRadicadoCompletedEventHandler(object sender, respuestaRadicadoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class respuestaRadicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal respuestaRadicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public numsecuencia Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((numsecuencia)(this.results[0]));
            }
        }
    }

}
