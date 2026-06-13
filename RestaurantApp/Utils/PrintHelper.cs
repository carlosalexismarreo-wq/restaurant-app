using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace RestaurantApp.Utils
{
    /// <summary>
    /// Helper para operaciones de impresión
    /// </summary>
    public class PrintHelper
    {
        public static void ImprimirTicket(string contenido, string nombreImpresora = null)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                
                if (!string.IsNullOrEmpty(nombreImpresora))
                {
                    pd.PrinterSettings.PrinterName = nombreImpresora;
                }

                pd.PrintPage += (sender, e) =>
                {
                    e.Graphics.DrawString(contenido, new System.Drawing.Font("Courier New", 10), 
                        System.Drawing.Brushes.Black, new System.Drawing.PointF(0, 0));
                };

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GenerarTicketVenta(string numeroVenta, DateTime fecha, decimal monto, decimal descuento, decimal total)
        {
            string ticket = "";
            ticket += "═════════════════════════════════════\n";
            ticket += "          TICKET DE VENTA             \n";
            ticket += "═════════════════════════════════════\n";
            ticket += $"Número: {numeroVenta}\n";
            ticket += $"Fecha: {fecha:dd/MM/yyyy HH:mm:ss}\n";
            ticket += "─────────────────────────────────────\n";
            ticket += $"Monto: {monto:C}\n";
            ticket += $"Descuento: {descuento:C}\n";
            ticket += "───��─────────────────────────────────\n";
            ticket += $"Total: {total:C}\n";
            ticket += "═════════════════════════════════════\n";
            ticket += $"Impreso: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n";

            return ticket;
        }

        public static string GenerarTicketCierre(int numeroCierre, decimal saldoInicial, decimal totalVentas, 
                                                decimal totalEgresos, decimal saldoFinal)
        {
            string ticket = "";
            ticket += "═════════════════════════════════════\n";
            ticket += "         CIERRE DE CAJA              \n";
            ticket += "═════════════════════════════════════\n";
            ticket += $"Número de Cierre: {numeroCierre}\n";
            ticket += $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n";
            ticket += "─────────────────────────────────────\n";
            ticket += $"Saldo Inicial: {saldoInicial:C}\n";
            ticket += $"Total Ventas: {totalVentas:C}\n";
            ticket += $"Total Egresos: {totalEgresos:C}\n";
            ticket += "─────────────────────────────────────\n";
            ticket += $"Saldo Final: {saldoFinal:C}\n";
            ticket += "═════════════════════════════════════\n";

            return ticket;
        }
    }
}
