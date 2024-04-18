window.onload = function () {
    ComprobarCambios();
    actualizarTablaClientes();
    actualizarTablaProductos();
    desactivarFac();

}
//#region 
//#endregion
////////////////////Captuara Datos Modal/////////////////////////////////
const NombreClienteModal = document.getElementById("nombreClienteModal");
const ApellidoInputModal = document.getElementById("apellidoClienteModal");
const CedulaInputModal = document.getElementById("cedulaModal");
const telefonoinputModal = document.getElementById("telefonoModal");
let IdClientes;
//////////////////////////////////////////////////////////////////////////

////////////////////Captuara Datos Formulario/////////////////////////////////
const fechaHoy = document.getElementById("fechaFactura");
const CambioDiaFact = document.getElementById("cambioDiaFact");
const Numerofactura = document.getElementById("numeroFactura");
const NombreProductoImput = document.getElementById("NameProductFact");
const CategoriaFactura = document.getElementById("categoriafact");
const marcafact = document.getElementById("marcafact");
const CodProducfact = document.getElementById("codigoProductofact");
const cantidaprod = document.getElementById("cantidadProdcutoFact");
const StockProductFact = document.getElementById("stockProductoFact");
const precioProductC = document.getElementById("precioCosrdobaFact");
const ClienteImnpuFact = document.getElementById("nombreClienteFact");
const precioProductD = document.getElementById("precioDolarFact");
const CedualFactInput = document.getElementById("cedulaFact");
const RelefonoFactInput = document.getElementById("telefonoFact");
const txtTotalD = document.getElementById("totalDolarfact");
const txtTotalC = document.getElementById("totalCordobasFact");
const Vendedortxt = document.getElementById("vendedor");
let IdTipoCambio;
let cantidadProductosFact;
let precioCambioF;
let ultimoIdFactura;
let Idusuario;
let Idvendedor = 1;
let IdProductos;
let TotalC = 0;
let TotalD = 0;
ObtenerUltimoIdFactura();
//////////////////////////////////////////////////////////////////////////

/////////////////////////////Botones Modal////////////////////////////////
const BtnGuardarCliente = document.getElementById("BtnSaveliente");
const btnActualoizarCliente = document.getElementById("BtnACtualizarCliente");
const BtnClienteF = document.getElementById("clientesFactBtn");
const BtnProductos = document.getElementById("agregarProduct");

/////////////////////////////////////////////////////////////////////////

/////////////////////Botones Productos//////////////////////////////////
const BtnGuardar = document.getElementById("guardarProduct");
const BtnGuardarfactura = document.getElementById("guardarFactura");
const BtnActualizarPF = document.getElementById("actualizar");
const BtnNuevasFact = document.getElementById("nuevaVentaBtn");

///////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////
//#region Eventos de los botones
BtnGuardarCliente.addEventListener("click", function () {
    CrearCliente();
});

btnActualoizarCliente.addEventListener("click", function () {
    ActualizarClientes();
});

BtnGuardar.addEventListener("click", function () {
    if (cantidaprod.value == "" ) {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(cantidaprod.value))) {} else {
        alert("Ingrese un numero en la cantidad")
        return;
    }
    if (parseInt(cantidaprod.value) >= parseInt(StockProductFact.value) ){
       alert("La cantidad no puede superar el stock del producto")
       return;
    }
    agregarProductos();
    Limpiarproducto();
    DesactivarDatosfact();
    BtnGuardarfactura.disabled = false;
});

BtnGuardarfactura.addEventListener("click", function () {

    CrearFactura();
    ObtenerUltimoIdFactura();
    CapturarTabla();
    LimpiarTabla();
    LimpiarFact();
    ObtenerUltimoIdFactura();
    ObtenerCambioDia();
    DesactivarDatosfact();
    desactivarFac();
    abrirFact();
    

});

function abrirFact(){
    let url = 'http://localhost:5052/FacturaIDController/' + ultimoIdFactura;
    window.open(url, '_blank');
};

BtnNuevasFact.addEventListener("click", function(){
BtnClienteF.disabled = false;
BtnProductos.disabled = false;
cantidaprod.disabled = false;
});

