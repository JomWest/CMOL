///////////////////////////////////////////////////////////////////
//#region Declarar Variables

////////////*Captura de datos proveedores*/////////////////
const FechaDelDia = document.getElementById("fechaCompras");
const NoCompra = document.getElementById("NomeroCompra");
const NombreProveedorCompra = document.getElementById("NombreProveedorFact");
const RucProveedorCompra = document.getElementById("RUCProveedor");
const EmpresaCompra = document.getElementById("EmpresaProveedor");
let IdProveedorS;
let IdUsuario1 = 1;
/////////////////////////////////////////////////////////

////////////*Captura de datos Productos*/////////////////
const NamePrductoC = document.getElementById("NombreProductosC");
const codigoProdC = document.getElementById("codigoProductoC");
const PrecioVC = document.getElementById("PrecioVentaC");
const PrecioAC = document.getElementById("PrecioP");
const CantidadPRoductoC1 = document.getElementById("cantidadC");
const TxtTotalCompra1 = document.getElementById("totalDolarc");
const GuardarProductos = document.getElementById("guardar");
const GuardarCompra = document.getElementById("guardarCompra");
const AGGPro = document.getElementById("guardar");
const AggProveedor = document.getElementById("agregarProveedor");
const BtnNewSales = document.getElementById("nuevaVentaBtnC");
const btnUpdateProducto = document.getElementById("actualizar");
let CantidadPRoductoC4;
let IdProductosC;
let IdMarcaC;
let IdCategoriaC;
let totalDC = 0;
let IdProductosCEnEdicion = null;
////////////////////////////////////////////////////////
//#endregion 
//////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////
//#region  Eventos de los botones 
GuardarProductos.addEventListener("click", function(){
    if (CantidadPRoductoC1.value == "" || PrecioAC.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(CantidadPRoductoC1.value))) {} else {
        alert("Ingrese un numero en la cantidad")
        return;
    }
    if (Number.isInteger(parseInt(PrecioAC.value))) {} else {
        alert("Ingrese un numero en la precio")
        return;
    }
    if(parseFloat(PrecioAC.value) >= parseFloat(PrecioVC.value)){
        alert("No puede superar el precio de venta");
        return;
    }
agregarProductosC();
Limpiarproducto();
GuardarCompra.disabled = false;

});

GuardarCompra.addEventListener("click", function(){
CrearCompra();
CapturarTablaC();
desactivarCP();
limpiarTabla2();
LimpiarTodo();
ObtenerUltimoIdCompra();
});

BtnNewSales.addEventListener("click", function(){
activarCP();
btnUpdateProducto.disabled = true;
});

btnUpdateProducto.addEventListener("click", function(){
    if (CantidadPRoductoC1.value == "" || PrecioAC.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(CantidadPRoductoC1.value))) {} else {
        alert("Ingrese un numero en la cantidad")
        return;
    }
    if (Number.isInteger(parseInt(PrecioAC.value))) {} else {
        alert("Ingrese un numero en la precio")
        return;
    }
    if(parseFloat(PrecioAC.value) >= parseFloat(PrecioVC.value)){
        alert("No puede superar el precio de venta");
        return;
    }
actualizarProductosC();
});
//#endregion
/////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////
//#region Crear Compra
function CrearCompra() {
    let fechaHoy = document.getElementById("fechaCompras").value;

    const NuevaCompra = {
        fecha: fechaHoy,
        idUsuarios: IdUsuario1,
        idProveedores : IdProveedorS,
    };
    fetch('/ComprasController/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevaCompra)
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
        });
}

function CrearDetalleCompra() {
    let TotalCompra2 = document.getElementById("totalDolarc").value;

    const nuevoDetalleCompra = {
        precioCompra: TotalCompra2,
        cantidadProducto: CantidadPRoductoC4,
        totalCompra:TotalCompra2,
        idCompras: ultimoIdCompra,
        idProducto: IdProductosC,
    };

    fetch('/ComprasDetalle/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoDetalleCompra)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success === true) {
                alert('Detalle de Compra registrado exitosamente');
            } else {
                alert('Error al registrar Detalle de Compra: ' + data.message);
            }
        });
}
//#endregion
/////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////
//#region CargaForm
window.onload = function () {
CargarProveedores();
actualizarTablaProductosC();
obeterfechC();
ObtenerUltimoIdCompra();
desactivarCP();
btnUpdateProducto.disabled = true;

}
//#endregion
/////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////
//#region  Funciones Generales

