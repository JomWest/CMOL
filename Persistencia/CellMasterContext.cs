using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Dominio;

namespace Persistencia;

public partial class CellMasterDbContext : DbContext
{
    public CellMasterDbContext()
    {
    }

    public CellMasterDbContext(DbContextOptions<CellMasterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleDevolucion> DetalleDevolucions { get; set; }

    public virtual DbSet<DetalleFacura> DetalleFacuras { get; set; }

    public virtual DbSet<Devolucion> Devolucions { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<TipoCambio> TipoCambios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vcliente> Vclientes { get; set; }

    public virtual DbSet<Vdetalle> Vdetalles { get; set; }

    public virtual DbSet<VFactura> Vfacturas { get; set; }

    public virtual DbSet<VieFactura> VieFacturas { get; set; }

    public virtual DbSet<VistaCliente> VistaClientes { get; set; }

    public virtual DbSet<VistaCompra> VistaCompras { get; set; }

    public virtual DbSet<VistaFactura> VistaFacturas { get; set; }

    public virtual DbSet<VistaInventario> VistaInventarios { get; set; }

    public virtual DbSet<Vtc> Vtcs { get; set; }

    public virtual DbSet<Vusuario> Vusuarios { get; set; }

    public virtual DbSet<Vusuario1> Vusuarios1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategorias)
                .HasName("PK14")
                .IsClustered(false);

            entity.Property(e => e.IdCategorias).HasColumnName("Id_Categorias");
            entity.Property(e => e.Descripción)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes)
                .HasName("PK47")
                .IsClustered(false);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdClientes)
                .ValueGeneratedNever()
                .HasColumnName("Id_Clientes");

            entity.HasOne(d => d.IdClientesNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.IdClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Persona1");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompras)
                .HasName("PK16")
                .IsClustered(false);

            entity.ToTable(tb => tb.HasTrigger("trigger_insert_Compras"));

            entity.Property(e => e.IdCompras).HasColumnName("Id_Compras");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("Fecha_Compra");
            entity.Property(e => e.IdProveedores).HasColumnName("Id_Proveedores");
            entity.Property(e => e.IdUsuarios).HasColumnName("Id_Usuarios");

            entity.HasOne(d => d.IdProveedoresNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedores");

            entity.HasOne(d => d.IdUsuariosNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefUsuarios29");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra)
                .HasName("PK16_2")
                .IsClustered(false);

            entity.ToTable("Detalle Compra", tb => tb.HasTrigger("TR_Stock"));

            entity.Property(e => e.IdDetalleCompra).HasColumnName("Id_DetalleCompra");
            entity.Property(e => e.CantidadProducto).HasColumnName("Cantidad_Producto");
            entity.Property(e => e.IdCompras).HasColumnName("Id_Compras");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Compra");
            entity.Property(e => e.TotalCompra).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdComprasNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompras)
                .HasConstraintName("FK_Detalle Compra_Compras");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("RefProducto79");
        });

        modelBuilder.Entity<DetalleDevolucion>(entity =>
        {
            entity.HasKey(e => e.Iddetalledevolucion).HasName("PK__DetalleD__1CF67D05070859AE");

            entity.ToTable("DetalleDevolucion", tb => tb.HasTrigger("TR_StockDevolucionVentas"));

            entity.Property(e => e.Iddetalledevolucion).HasColumnName("iddetalledevolucion");
            entity.Property(e => e.Cant).HasColumnName("cant");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.Iddevoluciones).HasColumnName("iddevoluciones");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleDevolucions)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_DetalleDevolucion_Producto1");

            entity.HasOne(d => d.IddevolucionesNavigation).WithMany(p => p.DetalleDevolucions)
                .HasForeignKey(d => d.Iddevoluciones)
                .HasConstraintName("FK_DetalleDevolucion_Devolucion1");
        });

        modelBuilder.Entity<DetalleFacura>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactura)
                .HasName("PK16_1")
                .IsClustered(false);

            entity.ToTable("Detalle Facura", tb => tb.HasTrigger("TR_StockVentas"));

            entity.Property(e => e.IdDetalleFactura).HasColumnName("Id_DetalleFactura");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdtipoCambio).HasColumnName("IDTipoCambio");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacuras)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Detalle Facura_Factura");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleFacuras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("RefProducto34");

            entity.HasOne(d => d.IdtipoCambioNavigation).WithMany(p => p.DetalleFacuras)
                .HasForeignKey(d => d.IdtipoCambio)
                .HasConstraintName("FK_Detalle Facura_TipoCambio");
        });

        modelBuilder.Entity<Devolucion>(entity =>
        {
            entity.HasKey(e => e.Iddevoluciones).HasName("PK__Devoluci__5AE9AC5046C69913");

            entity.ToTable("Devolucion");

            entity.Property(e => e.Iddevoluciones).HasColumnName("iddevoluciones");
            entity.Property(e => e.Acciontomadas)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("acciontomadas");
            entity.Property(e => e.Fechadevolucion)
                .HasColumnType("date")
                .HasColumnName("fechadevolucion");
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdUsuarios).HasColumnName("Id_Usuarios");
            entity.Property(e => e.Motivosdevolucion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motivosdevolucion");
            entity.Property(e => e.Totaldevolucion)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("totaldevolucion");

            entity.HasOne(d => d.IdClientesNavigation).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.IdClientes)
                .HasConstraintName("FK_Devolucion_Cliente1");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Devolucion_Factura1");

            entity.HasOne(d => d.IdUsuariosNavigation).WithMany(p => p.Devolucions)
                .HasForeignKey(d => d.IdUsuarios)
                .HasConstraintName("FK_Devolucion_Usuarios1");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura)
                .HasName("PK33_1")
                .IsClustered(false);

            entity.ToTable("Factura", tb => tb.HasTrigger("trigger_insert_Factura"));

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.IdUsuarios).HasColumnName("Id_Usuarios");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClientesNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Cliente");

            entity.HasOne(d => d.IdUsuariosNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuarios)
                .HasConstraintName("RefUsuarios26");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca)
                .HasName("PK10")
                .IsClustered(false);

            entity.ToTable("Marca");

            entity.Property(e => e.IdMarca).HasColumnName("Id_Marca");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Marca");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona)
                .HasName("PK56")
                .IsClustered(false);

            entity.ToTable("Persona");

            entity.HasIndex(e => e.Cedula, "Cedula").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto)
                .HasName("PK18")
                .IsClustered(false);

            entity.ToTable("Producto");

            entity.HasIndex(e => e.CodigoProd, "CodigoProd").IsUnique();

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdCategorias).HasColumnName("Id_Categorias");
            entity.Property(e => e.IdMarca).HasColumnName("Id_Marca");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Venta");

            entity.HasOne(d => d.IdCategoriasNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategorias)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categorias");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Marca");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedores)
                .HasName("PK43")
                .IsClustered(false);

            entity.Property(e => e.IdProveedores)
                .ValueGeneratedNever()
                .HasColumnName("Id_Proveedores");
            entity.Property(e => e.Departamento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Empresa");

            entity.HasOne(d => d.IdProveedoresNavigation).WithOne(p => p.Proveedore)
                .HasForeignKey<Proveedore>(d => d.IdProveedores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedores_Persona1");
        });

        modelBuilder.Entity<TipoCambio>(entity =>
        {
            entity.HasKey(e => e.IdtipoCambio).HasName("PK__TipoCamb__B26D67848B65A96F");

            entity.ToTable("TipoCambio");

            entity.Property(e => e.IdtipoCambio).HasColumnName("IDTipoCambio");
            entity.Property(e => e.FechaC).HasColumnType("date");
            entity.Property(e => e.PrecioCambio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios)
                .HasName("PK46")
                .IsClustered(false);

            entity.Property(e => e.IdUsuarios)
                .ValueGeneratedNever()
                .HasColumnName("Id_Usuarios");
            entity.Property(e => e.Cargo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(15)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdUsuariosNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Persona1");
        });

        modelBuilder.Entity<Vcliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VClientes");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.IdDetalleFactura).HasColumnName("Id_DetalleFactura");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Vdetalle>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VDetalle");

            entity.Property(e => e.IdDetalleFactura).HasColumnName("Id_DetalleFactura");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdtipoCambio).HasColumnName("IDTipoCambio");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCambio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VFactura");

            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoTrabajador)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CargoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CedulaCliente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.Idfactura).HasColumnName("idfactura");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.NombreTrabajador)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCambio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotalFactura).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalFactura).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalFacturas).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VieFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VieFactura");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Venta");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Usuario).HasMaxLength(15);
        });

        modelBuilder.Entity<VistaCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaCliente");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdClientes).HasColumnName("Id_Clientes");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VistaCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaCompra");

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.NombreU)
                .HasMaxLength(62)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Producto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Proveedor)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VistaFactura>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaFactura");

            entity.Property(e => e.CedulaC)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Chambeador)
                .HasMaxLength(62)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Idfactura).HasColumnName("idfactura");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(62)
                .IsUnicode(false)
                .HasColumnName("Nombre_Cliente");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioCambio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalFactura).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VistaInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaInventario");

            entity.Property(e => e.IdCategorias).HasColumnName("Id_Categorias");
            entity.Property(e => e.IdMarca).HasColumnName("Id_Marca");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedores).HasColumnName("Id_Proveedores");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Empresa");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Marca");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Precio_Venta");
        });

        modelBuilder.Entity<Vtc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VTC");

            entity.Property(e => e.FechaC).HasColumnType("date");
            entity.Property(e => e.IdtipoCambio)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDTipoCambio");
            entity.Property(e => e.PrecioCambio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Vusuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUsuario");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cargo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Expr1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.IdDetalleFactura).HasColumnName("Id_DetalleFactura");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Vusuario1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUsuarios");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Cargo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.IdUsuarios).HasColumnName("Id_Usuarios");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Usuario).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