BtnActualizarPF.addEventListener("click", function(){
    if (cantidaprod.value == "" ) {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(cantidaprod.value))) {} else {
        alert("Ingrese un numero en la cantidad")
        return;
    }
    if (parseInt(cantidaprod.value) >= parseInt(StockProductFact.value) ){
        alert("La cantidad no puede superar el stock del producto")
        return;
     }
actualizarProductosfact();
Limpiarproducto();
BtnGuardar.disabled = false;
BtnActualizarPF.disabled = true;
});
//#endregion
////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////
//#region Validar si existe el cambio para el dia
function ComprobarCambios() {
    fetch('/TipoCambio/ExisteTipoCambioParaFechaActual')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al comprobar el tipo de cambio para la fecha actual.');
            }
            return response.json();
        })
        .then(data => {
            if (data.existeTipoCambio) {
               
                ;
            } else {
                window.location.href = '/CambioDia.html';
            }
        })
        .catch(error => {
            // Manejo de errores si es necesario
            console.error(error.message);

            // Muestra un mensaje de error al usuario
            alert('Hubo un error al comprobar el tipo de cambio. Por favor, inténtalo nuevamente más tarde.');
        });
}
//#endregion
////////////////////////////////////////////////////

///////////////////////////////////////////////////
//#region  Validaciones

function desactivarFac(){
     fechaHoy.disabled = true;
 CambioDiaFact.disabled = true;
 Numerofactura.disabled = true;
 NombreProductoImput.disabled = true;
 CategoriaFactura.disabled = true;
 marcafact.disabled = true;
 CodProducfact.disabled = true;
 cantidaprod.disabled = true;
 StockProductFact.disabled = true;
 precioProductC.disabled = true;
 ClienteImnpuFact.disabled = true;
 precioProductD.disabled = true;
 CedualFactInput.disabled = true;
 RelefonoFactInput.disabled = true;
 txtTotalD.disabled = true;
 txtTotalC.disabled = true;
 BtnProductos.disabled = true;
BtnClienteF.disabled = true;
Vendedortxt.disabled = true;
BtnGuardarfactura.disabled = true;
BtnActualizarPF.disabled = true;
};

function Activarfact(){
    fechaHoy.disabled = false;
CambioDiaFact.disabled = false;
Numerofactura.disabled = false;
NombreProductoImput.disabled = false;
CategoriaFactura.disabled = false;
marcafact.disabled = false;
CodProducfact.disabled = false;
cantidaprod.disabled = false;
StockProductFact.disabled = false;
precioProductC.disabled = false;
ClienteImnpuFact.disabled = false;
precioProductD.disabled = false;
CedualFactInput.disabled = false;
RelefonoFactInput.disabled = false;
txtTotalD.disabled = false;
txtTotalC.disabled = false;
 BtnGuardar.disabled = false;
 BtnGuardarfactura.disabled = false;
 BtnProductos.disabled = false;
 BtnClienteF.disabled = false;
};

function LimpiarFact(){
    fechaHoy.value = "";
    CambioDiaFact.value = "";
    Numerofactura.value = "";
    NombreProductoImput.value = "";
    CategoriaFactura.value = "";
    marcafact.value = "";
    CodProducfact.value = "";
    cantidaprod.value = "";
    StockProductFact.value = "";
    precioProductC.value = "";
    ClienteImnpuFact.value = "";
    precioProductD.value = "";
    CedualFactInput.value = "";
    RelefonoFactInput.value = "";
    txtTotalD.value = "";
    txtTotalC.value = "";
};

function DesactivarDatosfact(){
    fechaHoy.disabled = true;
    CambioDiaFact.disabled = true;
    Numerofactura.disabled = true;
    ClienteImnpuFact.disabled = true;
    CedualFactInput.disabled = true;
 RelefonoFactInput.disabled = true;
 BtnClienteF.disabled = false;
};

function Limpiarproducto(){
    NombreProductoImput.value = "";
    CategoriaFactura.value = "";
    marcafact.value = "";
    CodProducfact.value = "";
    cantidaprod.value = "";
    StockProductFact.value = "";
    precioProductC.value = "";
    precioProductD.value = "";
};
    var salirBtn = document.getElementById('salir');
    salirBtn.addEventListener('click', function () {
        window.location.replace('Inicio.html');
    });
