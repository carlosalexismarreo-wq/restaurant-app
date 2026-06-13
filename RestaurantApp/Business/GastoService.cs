using System;
using System.Collections.Generic;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar gastos adicionales
    /// </summary>
    public class GastoService
    {
        private const string TABLE_NAME = "Gastos";

        public bool RegistrarGasto(int usuarioID, string descripcion, decimal monto, 
                                  int monedaID, string categoriaGasto)
        {
            try
            {
                Gasto gasto = new Gasto
                {
                    FechaGasto = DateTime.Now,
                    UsuarioID = usuarioID,
                    Descripcion = descripcion,
                    Monto = monto,
                    MonedaID = monedaID,
                    CategoriaGasto = categoriaGasto,
                    EsContabilizado = false
                };

                using (var conn = Data.DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME}
                                    (FechaGasto, UsuarioID, Descripcion, Monto, MonedaID, CategoriaGasto, EsContabilizado)
                                    VALUES (@FechaGasto, @UsuarioID, @Descripcion, @Monto, @MonedaID, @CategoriaGasto, @EsContabilizado)";
                    
                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FechaGasto", gasto.FechaGasto);
                        cmd.Parameters.AddWithValue("@UsuarioID", gasto.UsuarioID);
                        cmd.Parameters.AddWithValue("@Descripcion", gasto.Descripcion);
                        cmd.Parameters.AddWithValue("@Monto", gasto.Monto);
                        cmd.Parameters.AddWithValue("@MonedaID", gasto.MonedaID);
                        cmd.Parameters.AddWithValue("@CategoriaGasto", gasto.CategoriaGasto);
                        cmd.Parameters.AddWithValue("@EsContabilizado", gasto.EsContabilizado);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar gasto: " + ex.Message);
            }
        }
    }
}
