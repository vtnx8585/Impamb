using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Impamb
{
    public partial class EstudiosDeImpactoAmbiental : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                DataTable dtDelegacion = new DataTable();
                DataTable dtTipoInstrumento = new DataTable();
                DataTable dtExpedientes = new DataTable();
                DataTable dtPeriodo = new DataTable();

                try
                {
                    dtDelegacion.Columns.Add("UnidadCodigo", Type.GetType("System.String"));
                    dtDelegacion.Columns.Add("UnidadAdministrativa", Type.GetType("System.String"));

                    dtTipoInstrumento.Columns.Add("CodigoInstrumento", Type.GetType("System.String"));
                    dtTipoInstrumento.Columns.Add("DescripcionInstrumento", Type.GetType("System.String"));

                    dtExpedientes.Columns.Add("Periodo", Type.GetType("System.String"));
                    dtExpedientes.Columns.Add("NumeroEstudio", Type.GetType("System.String"));
                    dtExpedientes.Columns.Add("NombreProyecto", Type.GetType("System.String"));
                    dtExpedientes.Columns.Add("RepresentanteLegal", Type.GetType("System.String"));
                    dtExpedientes.Columns.Add("DireccionProyecto", Type.GetType("System.String"));
                    dtExpedientes.Columns.Add("FechaRecibidaNotificar", Type.GetType("System.String"));

                    dtPeriodo.Columns.Add("Anio", Type.GetType("System.String"));

                    Session["dtExpedientes"] = dtExpedientes;

                    Query.EstudiosImpactoAmbientalQuery qry = new Query.EstudiosImpactoAmbientalQuery();
                    qry.DropDownDelegaciones(dtDelegacion);
                    ddlDelegaciones.DataSource = dtDelegacion;
                    ddlDelegaciones.DataTextField = "UnidadAdministrativa";
                    ddlDelegaciones.DataValueField = "UnidadCodigo";
                    ddlDelegaciones.DataBind();

                    qry.DropDownTipoInstrumentos(dtTipoInstrumento);
                    ddlTipoInstrumento.DataSource = dtTipoInstrumento;
                    ddlTipoInstrumento.DataTextField = "DescripcionInstrumento";
                    ddlTipoInstrumento.DataValueField = "CodigoInstrumento";
                    ddlTipoInstrumento.DataBind();

                    qry.grdEIA(dtExpedientes);
                    grdExpedienteInstrumentoAmbiental.DataSource = dtExpedientes;
                    grdExpedienteInstrumentoAmbiental.DataBind();

                    
                    for (int i = 2007; i <= DateTime.Today.Year; i++) {
                        dtPeriodo.Rows.Add(i);
                    }
                    ddlPeriodo.DataSource = dtPeriodo;
                    ddlPeriodo.DataTextField = "Anio";
                    ddlPeriodo.DataValueField = "Anio";
                    ddlPeriodo.DataBind();
                }
                catch (Exception ex) { throw ex; }
            }                        
        }

        protected void grdExpedienteInstrumentoAmbiental_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExpedienteInstrumentoAmbiental.PageIndex = e.NewPageIndex;
            DataTable DT = new DataTable();
            DT = (DataTable)Session["dtExpedientes"];
            grdExpedienteInstrumentoAmbiental.DataSource = DT;
            grdExpedienteInstrumentoAmbiental.DataBind();

        }

        protected void btnEjecutarBusqueda_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["dtExpedientes"];
                dt.Clear();
                int delegacion;
                int periodo;
                String tipoinstrumento;
                String numeroestudio;
                lblMensajeExpediente.InnerText = "";

                if (ddlDelegaciones.Text == "0")
                {
                    delegacion = 0;
                }
                else {
                    delegacion = Convert.ToInt32(ddlDelegaciones.Text);
                }

                if (ddlPeriodo.Text == "0")
                {
                    periodo = 0;
                }
                else {
                    periodo = Convert.ToInt32(ddlPeriodo.Text);
                }

                if (ddlTipoInstrumento.Text == "0")
                {
                    tipoinstrumento = "";
                }
                else {
                    tipoinstrumento = ddlTipoInstrumento.Text;
                }

                if (txtNumeroEstudio.Text == "")
                {
                    numeroestudio = "";
                }
                else {
                    numeroestudio = txtNumeroEstudio.Text;
                }

                Query.EstudiosImpactoAmbientalQuery qry = new Query.EstudiosImpactoAmbientalQuery();
                qry.grBusquedaPorCampos(dt, delegacion, tipoinstrumento, periodo, numeroestudio);
                grdExpedienteInstrumentoAmbiental.DataSource = dt;
                grdExpedienteInstrumentoAmbiental.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMensajeExpediente.InnerText = "No se encontraron expedientes para notificar";
                }

                ddlDelegaciones.ClearSelection();
                ddlPeriodo.ClearSelection();
                ddlTipoInstrumento.ClearSelection();
                txtNumeroEstudio.Dispose();
            }
            catch (Exception ex) { throw ex; }
        }

    }
}