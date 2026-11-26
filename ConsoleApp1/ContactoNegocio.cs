using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class ContactoNegocio
    {
        private readonly ContactoDatos _datos = new ContactoDatos();

        public void Agregar(Contacto c)
        {
            PrepararYValidar(c);
            _datos.Insertar(c);
        }

        public void Modificar(Contacto c)
        {
            PrepararYValidar(c);
            bool actualizado = _datos.Modificar(c);
            if (!actualizado)
                throw new KeyNotFoundException("No se encontró ningún contacto con ese DNI para modificar.");
        }

        public void Eliminar(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("Debe ingresar un DNI.");

            bool eliminado = _datos.EliminarPorDni(dni.Trim());
            if (!eliminado)
                throw new KeyNotFoundException("No se encontró ningún contacto con ese DNI para eliminar.");
        }

        public List<Contacto> Buscar(string criterio, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("Debe ingresar un valor de búsqueda.");

            return _datos.Buscar(criterio, valor.Trim());
        }

        private void PrepararYValidar(Contacto c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c));

            if (string.IsNullOrWhiteSpace(c.Dni) ||
                string.IsNullOrWhiteSpace(c.Apellido) ||
                string.IsNullOrWhiteSpace(c.Nombres) ||
                string.IsNullOrWhiteSpace(c.Calle) ||
                string.IsNullOrWhiteSpace(c.Ciudad) ||
                string.IsNullOrWhiteSpace(c.Telefono) ||
                string.IsNullOrWhiteSpace(c.Email))
            {
                throw new ArgumentException("DNI, Apellido, Nombres, Calle, Ciudad, Teléfono y Email son obligatorios.");
            }

            if (!c.Email.Contains("@"))
                throw new ArgumentException("El email no tiene un formato válido.");

            c.FechaAlta = c.FechaAlta == default ? DateTime.Today : c.FechaAlta.Date;

            string estado = string.IsNullOrWhiteSpace(c.Estado) ? "ACTIVO" : c.Estado.Trim().ToUpper();
            if (estado != "ACTIVO" && estado != "INACTIVO")
                throw new ArgumentException("El estado debe ser ACTIVO o INACTIVO.");
            c.Estado = estado;

            if (!string.IsNullOrWhiteSpace(c.Instagram))
            {
                string ig = c.Instagram.Trim();
                c.Instagram = ig.StartsWith("@") ? ig : "@" + ig;
            }

            if (c.CuentaCorriente == null)
                c.CuentaCorriente = new CuentaCte();

            if (c.CuentaCorriente.LimiteCredito < 0)
                throw new ArgumentException("El límite de crédito no puede ser negativo.");

            c.CuentaCorriente.FechaApertura = c.CuentaCorriente.FechaApertura == default
                ? DateTime.Today
                : c.CuentaCorriente.FechaApertura.Date;

            string estadoCta = string.IsNullOrWhiteSpace(c.CuentaCorriente.EstadoCredito)
                ? "Activo"
                : c.CuentaCorriente.EstadoCredito.Trim();

            if (string.Equals(estadoCta, "activo", StringComparison.OrdinalIgnoreCase))
                estadoCta = "Activo";
            else if (string.Equals(estadoCta, "suspendido", StringComparison.OrdinalIgnoreCase))
                estadoCta = "Suspendido";
            else
                throw new ArgumentException("El estado del crédito debe ser Activo o Suspendido.");

            c.CuentaCorriente.EstadoCredito = estadoCta;
        }
    }
}