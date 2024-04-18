window.onload = function () {
    actualizarTablaCategoria();
    actualizarTablaMarca();
    actualizarTablaProductos();
    desactivarPr();
}
//////////////////////////////////////////////////////////
//#region CapturarDatos
const Nuevoproductobtn = document.getElementById("nuevoProducto");
const NameProductImput = document.getElementById("NombreProducto");
const CodigoProdcuto = document.getElementById("codigoProducto");
const MarcaProduct = document.getElementById("MarcaProducto");
const CategoriaProduct = document.getElementById("CategoriaProducto");
const StockProduct = document.getElementById("StockProductos");
const StockMInimo = document.getElementById("StockMin");
const PrevioVentaInput = document.getElementById("PrecioVentaProd");
const MarcaModal1 = document.getElementById("nombreMarcaModal");
const CategoriaModal1 = document.getElementById("nombreCategoriaModal");
const descripción1 = document.getElementById("DescripcionCategoriaModal");
let IdMarca;
let IdCategoria;
let IdProductos;
//////Botones/////
const BtnSavecategoria = document.getElementById("BtnSaveCategoria");
const BtnSaveMarca = document.getElementById("BtnSaveMarca");
const BtnSaveProduct = document.getElementById("guardar");
const BtnActualizarM = document.getElementById("BtnACtualizarMarca");
const BtnActualizarC = document.getElementById("BtnACtualizarCategoria");
const BtnActualizarProd = document.getElementById("actualizar");
const BtnCategoria = document.getElementById("BtnCategoria");
const BtnMarca = document.getElementById("btnMarcaModal");
/////////////////
//#endregion
/////////////////////////////////////////////////////////
//#region Evento de los Botones
////////////////////////////////////////////////////////
///////////Botones para Guardar////////////////////////
///////////////////////////////////////////////////////
BtnSaveMarca.addEventListener("click", function () {
    if (MarcaModal1.value == "" ) {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    CrearMarca();
    MarcaModal1.value = "";
});

BtnSavecategoria.addEventListener("click", function () {
    if (CategoriaModal1.value == "" || descripción1.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    CrearCategoria();
    actualizarTablaCategoria();
    CategoriaModal1.value = "";
    descripción1.value = "";
});

BtnSaveProduct.addEventListener("click", function () {
    if (NameProductImput.value == "" || CodigoProdcuto.value == "" || MarcaProduct.value == "" || CategoriaProduct.value == "" || StockProduct.value == "" || StockMInimo.value == "" || PrevioVentaInput == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(StockProduct.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(StockMInimo.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(StockMInimo.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(PrevioVentaInput.value))) {} else {
        alert("Ingrese un precio de venta válido")
        return;
    }
    CrearProductos();
    Limpiarproducto();
    desactivarPr();
    Nuevoproductobtn.disabled = false;
});

Nuevoproductobtn.addEventListener("click", function () {
    ActivarProductos();
    Nuevoproductobtn.disabled = true;
});

BtnMarca.addEventListener("click", function () {
    BtnActualizarM.disabled = true;
});

BtnCategoria.addEventListener("click", function () {
    BtnActualizarC.disabled = true;
});


////////////////////////////////////////////////////////
///////////Botones para Actualizar/////////////////////
//////////////////////////////////////////////////////

BtnActualizarM.addEventListener("click", function () {
    if (MarcaModal1.value == "" ) {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    actualizarMarca();
    MarcaModal1.value = "";
    BtnActualizarM.disabled = true;
    BtnSaveMarca. disabled = false;

});
BtnActualizarC.addEventListener("click", function () {
    if (CategoriaModal1.value == "" || descripción1.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    ActualizarCategoria();
    CategoriaModal1.value = "";
    descripción1.value = "";
    BtnActualizarC.disabled = true;
    BtnSavecategoria. disabled = false;
});
BtnActualizarProd.addEventListener("click", function () {
    if (NameProductImput.value == "" || CodigoProdcuto.value == "" || MarcaProduct.value == "" || CategoriaProduct.value == "" || StockProduct.value == "" || StockMInimo.value == "" || PrevioVentaInput == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(StockProduct.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(StockMInimo.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(StockMInimo.value))) {} else {
        alert("Ingrese un numero en el stock válido")
        return;
    }
    if (Number.isInteger(parseInt(PrevioVentaInput.value))) {} else {
        alert("Ingrese un precio de venta válido")
        return;
    }
    actualizarProductos();
    Limpiarproducto();
    desactivarPr();
    Nuevoproductobtn.disabled = false;

});
//#endregion
///////////////////////////////////////////////////////////
//#region Metodos Crear
function CrearMarca() {
    let MarcaModal = document.getElementById("nombreMarcaModal").value;
    const NuenaMarca = {
        NombreMarca: MarcaModal
    };
    fetch('/MarcasController/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuenaMarca)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Marca registrada exitosamente');
                actualizarTablaMarca();
            } else {
                alert('Error al registrar la Marca:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));

};
////////////////////////////////////////////////////////////
////////////*Crear Categorias*/////////////////////////////
//////////////////////////////////////////////////////////
function CrearCategoria() {
    let CategoriaModal = document.getElementById("nombreCategoriaModal").value;
    let Descripcion = document.getElementById("DescripcionCategoriaModal").value;

    const NuevaCategoria = {
        nombre: CategoriaModal,
        descripción: Descripcion
    };

    fetch('/CategoriaController/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevaCategoria)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Categoria registrada exitosamente');
                actualizarTablaCategoria();
            } else {
                alert('Error al registrar la Categoria:', data.message);
            }
        })

};

///////////////////////////////////////////////////////////
////////////*Crear Productos*/////////////////////////////
/////////////////////////////////////////////////////////
function CrearProductos() {

    let NameProductImput = document.getElementById("NombreProducto").value;
    let CodigoProdcuto = document.getElementById("codigoProducto").value;
    let PrevioVentaInput1 = document.getElementById("PrecioVentaProd").value;
    let StockProduct = document.getElementById("StockProductos").value;
    let StockMInimoProduct = document.getElementById("StockMin").value;
    const NuevoProducto = {
        codigoProd: parseInt(CodigoProdcuto),
        nombreProducto: NameProductImput,
        precioVenta: Number(PrevioVentaInput1),
        stock: Number(StockProduct),
        StockMInimo: parseInt(StockMInimoProduct),
        idCategorias: IdCategoria,
        idMarca: IdMarca
    };


    fetch('/Productos/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoProducto)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Producto registrado exitosamente');
                actualizarTablaProductos();
            } else {
                alert('Error al registrar el Producto:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));

};


//#endregion
///////////////////////////////////////////////////////////
//#region Metodod Actualizar
///////////////////////////////////////////////////////
///////////////Actualizar Categoria///////////////////
/////////////////////////////////////////////////////
function ActualizarCategoria()
{

   let categoriaA = document.getElementById("nombreCategoriaModal").value;
   let DescripcionCat = document.getElementById("DescripcionCategoriaModal").value;
       const CategoriaData =
       {
        nombre : categoriaA,
        descripción : DescripcionCat,
       };
       fetch(`/CategoriaController/Update/${IdCategoria}`,
       {
        method:'PUT',
        headers: {
          'Content-Type': 'Application/json'
        }, 
        body:JSON.stringify(CategoriaData)
       }).then(response => response.json()).then(data=>{
        console.log(data);
        if(data.success===true)
        {
            alert('Componente actualizado exitoxamente');
            actualizarTablaCategoria();
        }
        else{
            alert('Error al actualizar el componente');
        }
       }).catch(error=> console.error('Error:',error)); 
}

///////////////////////////////////////////////////////
///////////////Actualizar Marca///////////////////////
//////////////////////////////////////////////////////
function actualizarMarca() {

    let MarcaModal = document.getElementById("nombreMarcaModal").value;
    const NuenaMarca = {
        NombreMarca: MarcaModal
    };
    fetch(`/MarcasController/Update/${IdMarca}/`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuenaMarca)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            if (data.success === true) {
                alert('Marca actualizada exitosamente');
                actualizarTablaMarca();
            } else {
                alert('Error al actualizar la Marca');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            // Muestra detalles del error en la consola
        });
}
///////////////////////////////////////////////////////
///////////////Actualizar Productos///////////////////
/////////////////////////////////////////////////////
function actualizarProductos() {

    let NameProductImput = document.getElementById("NombreProducto").value;
    let CodigoProdcuto = document.getElementById("codigoProducto").value;
    let PrevioVentaInput1 = document.getElementById("PrecioVentaProd").value;
    let StockProduct = document.getElementById("StockProductos").value;
    let StockMInimoProduct = document.getElementById("StockMin").value;

    const NuevoProducto = {
        codigoProd: parseInt(CodigoProdcuto),
        nombreProducto: NameProductImput,
        precioVenta: Number(PrevioVentaInput1),
        stock: parseInt(StockProduct),
        StockMInimo: parseInt(StockMInimoProduct),
        idCategorias: IdCategoria,
        idMarca: IdMarca
    };
    fetch(`/Productos/Update/${IdProductos}/`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoProducto)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            if (data.success === true) {
                alert('producto actualizado exitosamente');
                actualizarTablaProductos();
            } else {
                alert('Error al actualizar el producto');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            // Muestra detalles del error en la consola
        });
}
//#endregion
///////////////////////////////////////////////////////////
//#region Metodos Para cargar Tablas
function actualizarTablaMarca() {
    fetch('/MarcasController/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody11');
            tableBody.innerHTML = '';

            data.forEach(Marca => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Marca.idMarca}</td>
                          <td>${Marca.nombreMarca}</td>
                          <td>${'<button class="btn-editar">Editar</button>'}</td>
                          <td>${'<button class="btn-Select" data-dismiss="modal">Selecionar</button>'}</td>`;

                tableBody.appendChild(row);
                const editarBtn = row.querySelector('.btn-editar');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        MarcaModal1.value = Marca.nombreMarca;
                        IdMarca = parseInt(Marca.idMarca);
                        BtnSaveMarca.disabled = true;
                        BtnActualizarM.disabled = false;

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
                const Selectbtn = row.querySelector('.btn-Select');
                if (Selectbtn) {
                    Selectbtn.addEventListener('click', () => {

                        MarcaProduct.value = Marca.nombreMarca;
                        IdMarca = parseInt(Marca.idMarca);
                  
                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
}
//////////////////////////////////////////////////////////////////////////////////////////////
/*Actualizar TablaCategoria*/
//////////////////////////////////////////////////////////////////////////////////////////////
function actualizarTablaCategoria() {
    fetch('/CategoriaController/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody1');
            tableBody.innerHTML = '';

            data.forEach(Categoria => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Categoria.idCategorias}</td>
                          <td>${Categoria.nombre}</td>
                          <td>${Categoria.descripción}</td>
                          <td>${'<button class="btn-editar">Editar</button>'}</td>
                          <td>${'<button class="btn-Select1" data-dismiss="modal">Selecionar</button>'}</td>`;

                tableBody.appendChild(row);
                const editarBtn = row.querySelector('.btn-editar');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        CategoriaModal1.value = Categoria.nombre;
                        descripción1.value = Categoria.descripción;
                        IdCategoria = parseInt(Categoria.idCategorias);
                        BtnActualizarC.disabled = false;
                        BtnSavecategoria.disabled = true;

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
                const SelectCategoria = row.querySelector('.btn-Select1');
                if (SelectCategoria) {
                    SelectCategoria.addEventListener('click', () => {

                        CategoriaProduct.value = Categoria.nombre,
                            IdCategoria = parseInt(Categoria.idCategorias);

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
}
//////////////////////////////////////////////////////////////////
///////////Actualizar Tabla Productos////////////////////////////
/////////////////////////////////////////////////////////////////
function actualizarTablaProductos() {
    fetch('/Productos/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody2');
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
                          <td>${'<button class="btn-editar">Editar</button>'}</td>`;

                tableBody.appendChild(row);
                const editarBtn = row.querySelector('.btn-editar');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        NameProductImput.value = Productos.nombreProducto;
                        CodigoProdcuto.value = Productos.codigoProd;
                        MarcaProduct.value = Productos.marca.nombreMarca;
                        CategoriaProduct.value = Productos.categoria.nombre;
                        StockProduct.value = Productos.stock;
                        StockMInimo.value = Productos.stockMinimo;
                        document.getElementById("StockMin");
                        PrevioVentaInput.value = Productos.precioVenta;
                        IdProductos = parseInt(Productos.idProducto);
                        IdMarca = parseInt(Productos.marca.idMarca);
                        IdCategoria = parseInt(Productos.categoria.idCategorias);
                        ActivarProductos();
                        BtnSaveProduct.disabled = true;
                        BtnActualizarProd.disabled = false;

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
}
//#endregion
//////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////
//#region Metodos Generales
function desactivarPr() {
    NameProductImput.disabled = true;
    CodigoProdcuto.disabled = true;
    MarcaProduct.disabled = true;
    CategoriaProduct.disabled = true;
    StockProduct.disabled = true;
    StockMInimo.disabled = true;
    PrevioVentaInput.disabled = true;
    MarcaModal1.disabled = true;
    CategoriaModal1.disabled = true;
    descripción1.disabled = true;
    BtnSavecategoria.disabled = true;
    BtnSaveMarca.disabled = true;
    BtnSaveProduct.disabled = true;
    BtnActualizarM.disabled = true;
    BtnActualizarC.disabled = true;
    BtnActualizarProd.disabled = true;
    BtnMarca.disabled = true;
    BtnCategoria.disabled = true;
}

function ActivarProductos() {
    NameProductImput.disabled = false;
    CodigoProdcuto.disabled = false;;
    StockProduct.disabled = false;
    StockMInimo.disabled = false;
    PrevioVentaInput.disabled = false;
    MarcaModal1.disabled = false;
    CategoriaModal1.disabled = false;
    descripción1.disabled = false;
    BtnSavecategoria.disabled = false;
    BtnSaveMarca.disabled = false;
    BtnSaveProduct.disabled = false;
    BtnActualizarM.disabled = false;
    BtnActualizarC.disabled = false;
    BtnMarca.disabled = false;
    BtnCategoria.disabled = false;
};

function Limpiarproducto() {
    Nuevoproductobtn.value = "";
    NameProductImput.value = "";
    CodigoProdcuto.value = "";
    MarcaProduct.value = "";
    CategoriaProduct.value = "";
    StockProduct.value = "";
    StockMInimo.value = "";
    PrevioVentaInput.value = "";
    MarcaModal1.value = "";
    CategoriaModal1.value = "";
    descripción1.value = "";

};
//#endregion
/////////////////////////////////////////////////////////

//#region Validaciones
var Productos = {
    desactivarCamposYBotones: function () {
        var campos = document.querySelectorAll('input, textarea, select');
        campos.forEach(function (campo) {
            campo.disabled = true;
        });

        var botones = document.querySelectorAll('button');
        botones.forEach(function (boton) {
            boton.disabled = true;
        });

        var nuevaVentaBtn = document.getElementById('nuevoProducto');
        nuevaVentaBtn.disabled = false;
    },

    activarCamposYBotones: function () {
        var campos = document.querySelectorAll('input, textarea, select');
        campos.forEach(function (campo) {
            campo.disabled = false;
        });

        var botones = document.querySelectorAll('button');
        botones.forEach(function (boton) {
            boton.disabled = false;
        });

        var nuevaVentaBtn = document.getElementById('nuevoProducto');
        nuevaVentaBtn.disabled = true;
    },

    validarCampoNoVacio: function (valor, nombreCampo) {
        if (valor.trim() === '') {
            alert(`El campo ${nombreCampo} es obligatorio. Por favor, complete el campo.`);
            return false;
        }
        return true;
    },

    validarCampoNumerico: function (valor, nombreCampo) {
        if (isNaN(valor)) {
            alert(`El campo ${nombreCampo} debe ser un valor numérico.`);
            return false;
        }
        return true;
    },

    validarFormatoRUC: function (ruc) {
        var regexRUC = /^\d{3}-\d{6}-\d{4}$/;
        if (!regexRUC.test(ruc)) {
            alert('El RUC debe tener el formato xxx-xxxxxx-xxxx.');
            return false;
        }
        return true;
    },

    validarFormularioPrincipal: function () {
        var nombreProducto = document.getElementById('producto').value;
        var codigoProducto = document.getElementById('codigoProducto').value;
        var categoriaProducto = document.getElementById('categoria').value;
        var marcaProducto = document.getElementById('marca').value;
        var cantidadProducto = document.getElementById('cantidad').value;

        if (!this.validarCampoNoVacio(nombreProducto, 'Nombre del Producto') ||
            !this.validarCampoNoVacio(codigoProducto, 'Código del Producto') ||
            !this.validarCampoNoVacio(categoriaProducto, 'Categoría del Producto') ||
            !this.validarCampoNoVacio(marcaProducto, 'Marca del Producto') ||
            !this.validarCampoNumerico(cantidadProducto, 'Cantidad del Producto')) {
            return false;
        }

        alert('Producto guardado exitosamente');
        return true;
    }

}
//#endregion