////////////////////Cargar Tabla Proveedores////////////////////
function CargarProveedores() {
    fetch('/Proveedor/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody66');
            tableBody.innerHTML = '';

            data.forEach(Proveedore => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Proveedore.idproveedor}</td>
                          <td>${Proveedore.idProveedoresNavigation.cedula}</td>
                          <td>${Proveedore.idProveedoresNavigation.nombre}</td>
                          <td>${Proveedore.nombreEmpresa}</td>
                          <td>${Proveedore.idProveedoresNavigation.telefono}</td>
                          <td>${Proveedore.email}</td>
                          <td>${'<button class="btn-Select105" data-dismiss="modal">Selecionar</button>'}</td>`;

                tableBody.appendChild(row);
                const selecProve = row.querySelector('.btn-Select105');
                if (selecProve) {
                    selecProve.addEventListener('click', () => {

                        RucProveedorCompra.value = Proveedore.idProveedoresNavigation.cedula;
                        NombreProveedorCompra.value = Proveedore.idProveedoresNavigation.nombre;
                        EmpresaCompra.value = Proveedore.nombreEmpresa;
                        IdProveedorS = parseInt(Proveedore.idproveedor);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
}
///////////////////////////////////////////////////////////////

///////////////Cargar Tabla Productso////////////////////////
function actualizarTablaProductosC() {
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
                        NamePrductoC.value = Productos.nombreProducto;
                        codigoProdC.value = Productos.codigoProd;
                        PrecioVC.value = parseFloat(Productos.precioVenta).toFixed(2);
                        IdProductosC = parseInt(Productos.idProducto);
                        IdCategoriaC = parseInt(Productos.categoria.idCategorias);
                        IdMarcaC = parseInt(Productos.marca.idMarca);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        });
}
/////////////////////////////////////////////////////////////

////////////////////////Obtener Fecha////////////////////////
function obeterfechC() {
    let fechaActual = new Date().toISOString().split('T')[0];
    FechaDelDia.value = fechaActual;
};
////////////////////////////////////////////////////////////

///////////////////Obtener Id//////////////////////////////
function ObtenerUltimoIdCompra() {
    fetch('/ComprasController/ObtenerUltimoIdCompra')
        .then(response => {
            if (!response.ok) {
                throw new Error('No se pudo obtener el último ID de compra.');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            if (data && typeof data.ultimoIdCompra === 'number') {
                ultimoIdCompra = data.ultimoIdCompra;
                document.getElementById("NomeroCompra").value = ultimoIdCompra + 1;
            } else {
                console.log('No se encontró un último ID de compra válido.');
            }
        })
        .catch(error => {
            console.error('Error al obtener el último ID de compra:', error.message);
        });
}
////////////////////////////////////////////////////////// 

///////////////////Agregar Productos/////////////////////
function agregarProductosC() {


    let NamePrductoC = document.getElementById("NombreProductosC");
    let codigoProdC = document.getElementById("codigoProductoC");
    let PrecioVC = document.getElementById("PrecioVentaC");
    let PrecioAC = document.getElementById("PrecioP");
    let CantidadPRoductoC1 = document.getElementById("cantidadC");

    // Asegúrate de que los elementos existan antes de acceder a sus valores
    if (NamePrductoC && codigoProdC && PrecioVC && PrecioAC && CantidadPRoductoC1) {
        const tableBody = document.querySelector('.tbodyYe2');
        const row = document.createElement('tr');

        // Usa toFixed para formatear el número y agrega el símbolo "$"
        row.innerHTML = `<td>${IdProductosC}</td>
                            <td>${NamePrductoC.value}</td>
                            <td>${codigoProdC.value}</td>
                            <td>${CantidadPRoductoC1.value}</td>
                            <td>${parseFloat(PrecioAC.value).toFixed(2)}$</td>
                            <td hidden>${PrecioVC.value}</td>
                            <td><button class="btn-editar">Editar</button></td>
                            <td><button class="btn-delete">Eliminar</button></td>`;

        tableBody.appendChild(row);

        row.querySelector('.btn-delete').addEventListener('click', function() {
            eliminarFila(this);
        });
        totalDC += parseFloat((PrecioAC.value * CantidadPRoductoC1.value).toFixed(2));
        TxtTotalCompra1.value = totalDC;
    } else {
        console.error("Alguno de los elementos no existe o es nulo.");
    }
}
/////////////////////////////////////////////////////////

///////////////Capturar Tabla///////////////////////////
function CapturarTablaC() {
    var Tabla = document.getElementById("Table");
    var filas = Tabla.getElementsByTagName("tr");
    

    for (var i = 1; i < filas.length; i++) {
        var fila = filas[i];

        IdProductosC = parseInt(fila.cells[0].textContent);
        CantidadPRoductoC4 = parseInt(fila.cells[3].textContent);
        CrearDetalleCompra();

    }

}
///////////////////////////////////////////////////////

//////////////////////////////////////////////////////
document.addEventListener('click', function(e) {
    if (e.target.classList.contains('btn-editar')) {
        const fila = e.target.closest('tr');

        if (fila) {
            IdProductosC = fila.querySelector('td:nth-child(1)').textContent.trim();
            const nombreProductoC = fila.querySelector('td:nth-child(2)').textContent.trim();
            const codigoProductoC = fila.querySelector('td:nth-child(3)').textContent.trim();
            const cantidadProductoC = fila.querySelector('td:nth-child(4)').textContent.trim();
            const precioProductoAC = fila.querySelector('td:nth-child(5)').textContent.trim();
            const precioProductovc = fila.querySelector('td:nth-child(6)').textContent.trim();

            document.getElementById("NombreProductosC").value = nombreProductoC;
            document.getElementById("codigoProductoC").value = codigoProductoC;
            document.getElementById("cantidadC").value = cantidadProductoC;
            document.getElementById("PrecioP").value = parseFloat(precioProductoAC.replace('$', ''));
            document.getElementById("PrecioVentaC").value = parseFloat(precioProductovc);

            const cantidadC = parseFloat(cantidadProductoC);
            const precioAC = parseFloat(precioProductoAC);
            
            if (!isNaN(cantidadC) && !isNaN(precioAC)) {
            
                let TotalAux = 0;
                TotalAux = parseFloat((precioAC * cantidadC).toFixed(2));
                totalDC -= TotalAux;
            
                TxtTotalCompra1.value = totalDC;
            } else {
                console.error("Uno o ambos valores no son numéricos.");
               
            }
            btnUpdateProducto.disabled = false;
            GuardarProductos.disabled = true;
  
    }
}
});

function eliminarFila(button) {
    const row = button.closest('tr');

    const precioProductC = parseFloat(row.querySelector('td:nth-child(5)').textContent);
    const cantidadProd = parseFloat(row.querySelector('td:nth-child(4)').textContent);
    

    if (!isNaN(precioProductC) && !isNaN(cantidadProd)) {
        totalDC -= parseFloat((precioProductC * cantidadProd).toFixed(2));
        TxtTotalCompra1.value = totalDC;
    } else {
        console.error("Uno o ambos valores no son numéricos.");
    }

    row.remove();
}
/////////////////////////////////////////////////////

///////////////////////////////////////////////////
function limpiarTabla2() {
    const tableBody2 = document.querySelector('.tbodyYe2');

    while (tableBody2.firstChild) {
        tableBody2.removeChild(tableBody2.firstChild);
    }

    totalDC = 0;
    TxtTotalCompra1.value = totalDC;
}
/////////////////////////////////////////////////////

//////////////////////////////////////////////////////
function actualizarProductosC() {
    alert(totalDC);
    const nombreProductoC =  document.getElementById("NombreProductosC").value;
    const codigoProductoC = document.getElementById("codigoProductoC").value;
    const cantidadProductoC = document.getElementById("cantidadC").value;
    const precioProductAC = document.getElementById("PrecioP").value;
    const precioProductovc = document.getElementById("PrecioVentaC").value;

    const filas = document.querySelectorAll('.tbodyYe2 tr');
    let filaOriginal;

    filas.forEach((fila) => {
        if (fila.textContent.includes(IdProductosC)) {
            filaOriginal = fila;
        }
    });

    if (filaOriginal) {


        filaOriginal.innerHTML = `<td>${IdProductosC}</td>
                                    <td>${nombreProductoC}</td>
                                    <td>${codigoProductoC}</td>
                                    <td>${cantidadProductoC}</td>
                                    <td>${precioProductAC}$</td>
                                    <td><button class="btn-editar">Editar</button></td>
                                    <td><button class="btn-delete">Eliminar</button></td>`;
totalDC += parseFloat((precioProductAC * cantidadProductoC).toFixed(2));
        TxtTotalCompra1.value = totalDC ;

        document.getElementById("guardar").disabled = false;
        document.getElementById("actualizar").disabled = true;

        Limpiarproducto();
    } else {
        console.error('No se encontró la fila original.');
    }
}

/////////////////////////////////////////////////////

//#endregion
///////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////
//#region  Validaciones 

function desactivarCP(){
    NamePrductoC.disabled = true;
    codigoProdC.disabled = true;
    PrecioVC.disabled = true;
    PrecioAC.disabled = true;
    CantidadPRoductoC1.disabled = true;
    TxtTotalCompra1.disabled = true;
    GuardarProductos.disabled = true;
    GuardarCompra.disabled = true;
     FechaDelDia.disabled = true;
 NoCompra.disabled = true;
 NombreProveedorCompra.disabled =true;;
 RucProveedorCompra.disabled = true;
 EmpresaCompra.disabled = true;
 AGGPro.disabled = true;
 AggProveedor.disabled = true;
};
function activarCP(){
   AGGPro.disabled = false;
   AggProveedor.disabled = false;
   GuardarProductos.disabled = false;
   PrecioAC.disabled = false;
   CantidadPRoductoC1.disabled = false;
};
function Limpiarproducto(){
    NamePrductoC.value = "";
    codigoProdC.value = "";
    PrecioVC.value = "";
    PrecioAC.value = "";
    CantidadPRoductoC1.value = "";
};

function LimpiarTodo(){
    NamePrductoC.value = "";
    codigoProdC.value = "";
    PrecioVC.value = "";
    PrecioAC.value = "";
    CantidadPRoductoC1.value = "";
    TxtTotalCompra1.value = "";
     FechaDelDia.value = "";
 NoCompra.value = "";
 NombreProveedorCompra.value ="";;
 RucProveedorCompra.value = "";
 EmpresaCompra.value = "";
};

//#endregion
//////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////
//////////////////////JomWest Inc.///////////////////////////////
////////////////////////////////////////////////////////////////
