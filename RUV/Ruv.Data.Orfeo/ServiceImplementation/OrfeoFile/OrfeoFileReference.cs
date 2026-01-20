
namespace Ruv.Data.Orfeo.ServiceImplementation.OrfeoFile {

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "metodos scanBinding", Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ObjectRefTRD))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ObjectRefLista))]
    public partial class OrfeoFileReference : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback loginOperationCompleted;

        private System.Threading.SendOrPostCallback usuarioOperationCompleted;

        private System.Threading.SendOrPostCallback radicados_usuarioOperationCompleted;

        private System.Threading.SendOrPostCallback registrarOperationCompleted;

        private System.Threading.SendOrPostCallback seriesOperationCompleted;

        private System.Threading.SendOrPostCallback subseriesOperationCompleted;

        private System.Threading.SendOrPostCallback tipodocumentosOperationCompleted;

        private System.Threading.SendOrPostCallback tipificarOperationCompleted;

        private System.Threading.SendOrPostCallback anexararchivoOperationCompleted;

        private System.Threading.SendOrPostCallback nombreanexoOperationCompleted;

        private System.Threading.SendOrPostCallback noty_prestamosOperationCompleted;

        private System.Threading.SendOrPostCallback UploadFileOperationCompleted;

        private System.Threading.SendOrPostCallback GetDirectoryOperationCompleted;

        private System.Threading.SendOrPostCallback publicarOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public OrfeoFileReference() {
            this.Url = global::Ruv.Data.Orfeo.Properties.Settings.Default.FileReferenceUrl;
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
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) && (this.useDefaultCredentialsSetExplicitly == false)) && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
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
        public event radicados_usuarioCompletedEventHandler radicados_usuarioCompleted;

        /// <remarks/>
        public event registrarCompletedEventHandler registrarCompleted;

        /// <remarks/>
        public event seriesCompletedEventHandler seriesCompleted;

        /// <remarks/>
        public event subseriesCompletedEventHandler subseriesCompleted;

        /// <remarks/>
        public event tipodocumentosCompletedEventHandler tipodocumentosCompleted;

        /// <remarks/>
        public event tipificarCompletedEventHandler tipificarCompleted;

        /// <remarks/>
        public event anexararchivoCompletedEventHandler anexararchivoCompleted;

        /// <remarks/>
        public event nombreanexoCompletedEventHandler nombreanexoCompleted;

        /// <remarks/>
        public event noty_prestamosCompletedEventHandler noty_prestamosCompleted;

        /// <remarks/>
        public event UploadFileCompletedEventHandler UploadFileCompleted;

        /// <remarks/>
        public event GetDirectoryCompletedEventHandler GetDirectoryCompleted;

        /// <remarks/>
        public event publicarCompletedEventHandler publicarCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public Usuario login(string username, string password)
        {
            object[] results = this.Invoke("login", new object[] {
                        username,
                        password});
            return ((Usuario)(results[0]));
        }

        /// <remarks/>
        public void loginAsync(string username, string password)
        {
            this.loginAsync(username, password, null);
        }

        /// <remarks/>
        public void loginAsync(string username, string password, object userState)
        {
            if ((this.loginOperationCompleted == null))
            {
                this.loginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnloginOperationCompleted);
            }
            this.InvokeAsync("login", new object[] {
                        username,
                        password}, this.loginOperationCompleted, userState);
        }

        private void OnloginOperationCompleted(object arg)
        {
            if ((this.loginCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.loginCompleted(this, new loginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public Usuario usuario(string username)
        {
            object[] results = this.Invoke("usuario", new object[] {
                        username});
            return ((Usuario)(results[0]));
        }

        /// <remarks/>
        public void usuarioAsync(string username)
        {
            this.usuarioAsync(username, null);
        }

        /// <remarks/>
        public void usuarioAsync(string username, object userState)
        {
            if ((this.usuarioOperationCompleted == null))
            {
                this.usuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnusuarioOperationCompleted);
            }
            this.InvokeAsync("usuario", new object[] {
                        username}, this.usuarioOperationCompleted, userState);
        }

        private void OnusuarioOperationCompleted(object arg)
        {
            if ((this.usuarioCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.usuarioCompleted(this, new usuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefLista[] radicados_usuario(string username, string inifecha, string finfecha, string criterio)
        {
            object[] results = this.Invoke("radicados_usuario", new object[] {
                        username,
                        inifecha,
                        finfecha,
                        criterio});
            return ((ObjectRefLista[])(results[0]));
        }

        /// <remarks/>
        public void radicados_usuarioAsync(string username, string inifecha, string finfecha, string criterio)
        {
            this.radicados_usuarioAsync(username, inifecha, finfecha, criterio, null);
        }

        /// <remarks/>
        public void radicados_usuarioAsync(string username, string inifecha, string finfecha, string criterio, object userState)
        {
            if ((this.radicados_usuarioOperationCompleted == null))
            {
                this.radicados_usuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.Onradicados_usuarioOperationCompleted);
            }
            this.InvokeAsync("radicados_usuario", new object[] {
                        username,
                        inifecha,
                        finfecha,
                        criterio}, this.radicados_usuarioOperationCompleted, userState);
        }

        private void Onradicados_usuarioOperationCompleted(object arg)
        {
            if ((this.radicados_usuarioCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.radicados_usuarioCompleted(this, new radicados_usuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datdigitalizador registrar(string pathimagen, int nropaginas, string nroradicado, string usudigitalizador)
        {
            object[] results = this.Invoke("registrar", new object[] {
                        pathimagen,
                        nropaginas,
                        nroradicado,
                        usudigitalizador});
            return ((datdigitalizador)(results[0]));
        }

        /// <remarks/>
        public void registrarAsync(string pathimagen, int nropaginas, string nroradicado, string usudigitalizador)
        {
            this.registrarAsync(pathimagen, nropaginas, nroradicado, usudigitalizador, null);
        }

        /// <remarks/>
        public void registrarAsync(string pathimagen, int nropaginas, string nroradicado, string usudigitalizador, object userState)
        {
            if ((this.registrarOperationCompleted == null))
            {
                this.registrarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnregistrarOperationCompleted);
            }
            this.InvokeAsync("registrar", new object[] {
                        pathimagen,
                        nropaginas,
                        nroradicado,
                        usudigitalizador}, this.registrarOperationCompleted, userState);
        }

        private void OnregistrarOperationCompleted(object arg)
        {
            if ((this.registrarCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.registrarCompleted(this, new registrarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/upload.php/series", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefTRD[] series(string username, string password, int codidepe)
        {
            object[] results = this.Invoke("series", new object[] {
                        username,
                        password,
                        codidepe});
            return ((ObjectRefTRD[])(results[0]));
        }

        /// <remarks/>
        public void seriesAsync(string username, string password, int codidepe)
        {
            this.seriesAsync(username, password, codidepe, null);
        }

        /// <remarks/>
        public void seriesAsync(string username, string password, int codidepe, object userState)
        {
            if ((this.seriesOperationCompleted == null))
            {
                this.seriesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnseriesOperationCompleted);
            }
            this.InvokeAsync("series", new object[] {
                        username,
                        password,
                        codidepe}, this.seriesOperationCompleted, userState);
        }

        private void OnseriesOperationCompleted(object arg)
        {
            if ((this.seriesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.seriesCompleted(this, new seriesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/upload.php/subseries", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefTRD[] subseries(string username, string password, int codidepe, int codiserie)
        {
            object[] results = this.Invoke("subseries", new object[] {
                        username,
                        password,
                        codidepe,
                        codiserie});
            return ((ObjectRefTRD[])(results[0]));
        }

        /// <remarks/>
        public void subseriesAsync(string username, string password, int codidepe, int codiserie)
        {
            this.subseriesAsync(username, password, codidepe, codiserie, null);
        }

        /// <remarks/>
        public void subseriesAsync(string username, string password, int codidepe, int codiserie, object userState)
        {
            if ((this.subseriesOperationCompleted == null))
            {
                this.subseriesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsubseriesOperationCompleted);
            }
            this.InvokeAsync("subseries", new object[] {
                        username,
                        password,
                        codidepe,
                        codiserie}, this.subseriesOperationCompleted, userState);
        }

        private void OnsubseriesOperationCompleted(object arg)
        {
            if ((this.subseriesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.subseriesCompleted(this, new subseriesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://orfeo.unidadvictimas.gov.co/webservice/upload.php/tipodocumentos", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public ObjectRefTRD[] tipodocumentos(string username, string password, int codidepe, int codiserie, int codisubserie)
        {
            object[] results = this.Invoke("tipodocumentos", new object[] {
                        username,
                        password,
                        codidepe,
                        codiserie,
                        codisubserie});
            return ((ObjectRefTRD[])(results[0]));
        }

        /// <remarks/>
        public void tipodocumentosAsync(string username, string password, int codidepe, int codiserie, int codisubserie)
        {
            this.tipodocumentosAsync(username, password, codidepe, codiserie, codisubserie, null);
        }

        /// <remarks/>
        public void tipodocumentosAsync(string username, string password, int codidepe, int codiserie, int codisubserie, object userState)
        {
            if ((this.tipodocumentosOperationCompleted == null))
            {
                this.tipodocumentosOperationCompleted = new System.Threading.SendOrPostCallback(this.OntipodocumentosOperationCompleted);
            }
            this.InvokeAsync("tipodocumentos", new object[] {
                        username,
                        password,
                        codidepe,
                        codiserie,
                        codisubserie}, this.tipodocumentosOperationCompleted, userState);
        }

        private void OntipodocumentosOperationCompleted(object arg)
        {
            if ((this.tipodocumentosCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tipodocumentosCompleted(this, new tipodocumentosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datdigitalizador tipificar(string username, string nroradicado, int codiserie, int codisubserie, int tipodoc)
        {
            object[] results = this.Invoke("tipificar", new object[] {
                        username,
                        nroradicado,
                        codiserie,
                        codisubserie,
                        tipodoc});
            return ((datdigitalizador)(results[0]));
        }

        /// <remarks/>
        public void tipificarAsync(string username, string nroradicado, int codiserie, int codisubserie, int tipodoc)
        {
            this.tipificarAsync(username, nroradicado, codiserie, codisubserie, tipodoc, null);
        }

        /// <remarks/>
        public void tipificarAsync(string username, string nroradicado, int codiserie, int codisubserie, int tipodoc, object userState)
        {
            if ((this.tipificarOperationCompleted == null))
            {
                this.tipificarOperationCompleted = new System.Threading.SendOrPostCallback(this.OntipificarOperationCompleted);
            }
            this.InvokeAsync("tipificar", new object[] {
                        username,
                        nroradicado,
                        codiserie,
                        codisubserie,
                        tipodoc}, this.tipificarOperationCompleted, userState);
        }

        private void OntipificarOperationCompleted(object arg)
        {
            if ((this.tipificarCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.tipificarCompleted(this, new tipificarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datdigitalizador anexararchivo(string username, string nroradicado, int anextipo, double tamano, string solectura, string codTrd, string anexdesc)
        {
            object[] results = this.Invoke("anexararchivo", new object[] {
                        username,
                        nroradicado,
                        anextipo,
                        tamano,
                        solectura,
                        codTrd,
                        anexdesc});
            return ((datdigitalizador)(results[0]));
        }

        /// <remarks/>
        public void anexararchivoAsync(string username, string nroradicado, int anextipo, double tamano, string solectura, string codTrd, string anexdesc)
        {
            this.anexararchivoAsync(username, nroradicado, anextipo, tamano, solectura, codTrd, anexdesc, null);
        }

        /// <remarks/>
        public void anexararchivoAsync(string username, string nroradicado, int anextipo, double tamano, string solectura, string codTrd, string anexdesc, object userState)
        {
            if ((this.anexararchivoOperationCompleted == null))
            {
                this.anexararchivoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnanexararchivoOperationCompleted);
            }
            this.InvokeAsync("anexararchivo", new object[] {
                        username,
                        nroradicado,
                        anextipo,
                        tamano,
                        solectura,
                        codTrd,
                        anexdesc}, this.anexararchivoOperationCompleted, userState);
        }

        private void OnanexararchivoOperationCompleted(object arg)
        {
            if ((this.anexararchivoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.anexararchivoCompleted(this, new anexararchivoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#login", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public datdigitalizador nombreanexo(string nroradicado, string filename)
        {
            object[] results = this.Invoke("nombreanexo", new object[] {
                        nroradicado,
                        filename});
            return ((datdigitalizador)(results[0]));
        }

        /// <remarks/>
        public void nombreanexoAsync(string nroradicado, string filename)
        {
            this.nombreanexoAsync(nroradicado, filename, null);
        }

        /// <remarks/>
        public void nombreanexoAsync(string nroradicado, string filename, object userState)
        {
            if ((this.nombreanexoOperationCompleted == null))
            {
                this.nombreanexoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnombreanexoOperationCompleted);
            }
            this.InvokeAsync("nombreanexo", new object[] {
                        nroradicado,
                        filename}, this.nombreanexoOperationCompleted, userState);
        }

        private void OnnombreanexoOperationCompleted(object arg)
        {
            if ((this.nombreanexoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.nombreanexoCompleted(this, new nombreanexoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#noty_prestamos", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public radinotifica noty_prestamos(string nroradicado, string usunotifica)
        {
            object[] results = this.Invoke("noty_prestamos", new object[] {
                        nroradicado,
                        usunotifica});
            return ((radinotifica)(results[0]));
        }

        /// <remarks/>
        public void noty_prestamosAsync(string nroradicado, string usunotifica)
        {
            this.noty_prestamosAsync(nroradicado, usunotifica, null);
        }

        /// <remarks/>
        public void noty_prestamosAsync(string nroradicado, string usunotifica, object userState)
        {
            if ((this.noty_prestamosOperationCompleted == null))
            {
                this.noty_prestamosOperationCompleted = new System.Threading.SendOrPostCallback(this.Onnoty_prestamosOperationCompleted);
            }
            this.InvokeAsync("noty_prestamos", new object[] {
                        nroradicado,
                        usunotifica}, this.noty_prestamosOperationCompleted, userState);
        }

        private void Onnoty_prestamosOperationCompleted(object arg)
        {
            if ((this.noty_prestamosCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.noty_prestamosCompleted(this, new noty_prestamosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#UploadFile", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string UploadFile(string bytes, string filename)
        {
            object[] results = this.Invoke("UploadFile", new object[] {
                        bytes,
                        filename});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void UploadFileAsync(string bytes, string filename)
        {
            this.UploadFileAsync(bytes, filename, null);
        }

        /// <remarks/>
        public void UploadFileAsync(string bytes, string filename, object userState)
        {
            if ((this.UploadFileOperationCompleted == null))
            {
                this.UploadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileOperationCompleted);
            }
            this.InvokeAsync("UploadFile", new object[] {
                        bytes,
                        filename}, this.UploadFileOperationCompleted, userState);
        }

        private void OnUploadFileOperationCompleted(object arg)
        {
            if ((this.UploadFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileCompleted(this, new UploadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#GetDirectory", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string GetDirectory(string numradicado)
        {
            object[] results = this.Invoke("GetDirectory", new object[] {
                        numradicado});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GetDirectoryAsync(string numradicado)
        {
            this.GetDirectoryAsync(numradicado, null);
        }

        /// <remarks/>
        public void GetDirectoryAsync(string numradicado, object userState)
        {
            if ((this.GetDirectoryOperationCompleted == null))
            {
                this.GetDirectoryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDirectoryOperationCompleted);
            }
            this.InvokeAsync("GetDirectory", new object[] {
                        numradicado}, this.GetDirectoryOperationCompleted, userState);
        }

        private void OnGetDirectoryOperationCompleted(object arg)
        {
            if ((this.GetDirectoryCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDirectoryCompleted(this, new GetDirectoryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:orfeoconnect#publicar", RequestNamespace = "http://orfeo.unidadvictimas.gov.co/webservice", ResponseNamespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public string publicar(string numradicado, string bytes, string filename)
        {
            object[] results = this.Invoke("publicar", new object[] {
                        numradicado,
                        bytes,
                        filename});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void publicarAsync(string numradicado, string bytes, string filename)
        {
            this.publicarAsync(numradicado, bytes, filename, null);
        }

        /// <remarks/>
        public void publicarAsync(string numradicado, string bytes, string filename, object userState)
        {
            if ((this.publicarOperationCompleted == null))
            {
                this.publicarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpublicarOperationCompleted);
            }
            this.InvokeAsync("publicar", new object[] {
                        numradicado,
                        bytes,
                        filename}, this.publicarOperationCompleted, userState);
        }

        private void OnpublicarOperationCompleted(object arg)
        {
            if ((this.publicarCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.publicarCompleted(this, new publicarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class radinotifica
    {

        private string errorField;

        private string mensajeField;

        private string nroradiField;

        private string usunomField;

        private string usuemailField;

        private string fecradField;

        private string radiasunField;

        private string radipathField;

        /// <remarks/>
        public string error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        public string mensaje
        {
            get
            {
                return this.mensajeField;
            }
            set
            {
                this.mensajeField = value;
            }
        }

        /// <remarks/>
        public string nroradi
        {
            get
            {
                return this.nroradiField;
            }
            set
            {
                this.nroradiField = value;
            }
        }

        /// <remarks/>
        public string usunom
        {
            get
            {
                return this.usunomField;
            }
            set
            {
                this.usunomField = value;
            }
        }

        /// <remarks/>
        public string usuemail
        {
            get
            {
                return this.usuemailField;
            }
            set
            {
                this.usuemailField = value;
            }
        }

        /// <remarks/>
        public string fecrad
        {
            get
            {
                return this.fecradField;
            }
            set
            {
                this.fecradField = value;
            }
        }

        /// <remarks/>
        public string radiasun
        {
            get
            {
                return this.radiasunField;
            }
            set
            {
                this.radiasunField = value;
            }
        }

        /// <remarks/>
        public string radipath
        {
            get
            {
                return this.radipathField;
            }
            set
            {
                this.radipathField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectRefTRD
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
    public partial class datdigitalizador
    {

        private string errorField;

        private string mensajeField;

        /// <remarks/>
        public string error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        public string mensaje
        {
            get
            {
                return this.mensajeField;
            }
            set
            {
                this.mensajeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://orfeo.unidadvictimas.gov.co/webservice")]
    public partial class ObjectRefLista
    {

        private string nroradicadoField;

        private string fechradicadoField;

        private string depradicadoField;

        private string depnombradicadoField;

        private string asunradicadoField;

        private string nrohojasradicadoField;

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
        public string fechradicado
        {
            get
            {
                return this.fechradicadoField;
            }
            set
            {
                this.fechradicadoField = value;
            }
        }

        /// <remarks/>
        public string depradicado
        {
            get
            {
                return this.depradicadoField;
            }
            set
            {
                this.depradicadoField = value;
            }
        }

        /// <remarks/>
        public string depnombradicado
        {
            get
            {
                return this.depnombradicadoField;
            }
            set
            {
                this.depnombradicadoField = value;
            }
        }

        /// <remarks/>
        public string asunradicado
        {
            get
            {
                return this.asunradicadoField;
            }
            set
            {
                this.asunradicadoField = value;
            }
        }

        /// <remarks/>
        public string nrohojasradicado
        {
            get
            {
                return this.nrohojasradicadoField;
            }
            set
            {
                this.nrohojasradicadoField = value;
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
    public delegate void radicados_usuarioCompletedEventHandler(object sender, radicados_usuarioCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class radicados_usuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal radicados_usuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ObjectRefLista[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefLista[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void registrarCompletedEventHandler(object sender, registrarCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class registrarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal registrarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public datdigitalizador Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((datdigitalizador)(this.results[0]));
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
        public ObjectRefTRD[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefTRD[])(this.results[0]));
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
        public ObjectRefTRD[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefTRD[])(this.results[0]));
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
        public ObjectRefTRD[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ObjectRefTRD[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void tipificarCompletedEventHandler(object sender, tipificarCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class tipificarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal tipificarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public datdigitalizador Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((datdigitalizador)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void anexararchivoCompletedEventHandler(object sender, anexararchivoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class anexararchivoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal anexararchivoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public datdigitalizador Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((datdigitalizador)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void nombreanexoCompletedEventHandler(object sender, nombreanexoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class nombreanexoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal nombreanexoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public datdigitalizador Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((datdigitalizador)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void noty_prestamosCompletedEventHandler(object sender, noty_prestamosCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class noty_prestamosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal noty_prestamosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public radinotifica Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((radinotifica)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void UploadFileCompletedEventHandler(object sender, UploadFileCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetDirectoryCompletedEventHandler(object sender, GetDirectoryCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDirectoryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetDirectoryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void publicarCompletedEventHandler(object sender, publicarCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class publicarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal publicarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
