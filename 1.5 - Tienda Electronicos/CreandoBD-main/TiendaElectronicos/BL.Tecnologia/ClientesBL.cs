 using BL.Tecnologia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tecnologia
{
    public class ClientesBL
    {

        Contexto _contexto;

        public BindingList<Cliente> ListaClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();
            ListaClientes = new BindingList<Cliente>();
        }

        public BindingList<Cliente> ObtenerClientes()
        {
            _contexto.Clientes.Load(); // Error al Cargar [ public DbSet<Cliente> Cliente { get; set; } ] desde la Base de Datos | Solucion Se cambio el nombre [Cliente] a [Clientes] en la Tabla DbSet en Contexto2
            ListaClientes = _contexto.Clientes.Local.ToBindingList();

            return ListaClientes;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Respuesta GuardarCliente(Cliente cliente)
        {
            var respuesta = Validar(cliente);
            if(respuesta.Exitoso == false)
            {
                return respuesta;
            }

            /* if(cliente.Id == 0) // Solo se usa con datos de prueba
             {
                 cliente.Id = ListaClientes.Max(item => item.Id) + 1;
             }*/

             _contexto.SaveChanges();
             respuesta.Exitoso = true;
             return respuesta;
         }

         public void AgregarCliente()
         {
             var nuevocliente = new Cliente();
            _contexto.Clientes.Add(nuevocliente);
        }

         public bool EliminarCliente(int Id)
        {

            foreach (var cliente in ListaClientes.ToList())
            {
                if(cliente.Id == Id )
                {
                    ListaClientes.Remove(cliente);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

         private Respuesta Validar(Cliente cliente)
         {
             var respuesta = new Respuesta();
             respuesta.Exitoso = true;

             if (cliente == null)  // Agregar esta validacion cuando ya este resulto el problema del Formulario Clientes
             {
                 respuesta.Mensaje = "agregue un cliente valido";
                 respuesta.Exitoso = false;            
             }

            if (string.IsNullOrEmpty(cliente.Name)== true)
            {
                respuesta.Mensaje = "Ingrese un Nombre";
                respuesta.Exitoso = false;
            }

            if (string.IsNullOrEmpty(cliente.LastName)== true)
            {
                respuesta.Mensaje = "Ingrese un Apellido";
                respuesta.Exitoso = false;
            }

            if (string.IsNullOrEmpty(cliente.Add)== true)
            {
                respuesta.Mensaje = "Ingrese una Direccion";
                respuesta.Exitoso = false;
            }

            return respuesta;
        }       
    }
    
    public class Cliente
    {
        public int Id { get; set; } // IdCustumer, este nombre no es valido para un Id
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Add { get; set; }
        public double Telephone{ get; set; }
        public bool Activo { get; set; }

        public Cliente()
        {
            Activo = true;
        }

    }

    public class Respuesta // 3er clase usada para validaciones
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }
}