//#endregion
///////////////////////////////////////////////////

///////////////////////////////////////////////////
//#region Metodo Actualizar
function ActualizarClientes() {

    let NombreClienteModal1 = document.getElementById("nombreClienteModal").value;
    let ApellidoInputModal1 = document.getElementById("apellidoClienteModal").value;
    let CedulaInputModal1 = document.getElementById("cedulaModal").value;
    let telefonoinputModal1 = document.getElementById("telefonoModal").value;

    const NuevoCliente = {

        cedula: CedulaInputModal1,
        nombre: NombreClienteModal1,
        apellido: ApellidoInputModal1,
        telefono: telefonoinputModal1,
    };
    fetch(`/ClientesController/Update/${IdClientes}/`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoCliente)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            if (data.success === true) {
                alert('producto actualizado exitosamente');
                actualizarTablaClientes();
            } else {
                alert('Error al actualizar el producto');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            // Muestra detalles del error en la consola
        });
}

function actualizarProductosfact() {
    const nombreProducto = document.getElementById("NameProductFact").value;
    const codigoProducto = document.getElementById("codigoProductofact").value;
    const categoriaFactura = document.getElementById("categoriafact").value;
    const idCategoria = IdCategoria.value;
    const marcaFact = document.getElementById("marcafact").value;
    const idMarca = IdMarca.value;
    const cantidadProd = parseFloat(document.getElementById("cantidadProdcutoFact").value);
    const precioProductD = parseFloat(document.getElementById("precioDolarFact").value);
    const precioProductC = parseFloat(document.getElementById("precioCosrdobaFact").value);
    const stockProducto = document.getElementById("stockProductoFact").value;
    const filas = document.querySelectorAll('.tbodyYe tr');
    let filaOriginal;
    filas.forEach((fila) => {
        if (fila.textContent.includes(IdProductos)) {
            filaOriginal = fila;
        }
    });

    if (filaOriginal) {


        filaOriginal.innerHTML = `<td>${IdProductos}</td>
                                    <td>${nombreProducto}</td>
                                    <td>${codigoProducto}</td>
                                    <td>${categoriaFactura}</td>
                                    <td hidden>${idCategoria}</td>
                                    <td>${marcaFact}</td>
                                    <td hidden>${idMarca}</td>
                                    <td>${cantidadProd}</td>
                                    <td>${precioProductD}</td>
                                    <td>${precioProductC}</td>
                                    <td hidden>${stockProducto}</td>
                                    <td><button class="btn-editar">Editar</button></td>
                                    <td><button class="btn-delete">Eliminar</button></td>`;
                                    
                                    TotalC += parseFloat((precioProductC * cantidadProd).toFixed(2));
                                    TotalD += parseFloat((precioProductD * cantidadProd).toFixed(2));
                                    txtTotalC.value = TotalC;
                                    txtTotalD.value = TotalD;
    } else {
        console.error('No se encontró la fila original.');
    }
}
//#endregion
///////////////////////////////////////////////////

