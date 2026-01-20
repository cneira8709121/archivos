using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;
using System.Threading;
using System.Globalization;
using Ruv.WebSite.Common;
using Ruv.WebSite.Utilidades.Controles;

    public class PaginaBase : System.Web.UI.Page {
        
        protected override void InitializeCulture() {
            string Culture = "es-CO";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Culture);
            
            base.InitializeCulture();
        }

        protected virtual void Page_PreInit(object sender, EventArgs e) {
            if (!Page.User.Identity.IsAuthenticated) {
                Response.Redirect("~/Login.aspx"); //?message=Se ha detectado que no está autenticado. Por favor, ingrese sus credenciales.");
            }
            if (!UserAuthenticatedBySession()) {
                Response.Redirect("~/Login.aspx?message=Se ha perdido la información de usuario. Por favor, ingrese de nuevo.");
            }
        }

        protected override void OnError(EventArgs e) {
            GC.Collect();
            base.OnError(e);
        }

        protected IModalPopUp ModalPopUp {
            get { return this.Master.FindControlRecursively("generalPopup") as IModalPopUp; }
        }

        protected bool UserAuthenticatedBySession() { 
            if (HttpContext.Current.Session[ConstantesSesion.USUARIO] == null) {
                // Verify if its possible to recreate
                try {
                    var sessionCookie = Request.Cookies["RUVSessionID"];
                    if (sessionCookie != null) {
                        clsCryptoUtil cifrado = new clsCryptoUtil();
                        LoginService loginService = new LoginService();
                        var contents = cifrado.DecryptStringFixed(sessionCookie.Value).Split('|');
                        var authenticatedUser = loginService.Authenticate(contents[0], cifrado.DecryptStringFixed(contents[1]), new Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed(), cifrado.EncryptStringFixed(DateTime.Now.ToString("yyyyMMddHHmmss")));
                        if (string.IsNullOrEmpty(authenticatedUser.MensajeAutenticacionFallida)) {
                            if (!string.IsNullOrEmpty(authenticatedUser.Contraseña))
                                authenticatedUser.Contraseña = cifrado.EncryptStringFixed(authenticatedUser.Contraseña);
                            Session[ConstantesSesion.USUARIO] = authenticatedUser;
                            Session[ConstantesSesion.USUARIO_ID_LOGIN] = authenticatedUser.Id;
                        }
                    }
                }
                catch { }
            }
            return HttpContext.Current.Session[ConstantesSesion.USUARIO] != null;
        }

    }
