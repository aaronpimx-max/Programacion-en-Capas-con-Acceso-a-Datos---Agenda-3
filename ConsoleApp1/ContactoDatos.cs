using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Datos
{
    public class ContactoDatos
    {
        private readonly string _connectionString =
            "Server=127.0.0.1;Port=3307;Database=agenda_db;Uid=root;Pwd=;";

        private static object DbOrNull(string val)
        {
            return string.IsNullOrWhiteSpace(val) ? DBNull.Value : (object)val.Trim();
        }

        public void Insertar(Contacto c)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO contactos_v2
                               (Dni, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email,
                                CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal,
                                Barrio, TelefonoAlternativo, Instagram, ProfesionOcupacion,
                                EmpresaLugarTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, ObservacionesNotas)
                               VALUES
                               (@Dni, @Apellido, @Nombres, @Calle, @Depto, @Piso, @Ciudad, @Telefono, @Email,
                                @CuilCuit, @FechaAlta, @EstadoCivil, @Nacionalidad, @Provincia, @CodigoPostal,
                                @Barrio, @TelefonoAlternativo, @Instagram, @ProfesionOcupacion,
                                @EmpresaLugarTrabajo, @NivelEstudios, @Estado, @MetodoPagoPreferido, @ObservacionesNotas);";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Dni", c.Dni);
                    cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                    cmd.Parameters.AddWithValue("@Nombres", c.Nombres);
                    cmd.Parameters.AddWithValue("@Calle", c.Calle);
                    cmd.Parameters.AddWithValue("@Depto", DbOrNull(c.Depto));
                    cmd.Parameters.AddWithValue("@Piso", DbOrNull(c.Piso));
                    cmd.Parameters.AddWithValue("@Ciudad", c.Ciudad);
                    cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@Email", c.Email);
                    cmd.Parameters.AddWithValue("@CuilCuit", DbOrNull(c.CuilCuit));
                    cmd.Parameters.AddWithValue("@FechaAlta", c.FechaAlta.Date);
                    cmd.Parameters.AddWithValue("@EstadoCivil", DbOrNull(c.EstadoCivil));
                    cmd.Parameters.AddWithValue("@Nacionalidad", DbOrNull(c.Nacionalidad));
                    cmd.Parameters.AddWithValue("@Provincia", DbOrNull(c.Provincia));
                    cmd.Parameters.AddWithValue("@CodigoPostal", DbOrNull(c.CodigoPostal));
                    cmd.Parameters.AddWithValue("@Barrio", DbOrNull(c.Barrio));
                    cmd.Parameters.AddWithValue("@TelefonoAlternativo", DbOrNull(c.TelefonoAlternativo));
                    cmd.Parameters.AddWithValue("@Instagram", DbOrNull(c.Instagram));
                    cmd.Parameters.AddWithValue("@ProfesionOcupacion", DbOrNull(c.ProfesionOcupacion));
                    cmd.Parameters.AddWithValue("@EmpresaLugarTrabajo", DbOrNull(c.EmpresaLugarTrabajo));
                    cmd.Parameters.AddWithValue("@NivelEstudios", DbOrNull(c.NivelEstudios));
                    cmd.Parameters.AddWithValue("@Estado", c.Estado);
                    cmd.Parameters.AddWithValue("@MetodoPagoPreferido", DbOrNull(c.MetodoPagoPreferido));
                    cmd.Parameters.AddWithValue("@ObservacionesNotas", DbOrNull(c.ObservacionesNotas));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Modificar(Contacto c)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE contactos_v2
                               SET Apellido = @Apellido,
                                   Nombres = @Nombres,
                                   Calle = @Calle,
                                   Depto = @Depto,
                                   Piso = @Piso,
                                   Ciudad = @Ciudad,
                                   Telefono = @Telefono,
                                   Email = @Email,
                                   CuilCuit = @CuilCuit,
                                   FechaAlta = @FechaAlta,
                                   EstadoCivil = @EstadoCivil,
                                   Nacionalidad = @Nacionalidad,
                                   Provincia = @Provincia,
                                   CodigoPostal = @CodigoPostal,
                                   Barrio = @Barrio,
                                   TelefonoAlternativo = @TelefonoAlternativo,
                                   Instagram = @Instagram,
                                   ProfesionOcupacion = @ProfesionOcupacion,
                                   EmpresaLugarTrabajo = @EmpresaLugarTrabajo,
                                   NivelEstudios = @NivelEstudios,
                                   Estado = @Estado,
                                   MetodoPagoPreferido = @MetodoPagoPreferido,
                                   ObservacionesNotas = @ObservacionesNotas
                               WHERE Dni = @Dni;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Dni", c.Dni);
                    cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                    cmd.Parameters.AddWithValue("@Nombres", c.Nombres);
                    cmd.Parameters.AddWithValue("@Calle", c.Calle);
                    cmd.Parameters.AddWithValue("@Depto", DbOrNull(c.Depto));
                    cmd.Parameters.AddWithValue("@Piso", DbOrNull(c.Piso));
                    cmd.Parameters.AddWithValue("@Ciudad", c.Ciudad);
                    cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@Email", c.Email);
                    cmd.Parameters.AddWithValue("@CuilCuit", DbOrNull(c.CuilCuit));
                    cmd.Parameters.AddWithValue("@FechaAlta", c.FechaAlta.Date);
                    cmd.Parameters.AddWithValue("@EstadoCivil", DbOrNull(c.EstadoCivil));
                    cmd.Parameters.AddWithValue("@Nacionalidad", DbOrNull(c.Nacionalidad));
                    cmd.Parameters.AddWithValue("@Provincia", DbOrNull(c.Provincia));
                    cmd.Parameters.AddWithValue("@CodigoPostal", DbOrNull(c.CodigoPostal));
                    cmd.Parameters.AddWithValue("@Barrio", DbOrNull(c.Barrio));
                    cmd.Parameters.AddWithValue("@TelefonoAlternativo", DbOrNull(c.TelefonoAlternativo));
                    cmd.Parameters.AddWithValue("@Instagram", DbOrNull(c.Instagram));
                    cmd.Parameters.AddWithValue("@ProfesionOcupacion", DbOrNull(c.ProfesionOcupacion));
                    cmd.Parameters.AddWithValue("@EmpresaLugarTrabajo", DbOrNull(c.EmpresaLugarTrabajo));
                    cmd.Parameters.AddWithValue("@NivelEstudios", DbOrNull(c.NivelEstudios));
                    cmd.Parameters.AddWithValue("@Estado", c.Estado);
                    cmd.Parameters.AddWithValue("@MetodoPagoPreferido", DbOrNull(c.MetodoPagoPreferido));
                    cmd.Parameters.AddWithValue("@ObservacionesNotas", DbOrNull(c.ObservacionesNotas));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EliminarPorDni(string dni)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM contactos_v2 WHERE Dni = @Dni;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Dni", dni);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Contacto> Buscar(string criterio, string valor)
        {
            string columna;
            switch (criterio.ToUpper())
            {
                case "DNI":
                    columna = "Dni";
                    break;
                case "APELLIDO":
                    columna = "Apellido";
                    break;
                case "NOMBRES":
                    columna = "Nombres";
                    break;
                case "CALLE":
                    columna = "Calle";
                    break;
                default:
                    throw new ArgumentException("Criterio de búsqueda no válido.");
            }

            var lista = new List<Contacto>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = $@"SELECT Id, Dni, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email,
                                       CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal,
                                       Barrio, TelefonoAlternativo, Instagram, ProfesionOcupacion,
                                       EmpresaLugarTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, ObservacionesNotas
                                FROM contactos_v2
                                WHERE {columna} LIKE @Valor;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Valor", $"%{valor}%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string GetOpt(string col) =>
                                reader.IsDBNull(reader.GetOrdinal(col)) ? "" : reader.GetString(col);

                            lista.Add(new Contacto
                            {
                                Id = reader.GetInt32("Id"),
                                Dni = reader.GetString("Dni"),
                                Apellido = reader.GetString("Apellido"),
                                Nombres = reader.GetString("Nombres"),
                                Calle = reader.GetString("Calle"),
                                Depto = GetOpt("Depto"),
                                Piso = GetOpt("Piso"),
                                Ciudad = reader.GetString("Ciudad"),
                                Telefono = reader.GetString("Telefono"),
                                Email = reader.GetString("Email"),
                                CuilCuit = GetOpt("CuilCuit"),
                                FechaAlta = reader.GetDateTime("FechaAlta"),
                                EstadoCivil = GetOpt("EstadoCivil"),
                                Nacionalidad = GetOpt("Nacionalidad"),
                                Provincia = GetOpt("Provincia"),
                                CodigoPostal = GetOpt("CodigoPostal"),
                                Barrio = GetOpt("Barrio"),
                                TelefonoAlternativo = GetOpt("TelefonoAlternativo"),
                                Instagram = GetOpt("Instagram"),
                                ProfesionOcupacion = GetOpt("ProfesionOcupacion"),
                                EmpresaLugarTrabajo = GetOpt("EmpresaLugarTrabajo"),
                                NivelEstudios = GetOpt("NivelEstudios"),
                                Estado = reader.GetString("Estado"),
                                MetodoPagoPreferido = GetOpt("MetodoPagoPreferido"),
                                ObservacionesNotas = GetOpt("ObservacionesNotas")
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}