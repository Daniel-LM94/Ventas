using BL.Tecnologia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tecnologia
{
    public class ProductosBL
    {
        Contexto _contexto;
        public BindingList<Producto> ListaProductos { get; set; }

        public ProductosBL()
        {

            _contexto = new Contexto();
            ListaProductos = new BindingList<Producto>();
        }

        public BindingList<Producto> ObtenerProductos()
        {
            _contexto.Productos.Load();
            ListaProductos =  _contexto.Productos.Local.ToBindingList();
            return ListaProductos;
        }
    
        public void cancelarcambios()
       {
           foreach (var item in _contexto.ChangeTracker.Entries())
           {
               item.State = EntityState.Unchanged;
               item.Reload();
           }
       }

        public Resultado GuardarProducto(Producto Producto)
        {
            var resultado = validar(Producto);
            if( resultado.Correcto == false)
            {
              
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Correcto = true;
            return resultado;

            
        }

    
        public void AgregarProducto()
        {
           var nuevoProducto = new Producto();
           _contexto.Productos.Add(nuevoProducto);

        }

        public bool Eliminar(int id)
        {
            foreach (var Producto in ListaProductos)
            {
                if (Producto.Id == id)
                {
                    ListaProductos.Remove(Producto);
                    _contexto.SaveChanges();
                    return true;
                }

            }
            return false;
        }


        private Resultado validar(Producto Producto)
        {
            var resultado = new Resultado();
            resultado.Correcto = true;

            if(string.IsNullOrEmpty(Producto.Descripcion) == true )
            {
                resultado.Incorrecto = "Ingrese un Producto";
                resultado.Correcto = false;
            }

            if (Producto.Inventario <=0)
            {
                resultado.Incorrecto = "El Producto debe ser mayor a cero";
                resultado.Correcto = false;
            }

            if (Producto.Precio <=0)
            {
                resultado.Incorrecto = "El Producto debe contener un Precio mayor a cero";
                resultado.Correcto = false;
            }


            if (Producto.TipoId == 0)
            {
                resultado.Incorrecto = "Seleccione un Tipo";
                resultado.Correcto = false;
            }

            if (Producto.CategoriaId == 0)
            {
                resultado.Incorrecto = "Seleccione una categoria";
                resultado.Correcto = false;
            }

            return resultado;
        }
    }


    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public double Precio { get; set; }
        public int Inventario { get; set; }
        public byte[] Foto{ get; set; }
        public bool Activo { get; set; }


        public Producto()
        {
            Activo = true;
        }

    }

    

    public class Resultado
    {
      public bool Correcto{ get; set;}
      public string Incorrecto { get; set;}
    }
}