//////////////////////////////////////////////////
//#region Carear 
function CrearCliente() {
    let NombreClienteModal1 = document.getElementById("nombreClienteModal").value;
    let ApellidoInputModal1 = document.getElementById("apellidoClienteModal").value;
    let CedulaInputModal1 = document.getElementById("cedulaModal").value;
    let telefonoinputModal1 = document.getElementById("telefonoModal").value;

    const NuevoCliente = {

        cedula: CedulaInputModal1,
        nombre: NombreClienteModal1,
        apellido: ApellidoInputModal1,
        telefono: telefonoinputModal1,
    };
    fetch('/ClientesController/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoCliente)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Cliente registrado exitosamente');
                actualizarTablaClientes();
            } else {
                alert('Error al registrar Cliente:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));
}

////////////////////Crear Factura//////////////////////////
function CrearFactura() {
    let fechaHoy = document.getElementById("fechaFactura").value;
    let totalFactura = document.getElementById("totalDolarfact").value;
    let idUsuariosFactura = Idvendedor;
    let idClientesFactura = IdClientes;
    let estadoFactura = "Activa";

    const nuevaFactura = {
        fecha: fechaHoy,
        total: totalFactura,
        devolucions: estadoFactura,
        idUsuarios: idUsuariosFactura,
        idClientes: idClientesFactura,
    };
    console.log(fechaHoy, totalFactura, estadoFactura, idUsuariosFactura, idClientesFactura);
    fetch('/ControladorFactura/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevaFactura)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.success === true) {
                alert('Factura registrada exitosamente');
            } else {
                alert('Error al registrar Factura: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error.message);
            alert('Error al comunicarse con el servidor');
        });
}

function CrearDetalleFactura() {
    let totalFactura = document.getElementById("totalDolarfact").value;

    const nuevoDetalleFactura = {
        cantidad: cantidadProductosFact,
        subtotal: totalFactura,
        total:totalFactura,
        idFactura: ultimoIdFactura,
        idProducto: IdProductos,
        idtipoCambio: IdTipoCambio,
       
        precioUnitario: txtTotalD.value,
    };
    console.log("Detalles", IdProductos, ultimoIdFactura, cantidadProductosFact, txtTotalD.value);

    fetch('/DetalleFactura/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoDetalleFactura)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success === true) {
                alert('Detalle de factura registrado exitosamente');
            } else {
                alert('Error al registrar Detalle de factura: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Ocurrió un error al intentar crear el detalle de factura');
        });
}
///////////////////////////////////////////////////////////
//#endregion
/////////////////////////////////////////////////

/////////////////////////////////////////////////
//#region Funciones generales
function obeterfecha() {
    let fechaActual = new Date().toISOString().split('T')[0];
    fechaHoy.value = fechaActual;
};
obeterfecha();
ObtenerCambioDia();


//////////////////////////////////////////////////////////////////
///////////Obtener Datos necesarios de otras entidades////////////
//////////////////////////////////////////////////////////////////

