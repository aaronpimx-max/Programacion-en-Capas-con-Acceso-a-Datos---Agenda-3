using System;
using System.Collections.Generic;
using System.Globalization;
using Entidades;
using Negocio;

namespace Presentacion
{
    internal class Program
    {
        private static readonly ContactoNegocio _negocio = new ContactoNegocio();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== AGENDA DE CONTACTOS (V2 + CUENTA CTE) ===");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Buscar contacto");
                Console.WriteLine("3. Modificar contacto (por DNI)");
                Console.WriteLine("4. Eliminar contacto (por DNI)");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                string op = Console.ReadLine()?.Trim();

                try
                {
                    switch (op)
                    {
                        case "1":
                            AgregarContacto();
                            break;
                        case "2":
                            BuscarContacto();
                            break;
                        case "3":
                            ModificarContacto();
                            break;
                        case "4":
                            EliminarContacto();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Revisá el formato de la fecha (yyyy-MM-dd) o del límite de crédito.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void AgregarContacto()
        {
            var c = LeerDatosContacto(dniFijo: null);
            _negocio.Agregar(c);
            Console.WriteLine("Contacto y cuenta corriente agregados correctamente.");
        }

        private static void ModificarContacto()
        {
            Console.Write("DNI del contacto a modificar: ");
            string dni = Console.ReadLine()?.Trim();

            var c = LeerDatosContacto(dniFijo: dni);
            _negocio.Modificar(c);
            Console.WriteLine("Contacto y cuenta corriente modificados correctamente.");
        }

        private static void EliminarContacto()
        {
            Console.Write("DNI del contacto a eliminar: ");
            string dni = Console.ReadLine()?.Trim();

            _negocio.Eliminar(dni);
            Console.WriteLine("Contacto y cuenta corriente eliminados correctamente.");
        }

        private static void BuscarContacto()
        {
            Console.WriteLine("Buscar por: 1-DNI | 2-APELLIDO | 3-NOMBRES | 4-CALLE");
            Console.Write("Elija criterio: ");
            string critOp = Console.ReadLine()?.Trim();

            string criterio;
            switch (critOp)
            {
                case "1":
                    criterio = "DNI";
                    break;
                case "2":
                    criterio = "APELLIDO";
                    break;
                case "3":
                    criterio = "NOMBRES";
                    break;
                case "4":
                    criterio = "CALLE";
                    break;
                default:
                    Console.WriteLine("Criterio inválido.");
                    return;
            }

            Console.Write($"Ingrese {criterio}: ");
            string valor = Console.ReadLine()?.Trim();

            List<Contacto> resultados = _negocio.Buscar(criterio, valor);
            if (resultados.Count == 0)
            {
                Console.WriteLine("No se encontraron contactos.");
                return;
            }

            foreach (var c in resultados)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"DNI: {c.Dni} | CUIL/CUIT: {c.CuilCuit} | Estado: {c.Estado} | Alta: {c.FechaAlta:yyyy-MM-dd}");
                Console.WriteLine($"Apellido y Nombres: {c.Apellido}, {c.Nombres}");
                Console.WriteLine($"Dirección: {c.Calle} | Piso: {c.Piso} | Depto: {c.Depto} | Barrio: {c.Barrio} | CP: {c.CodigoPostal} | {c.Ciudad}, {c.Provincia}");
                Console.WriteLine($"Tel: {c.Telefono} | Tel Alt: {c.TelefonoAlternativo} | Email: {c.Email} | IG: {c.Instagram}");
                Console.WriteLine($"Nacionalidad: {c.Nacionalidad} | Estado Civil: {c.EstadoCivil} | Estudios: {c.NivelEstudios}");
                Console.WriteLine($"Profesión: {c.ProfesionOcupacion} | Empresa: {c.EmpresaLugarTrabajo} | Pago: {c.MetodoPagoPreferido}");
                Console.WriteLine($"Observaciones: {c.ObservacionesNotas}");

                if (c.CuentaCorriente != null && c.CuentaCorriente.Id > 0)
                {
                    Console.WriteLine(
                        $"Cuenta Cte #{c.CuentaCorriente.Id} | Apertura: {c.CuentaCorriente.FechaApertura:yyyy-MM-dd} | " +
                        $"Límite: ${c.CuentaCorriente.LimiteCredito:N2} | Estado Crédito: {c.CuentaCorriente.EstadoCredito}"
                    );
                }
                else
                {
                    Console.WriteLine("Cuenta Cte: Sin cuenta corriente registrada.");
                }
            }
        }

