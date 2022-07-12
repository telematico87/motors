using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class ProductoCaracteristica
    {
        public Motor motor { get; set; }
        public Frenos frenos { get; set; }
        public Suspension suspension { get; set; }
        public Consumo consumo { get; set; }
        public Transmisions transmisiones { get; set; }
        public Dimensiones dimensiones { get; set; }
        public Destacados destacados { get; set; } 
        public AroLLanta arollanta { get; set; }


        public ProductoCaracteristica DefinirNulos()
        {

            ProductoCaracteristica productoCaracteristica = new ProductoCaracteristica();

            productoCaracteristica.motor = new Motor();
            productoCaracteristica.frenos = new Frenos();
            productoCaracteristica.suspension = new Suspension();
            productoCaracteristica.consumo = new Consumo();
            productoCaracteristica.transmisiones = new Transmisions();
            productoCaracteristica.dimensiones = new Dimensiones();
            productoCaracteristica.destacados = new Destacados();
            productoCaracteristica.arollanta = new AroLLanta();
            return productoCaracteristica;

        }

        public ProductoCaracteristica()
        {
            motor = new Motor();
            frenos = new Frenos();
            suspension = new Suspension();
            consumo = new Consumo();
            transmisiones = new Transmisions();
            dimensiones = new Dimensiones();
            destacados = new Destacados();
            arollanta = new AroLLanta();
        }
    }

    public class Motor
    {
        public string Cilindrada { get; set; }
        public string NroCilindrada { get; set; }
        public string Potencia { get; set; }
        public string TipoMotor { get; set; }
        public string SistemaEnfriamiento { get; set; }
        public string SistemaEncendido { get; set; }
        public string SistemaArranque { get; set; }
        public string Torque { get; set; }

        public Motor()
        {
            Cilindrada = null;
            NroCilindrada = null;
            Potencia = null;
            TipoMotor = null;
            SistemaEnfriamiento = null;
            SistemaEncendido = null;
            SistemaArranque = null;
            Torque = null;
        }
    }

    public class Frenos
    {
        public string FrenoDelantero { get; set; }
        public string FrenoTrasero { get; set; }

        public Frenos()
        {
            FrenoDelantero = null;
            FrenoTrasero = null;
        }
    }

    public class AroLLanta
    {

        public string NeumaticoDelantero{ get; set; }
        public string NeumaticoPosterior { get; set; }
        public string AroDelantero { get; set; }
        public string AroPosterior { get; set; }

        public AroLLanta()
        {
            NeumaticoDelantero = null;
            NeumaticoPosterior = null;
            AroDelantero = null;
            AroPosterior = null;
        }
    }

    public class Suspension  {

        public string SuspensionDelantera { get; set; } 
        public string SuspensionPosterior { get; set; }

        public Suspension()
        {
            SuspensionDelantera = null;
            SuspensionPosterior = null;
        }
    }
     
    public class Consumo
    {

        public string Octanaje { get; set; }
        public string SistemaCombustible { get; set; }
        public string CapacidadTanque { get; set; }
        public string Autonomia { get; set; }
        public string RendimientoGalon { get; set; }

        public Consumo()
        {
            Octanaje = null;
            SistemaCombustible = null;
            CapacidadTanque = null;
            Autonomia = null;
            RendimientoGalon = null;
        }
    }

    public class Transmisions
    {

        public string Transmision { get; set; }
        public string NroCambios { get; set; }
        public string VelocidadMaxima { get; set; }

        public Transmisions()
        {
            Transmision = null;
            NroCambios = null;
            VelocidadMaxima = null;
        }
    }

    public class Dimensiones
    {

        public string Peso { get; set; }
        public string CargaUtil { get; set; }
        public string Pasajeros { get; set; }

        public Dimensiones()
        {
            Peso = null;
            CargaUtil = null;
            Pasajeros = null;
        }
    }

    public class Destacados
    {

        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public string Texto3 { get; set; }
        public string Texto4 { get; set; }
        public string Compacta { get; set; }
        public string AroRayos { get; set; }
        public string ColoresDisponibles { get; set; }
        public string Adicionales { get; set; }

        public string Tablero { get; set; }
        //public string Garantia { get; set; }

        public Destacados()
        {
            Texto1 = null;
            Texto2 = null;
            Texto3 = null;
            Texto4 = null;
            Compacta = null;
            AroRayos = null;
            ColoresDisponibles = null;
            Adicionales = null;
            Tablero = null;
            //Garantia = null;
        }
    }
}