function ObtenerCambioDia() {
    fetch('/TipoCambio/ObtenerTipoCambioHoy')
        .then(response => {
            if (!response.ok) {
                throw new Error('No se pudo obtener el tipo de cambio para la fecha de hoy.');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            if (data && typeof data.idtipoCambio === 'number' && typeof data.precioCambio === 'number') {
                IdTipoCambio = data.idtipoCambio;
                CambioDiaFact.value = data.precioCambio;
            } else {
                console.log('No se encontró un tipo de cambio válido para la fecha de hoy.');
            }
        })
        .catch(error => {
            console.error('Error al obtener el tipo de cambio:', error.message);
        });
}

function ObtenerUltimoIdFactura() {
    fetch('/ControladorFactura/UltimoIdIngresado')
        .then(response => {
            if (!response.ok) {
                throw new Error('No se pudo obtener el último ID de factura ingresado.');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            if (data && typeof data.ultimoId === 'number') {
                ultimoIdFactura = data.ultimoId;

                // Actualiza el valor del input con id "Numerofactura"
                document.getElementById("numeroFactura").value = ultimoIdFactura + 1;
            } else {
                console.log('No se encontró un último ID de factura válido.');
            }
        })
        .catch(error => {
            console.error('Error al obtener el último ID de factura:', error.message);
        });
}

function agregarProductos() {
    let nombreProductoInput = document.getElementById("NameProductFact");
    let categoriaFactura = document.getElementById("categoriafact");
    let marcaFact = document.getElementById("marcafact");
    let codProducFact = document.getElementById("codigoProductofact");
    let cantidadProd = document.getElementById("cantidadProdcutoFact");
    let precioProductC = document.getElementById("precioCosrdobaFact");
    let precioProductD = document.getElementById("precioDolarFact");
    let StockProductl = document.getElementById("stockProductoFact");

    const tableBody = document.querySelector('.tbodyYe');
    const row = document.createElement('tr');

    row.innerHTML = `<td>${IdProductos}</td>
                        <td>${nombreProductoInput.value}</td>
                        <td>${codProducFact.value}</td>
                        <td>${categoriaFactura.value}</td>
                        <td hidden>${IdCategoria.value}</td>
                        <td>${marcaFact.value}</td>
                        <td hidden>${IdMarca.value}</td>
                        <td>${cantidadProd.value}</td>
                        <td>${precioProductD.value}</td>
                        <td>${precioProductC.value}</td>
                        <td hidden>${StockProductl.value}</td>
                        <td><button class="btn-editar">Editar</button></td>
                        <td><button class="btn-delete">Eliminar</button></td>`;

    tableBody.appendChild(row);
    row.querySelector('.btn-delete').addEventListener('click', function() {
        eliminarFila(this);
    });
    TotalC += parseFloat((precioProductC.value * cantidadProd.value).toFixed(2));
    TotalD += parseFloat((precioProductD.value * cantidadProd.value).toFixed(2));
    txtTotalC.value = TotalC;
    txtTotalD.value = TotalD;
}

function CapturarTabla() {
    var Tabla = document.getElementById("Table");
    var filas = Tabla.getElementsByTagName("tr");

    for (var i = 1; i < filas.length; i++) {
        var fila = filas[i];

        
        IdProductos = parseInt(fila.cells[0].textContent);
        IdCategoria = parseInt(fila.cells[4].textContent);
        cantidadProductosFact = parseInt(fila.cells[7].textContent); 
        IdMarca = parseInt(fila.cells[6].textContent);
        CrearDetalleFactura();


        console.log(IdProductos, cantidadProductosFact, IdCategoria, cantidadProductosFact, IdMarca);
    }

}

document.addEventListener('click', function(e) {
    if (e.target.classList.contains('btn-editar')) {
        // Obtener la fila actual
        const fila = e.target.closest('tr');

        // Asegurarte de que la fila no sea nula antes de continuar
        if (fila) {
            // Obtener los datos de la fila
            const idProducto = fila.querySelector('td:nth-child(1)').textContent.trim();
            const nombreProducto = fila.querySelector('td:nth-child(2)').textContent.trim();
            const codigoProducto = fila.querySelector('td:nth-child(3)').textContent.trim();
            const categoriaFactura = fila.querySelector('td:nth-child(4)').textContent.trim();
            const idCategoria = fila.querySelector('td:nth-child(5)').textContent.trim();
            const marcaFact = fila.querySelector('td:nth-child(6)').textContent.trim();
            const idMarca = fila.querySelector('td:nth-child(7)').textContent.trim();
            const cantidadProd = parseFloat(fila.querySelector('td:nth-child(8)').textContent.trim());
            const precioProductD = parseFloat(fila.querySelector('td:nth-child(9)').textContent.trim());
            const precioProductC = parseFloat(fila.querySelector('td:nth-child(10)').textContent.trim());
            
            const stockProducto = fila.querySelector('td:nth-child(11)').textContent.trim();

            // Rellenar el formulario con los datos de la fila
            IdProductos = idProducto;
            document.getElementById("NameProductFact").value = nombreProducto;
            document.getElementById("codigoProductofact").value = codigoProducto;
            document.getElementById("categoriafact").value = categoriaFactura;
            IdCategoria = idCategoria;
            document.getElementById("marcafact").value = marcaFact;
            IdMarca = idMarca;
            document.getElementById("cantidadProdcutoFact").value = cantidadProd;
            document.getElementById("precioDolarFact").value = precioProductD;
            document.getElementById("precioCosrdobaFact").value = precioProductC;
            document.getElementById("stockProductoFact").value = stockProducto;
            if (!isNaN(precioProductC) && !isNaN(cantidadProd) && !isNaN(precioProductD)) {
                TotalC -= parseFloat((precioProductC * cantidadProd).toFixed(2));
                TotalD -= parseFloat((precioProductD * cantidadProd).toFixed(2));
                txtTotalC.value = TotalC;
                txtTotalD.value = TotalD;
            } else {
                console.error("Uno o más valores no son numéricos.");
            }
            BtnActualizarPF.disabled = false;
            BtnGuardar.disabled = true;
        }
    }
});
function eliminarFila(button) {
    const row = button.closest('tr');

    const precioProductC = parseFloat(row.querySelector('td:nth-child(10)').textContent);
    const cantidadProd = parseFloat(row.querySelector('td:nth-child(8)').textContent);

    TotalC -= parseFloat((precioProductC * cantidadProd).toFixed(2));
    TotalD -= parseFloat((precioProductD.value * cantidadProd).toFixed(2));

    txtTotalC.value = TotalC.toFixed(2);
    txtTotalD.value = TotalD.toFixed(2);

    row.remove();
}
function LimpiarTabla(){
    const tableBody = document.querySelector('.tbodyYe');

    while (tableBody.firstChild) {
        tableBody.removeChild(tableBody.firstChild);
    }

    // También puedes reiniciar las variables de total si es necesario
    TotalC = 0;
    TotalD = 0;
    txtTotalC.value = TotalC;
    txtTotalD.value = TotalD;
}
  
ObtenerUltimoIdFactura();
//#endregion
/////////////////////////////////////////////////

////////////////////////////////////////////////
//#region  ActualizarTablas
function actualizarTablaClientes() {
    fetch('/ClientesController/GetAll')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody90');
            tableBody.innerHTML = '';

            data.forEach(Clientes => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Clientes.idClientesNavigation.nombre}</td>
                          <td>${Clientes.idClientesNavigation.apellido}</td>
                          <td>${Clientes.idClientesNavigation.telefono}</td>
                          <td>${Clientes.idClientesNavigation.cedula}</td>
                          <td>${'<button class="btn-editar">Editar</button>'}</td>
                          <td>${'<button class="btn-eliminar">Eliminar</button>'}</td>
                          <td>${'<button class="btn-Select10" data-dismiss="modal">Selecionar</button>'}</td>`;

                tableBody.appendChild(row);
                const editarBtn = row.querySelector('.btn-editar');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        NombreClienteModal.value = Clientes.idClientesNavigation.nombre;
                        ApellidoInputModal.value = Clientes.idClientesNavigation.apellido;
                        CedulaInputModal.value = Clientes.idClientesNavigation.cedula;
                        telefonoinputModal.value = Clientes.idClientesNavigation.telefono;

                        IdClientes = parseInt(Clientes.idClientesNavigation.idPersona);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
                const SelectCliente = row.querySelector('.btn-Select10');
                if (SelectCliente) {
                    SelectCliente.addEventListener('click', () => {

                        ClienteImnpuFact.value = Clientes.idClientesNavigation.nombre,
                            CedualFactInput.value = Clientes.idClientesNavigation.cedula,
                            RelefonoFactInput.value = Clientes.idClientesNavigation.telefono,
                            IdClientes = parseInt(Clientes.idClientesNavigation.idPersona);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        });
}

function actualizarTablaProductos() {
    fetch('/Productos/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody99');
            tableBody.innerHTML = '';

            data.forEach(Productos => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Productos.idProducto}</td>
                          <td>${Productos.nombreProducto}</td>
                          <td>${Productos.codigoProd}</td>
                          <td>${Productos.categoria.nombre}</td>
                          <td>${Productos.marca.nombreMarca}</td>
                          <td>${Productos.stock}</td>
                          <td>${Productos.stockMinimo}</td>
                          <td>${Productos.precioVenta}</td>
                          <td>${'<button class="btn-Select101" data-dismiss="modal">Selecionar</button>'}</td>`;

                tableBody.appendChild(row);
                const SelectPreoduc = row.querySelector('.btn-Select101');
                if (SelectPreoduc) {
                    SelectPreoduc.addEventListener('click', () => {
                        NombreProductoImput.value = Productos.nombreProducto;
                        CategoriaFactura.value = Productos.categoria.nombre;
                        marcafact.value = Productos.marca.nombreMarca;
                        CodProducfact.value = Productos.codigoProd;
                        StockProductFact.value = Productos.stock;
                        precioProductD.value = parseFloat(Productos.precioVenta).toFixed(2);
                        precioProductC.value = parseFloat(precioProductD.value * CambioDiaFact.value).toFixed(2);
                        IdProductos = parseInt(Productos.idProducto);
                        IdCategoria = parseInt(Productos.categoria.idCategorias);
                        IdMarca = parseInt(Productos.marca.idMarca);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        });
}
//#endregion 