        private static Contacto LeerDatosContacto(string dniFijo)
        {
            string dni = dniFijo == null ? PedirTexto("DNI: ") : dniFijo.Trim();
            string apellido = PedirTexto("Apellido: ");
            string nombres = PedirTexto("Nombres: ");
            string cuilCuit = PedirTexto("CUIL/CUIT: ");

            Console.Write("Fecha de alta (yyyy-MM-dd, Enter = hoy): ");
            string fechaStr = Console.ReadLine()?.Trim();
            DateTime fechaAlta = string.IsNullOrEmpty(fechaStr)
                ? DateTime.Today
                : DateTime.ParseExact(fechaStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            string estadoCivil = PedirTexto("Estado civil: ");
            string nacionalidad = PedirTexto("Nacionalidad: ");
            string calle = PedirTexto("Calle: ");
            string depto = PedirTexto("Depto (opcional): ");
            string piso = PedirTexto("Piso (opcional): ");
            string barrio = PedirTexto("Barrio: ");
            string codigoPostal = PedirTexto("Código postal: ");
            string ciudad = PedirTexto("Ciudad: ");
            string provincia = PedirTexto("Provincia: ");
            string telefono = PedirTexto("Teléfono: ");
            string telefonoAlternativo = PedirTexto("Teléfono alternativo: ");
            string email = PedirTexto("Email: ");
            string instagram = PedirTexto("Instagram (@usuario): ");
            string profesion = PedirTexto("Profesión/Ocupación: ");
            string empresa = PedirTexto("Empresa/Lugar de trabajo: ");
            string nivelEstudios = PedirTexto("Nivel de estudios: ");

            Console.Write("Estado (ACTIVO / INACTIVO, Enter = ACTIVO): ");
            string estado = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(estado)) estado = "ACTIVO";

            string metodoPago = PedirTexto("Método de pago preferido: ");
            string observaciones = PedirTexto("Observaciones/Notas: ");

            Console.WriteLine("--- Datos de Cuenta Corriente ---");
            Console.Write("Fecha de apertura (yyyy-MM-dd, Enter = hoy): ");
            string aperturaStr = Console.ReadLine()?.Trim();
            DateTime fechaApertura = string.IsNullOrEmpty(aperturaStr)
                ? DateTime.Today
                : DateTime.ParseExact(aperturaStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.Write("Límite de crédito: ");
            string limiteStr = Console.ReadLine()?.Trim().Replace(",", ".");
            decimal limiteCredito = string.IsNullOrEmpty(limiteStr)
                ? 0m
                : decimal.Parse(limiteStr, CultureInfo.InvariantCulture);

            Console.Write("Estado del crédito (Activo / Suspendido, Enter = Activo): ");
            string estadoCredito = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(estadoCredito)) estadoCredito = "Activo";

            return new Contacto
            {
                Dni = dni,
                Apellido = apellido,
                Nombres = nombres,
                CuilCuit = cuilCuit,
                FechaAlta = fechaAlta,
                EstadoCivil = estadoCivil,
                Nacionalidad = nacionalidad,
                Calle = calle,
                Depto = depto,
                Piso = piso,
                Barrio = barrio,
                CodigoPostal = codigoPostal,
                Ciudad = ciudad,
                Provincia = provincia,
                Telefono = telefono,
                TelefonoAlternativo = telefonoAlternativo,
                Email = email,
                Instagram = instagram,
                ProfesionOcupacion = profesion,
                EmpresaLugarTrabajo = empresa,
                NivelEstudios = nivelEstudios,
                Estado = estado,
                MetodoPagoPreferido = metodoPago,
                ObservacionesNotas = observaciones,
                CuentaCorriente = new CuentaCte
                {
                    FechaApertura = fechaApertura,
                    LimiteCredito = limiteCredito,
                    EstadoCredito = estadoCredito
                }
            };
        }

        private static string PedirTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine()?.Trim();
        }
    }
}