using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Impamb.Query
{
    public class EstudiosImpactoAmbientalQuery
    {
        Modelo.EstudiosImpactoAmbiental dsEstudioImpactoAmbiental = new Modelo.EstudiosImpactoAmbiental();

        #region LlenadoDelegaciones

        public DataTable DropDownDelegaciones(DataTable dt)
        {
            try
            {
                var qry = from eia in dsEstudioImpactoAmbiental.TM_ESTUDIOS_IMPAMB
                          from ua in dsEstudioImpactoAmbiental.TC_UNIDAD_ADMINISTRATIVA
                          where eia.COD_DELEG_INGRESO == ua.CODIGO_UNIDAD_ADMINISTRATIVA && ua.DESCRIPCION.Contains("DELEGACION") 
                          group ua by new{
                            ua.CODIGO_UNIDAD_ADMINISTRATIVA,
                            ua.DESCRIPCION
                          } into ua1
                          select new
                          {
                              UnidadCodigo = ua1.Key.CODIGO_UNIDAD_ADMINISTRATIVA,
                              UnidadAdministrativa = ua1.Key.DESCRIPCION
                          };

                foreach (var a in qry)
                {
                    dt.Rows.Add(a.UnidadCodigo, a.UnidadAdministrativa);
                }
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }         
        #endregion

        #region

        public DataTable DropDownTipoInstrumentos(DataTable dt) {
            try
            {
                var qry = from ti in dsEstudioImpactoAmbiental.TC_INSTRUMENTOS
                          select new
                          { 
                              CodigoInstrumento = ti.ESTATUS,
                              DescripcionInstrumento = ti.DESCRIPCION
                          };
                foreach (var a in qry) {
                    dt.Rows.Add(a.CodigoInstrumento, a.DescripcionInstrumento);
                }
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region

        public DataTable grdEIA(DataTable dt) {
            try
            {
                var qry = from ei in dsEstudioImpactoAmbiental.TM_ESTUDIOS_IMPAMB
                          where ei.FECHA_RECIBIDO_NOTIFICACION != null && ei.FECHA_NOTIFICACION == null
                          select new
                          {
                              Periodo = ei.PERIODO,
                              NumeroEstudio = ei.NUMERO_ESTUDIO,
                              NombreProyecto = ei.NOMBRE_PROYECTO,
                              RepresentanteLegal = ei.REPRESENTANTE_LEGAL,
                              DireccionProyecto = ei.DIRECCION_PROYECTO,
                              FechaRecibidaNotificar = ei.FECHA_RECIBIDO_NOTIFICACION
                          };

                foreach (var a in qry) {
                    dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                }

                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region

        public DataTable grBusquedaPorCampos(DataTable dt, int delegacion, String tipoinstrumento, int periodo, String numerostudio)
        {
            try
            {
                var qry = from ei in dsEstudioImpactoAmbiental.TM_ESTUDIOS_IMPAMB
                          where ei.FECHA_RECIBIDO_NOTIFICACION != null && ei.FECHA_NOTIFICACION == null
                          select new
                          {
                              Periodo = ei.PERIODO,
                              Deleg = ei.COD_DELEG_INGRESO,
                              instrumento = ei.TIPO,
                              NumeroEstudio = ei.NUMERO_ESTUDIO,
                              NombreProyecto = ei.NOMBRE_PROYECTO,
                              RepresentanteLegal = ei.REPRESENTANTE_LEGAL,
                              DireccionProyecto = ei.DIRECCION_PROYECTO,
                              FechaRecibidaNotificar = ei.FECHA_RECIBIDO_NOTIFICACION
                          };

                if (delegacion == 0 && tipoinstrumento == "" && periodo == 0 && numerostudio =="") {

                    foreach (var a in qry)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                #region FiltroDelegacion

                else if (delegacion != 0 && tipoinstrumento == "" && periodo == 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento != "" && periodo == 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.instrumento == tipoinstrumento);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento == "" && periodo != 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.Periodo == periodo );

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento == "" && periodo == 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento != "" && periodo != 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.instrumento == tipoinstrumento && w.Periodo == periodo);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento != "" && periodo == 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.instrumento == tipoinstrumento && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento == "" && periodo != 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.Periodo == periodo && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion != 0 && tipoinstrumento != "" && periodo != 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.Deleg == delegacion && w.instrumento == tipoinstrumento && w.Periodo == periodo && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }
                #endregion

                #region Filtro TipoInstrumento

                else if (delegacion == 0 && tipoinstrumento != "" && periodo == 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.instrumento == tipoinstrumento );

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion == 0 && tipoinstrumento != "" && periodo != 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.instrumento == tipoinstrumento && w.Periodo == periodo );

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion == 0 && tipoinstrumento != "" && periodo == 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.instrumento == tipoinstrumento && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion == 0 && tipoinstrumento != "" && periodo != 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.instrumento == tipoinstrumento && w.Periodo == periodo && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                #endregion

                #region Filtro Periodo

                else if (delegacion == 0 && tipoinstrumento == "" && periodo != 0 && numerostudio == "")
                {

                    var qryResult = qry.Where(w => w.Periodo == periodo );

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                else if (delegacion == 0 && tipoinstrumento == "" && periodo != 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.Periodo == periodo && w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }

                //else if (delegacion != 0 && tipoinstrumento == "" && periodo != 0 && numerostudio != "")
                //{

                //    var qryResult = qry.Where(w => w.Deleg == delegacion && w.Periodo == periodo && w.NumeroEstudio == numerostudio);

                //    foreach (var a in qryResult)
                //    {
                //        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                //    }
                //}
                #endregion

                #region

                else if (delegacion == 0 && tipoinstrumento == "" && periodo == 0 && numerostudio != "")
                {

                    var qryResult = qry.Where(w => w.NumeroEstudio == numerostudio);

                    foreach (var a in qryResult)
                    {
                        dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
                    }
                }
                #endregion


                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        //#region

        //public DataTable grdBusquedaAvanzada(DataTable dt, String var) {
        //    try
        //    {
        //        var qry = from ei in dsEstudioImpactoAmbiental.TM_ESTUDIOS_IMPAMB
        //                  where Convert.ToString(ei.PERIODO).Contains(var) || ei.NUMERO_ESTUDIO.Contains(var) || ei.NOMBRE_PROYECTO.Contains(var) || ei.REPRESENTANTE_LEGAL.Contains(var)
        //                  || ei.DIRECCION_PROYECTO.Contains(var) || Convert.ToString(ei.FECHA_RECIBIDO_NOTIFICACION).Contains(var)
        //                  select new
        //                  {
        //                      Periodo = ei.PERIODO,
        //                      NumeroEstudio = ei.NUMERO_ESTUDIO,
        //                      NombreProyecto = ei.NOMBRE_PROYECTO,
        //                      RepresentanteLegal = ei.REPRESENTANTE_LEGAL,
        //                      DireccionProyecto = ei.DIRECCION_PROYECTO,
        //                      FechaRecibidaNotificar = ei.FECHA_RECIBIDO_NOTIFICACION
        //                  };

        //        foreach (var a in qry)
        //        {
        //            dt.Rows.Add(a.Periodo, a.NumeroEstudio, a.NombreProyecto, a.RepresentanteLegal, a.DireccionProyecto, a.FechaRecibidaNotificar);
        //        }

        //        return dt;
        //    }
        //    catch (Exception ex) { throw ex; }
            
        //}
        //#endregion
    }
}