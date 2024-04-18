// Obtener referencias a los elementos del DOM
const nuevoProveedorBtn = document.getElementById("nuevoProveedorBtn");
const RucInput = document.getElementById("Ruc");
const NameInput = document.getElementById("nombre");
const ApellidoImput = document.getElementById("apellido");
const TelefonoInput = document.getElementById("telefono");
const EmailInput = document.getElementById("email");
const DireccionInput = document.getElementById("direccion");
const EmperesaInput = document.getElementById("Empresa");
const Departamentoinput = document.getElementById("Departamento");

const GuardarBtnM = document.getElementById("GuardarProvedor");
const ActualizarBtn = document.getElementById("Actualizarproveedor");
let idproveedor;
var inputs = document.getElementsByClassName("input");

nuevoProveedorBtn.addEventListener("click", function () {
activarP();
ActualizarBtn.disabled = true;
});
/////////
/////////
ActualizarBtn.addEventListener("click", function () {
    //valdaciones
    if (DireccionInput.value == "" || NameInput.value == "" || RucInput.value == "" || EmailInput.value == "" || ApellidoImput.value == "" || Departamentoinput.value == "" || EmperesaInput.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }


    if (TelefonoInput.value.length != 8) {
        alert("El número de teléfono ingresado no contiene 8 caracteres");
        return;
    }

    if (Number.isInteger(parseInt(TelefonoInput.value))) {} else {
        alert("Ingrese un número de teléfono válido")
        return;
    }


    if (EmailValido(EmailInput.value)) {} else {
        alert("Ingrese una dirección de correo electrónico válida");
        return;
    }
    actualizarUsuario();
    desactivarP();
    LimpiarP();
    ActualizarBtn.disabled = false;
    GuardarBtnM.disabled = true
});


GuardarBtnM.addEventListener("click", function () {
    //valdaciones
    if (DireccionInput.value == "" || NameInput.value == "" || RucInput.value == "" || EmailInput.value == "" || ApellidoImput.value == "" || Departamentoinput.value == "" || EmperesaInput.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }


    if (TelefonoInput.value.length != 8) {
        alert("El número de teléfono ingresado no contiene 8 caracteres");
        return;
    }

    if (Number.isInteger(parseInt(TelefonoInput.value))) {} else {
        alert("Ingrese un número de teléfono válido")
        return;
    }


    if (EmailValido(EmailInput.value)) {} else {
        alert("Ingrese una dirección de correo electrónico válida");
        return;
    }

    //Capturar datos
    let RucInput1 = document.getElementById("Ruc").value;
    let NameInput1 = document.getElementById("nombre").value;
    let ApellidoImput1 = document.getElementById("apellido").value;
    let TelefonoInput1 = document.getElementById("telefono").value;
    let EmailInput1 = document.getElementById("email").value;
    let DireccionInput1 = document.getElementById("direccion").value;
    let EmperesaInput1 = document.getElementById("Empresa").value;
    let Departamentoinput1 = document.getElementById("Departamento").value;

    const NuevoProveedorData = {

        cedula: RucInput1,
        nombre: NameInput1,
        apellido: ApellidoImput1,
        telefono: TelefonoInput1,
        nombreEmpresa: EmperesaInput1,
        departamento: Departamentoinput1,
        direccion: DireccionInput1,
        email: EmailInput1
    };
    fetch('/Proveedor/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoProveedorData)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Proveedor registrado exitosamente');
                actualizarTabla();
            } else {
                alert('Error al registrar Proveedor:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));
        desactivarP();
        LimpiarP();
});

function EmailValido(email) {
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}


function Limpiar(inputs) {
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].value = '';
    }
}





window.onload = function () {
    actualizarTabla();
    desactivarP();
}

function actualizarTabla() {
    fetch('/Proveedor/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody');
            tableBody.innerHTML = '';

            data.forEach(Proveedore => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${Proveedore.idproveedor}</td>
                          <td>${Proveedore.idProveedoresNavigation.cedula}</td>
                          <td>${Proveedore.idProveedoresNavigation.nombre}</td>
                          <td>${Proveedore.nombreEmpresa}</td>
                          <td>${Proveedore.idProveedoresNavigation.telefono}</td>
                          <td>${Proveedore.email}</td>
                          <td>${'<a href="#" class="btnAct"><i class="fas fa-edit"></i></a>'}</td>`;

                tableBody.appendChild(row);
                const editarBtn = row.querySelector('.btnAct');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        RucInput.value = Proveedore.idProveedoresNavigation.cedula;
                        NameInput.value = Proveedore.idProveedoresNavigation.nombre;
                        ApellidoImput.value = Proveedore.idProveedoresNavigation.apellido;
                        TelefonoInput.value = Proveedore.idProveedoresNavigation.telefono;
                        EmailInput.value = Proveedore.email;
                        DireccionInput.value = Proveedore.direcion;
                        EmperesaInput.value = Proveedore.nombreEmpresa;
                        Departamentoinput.value = Proveedore.departamento;
                        idproveedor = parseInt(Proveedore.idproveedor);
                        ActualizarBtn.disabled = false;
                        GuardarBtnM.disabled = true;

                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
}


function actualizarUsuario() {

    //Capturar datos
    let RucInput1 = document.getElementById("Ruc").value;
    let NameInput1 = document.getElementById("nombre").value;
    let ApellidoImput1 = document.getElementById("apellido").value;
    let TelefonoInput1 = document.getElementById("telefono").value;
    let EmailInput1 = document.getElementById("email").value;
    let DireccionInput1 = document.getElementById("direccion").value;
    let EmperesaInput1 = document.getElementById("Empresa").value;
    let Departamentoinput1 = document.getElementById("Departamento").value;


    const NuevoProveedorData = {
        cedula: RucInput1,
        nombre: NameInput1,
        apellido: ApellidoImput1,
        telefono: TelefonoInput1,
        nombreEmpresa: EmperesaInput1,
        departamento: Departamentoinput1,
        direccion: DireccionInput1,
        email: EmailInput1
    };
    fetch(`/Proveedor/Update/${idproveedor}/`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(NuevoProveedorData)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            if (data.success === true) {
                alert('Proveedor actualizado exitosamente');
                actualizarTabla();
            } else {
                alert('Error al actualizar el proveedor');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            // Muestra detalles del error en la consola
        });

}

function Limpiar(inputs) {
    if (btnCancelar.disabled == true) {
        alert("Desactivado");
    }
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].value = '';
        inputs[i].disabled = true;
    }
}
var inputs = document.getElementsByClassName("input");


function desactivarP() {
    RucInput.disabled = true;
    NameInput.disabled = true;
    ApellidoImput.disabled = true;
    TelefonoInput.disabled = true;
    EmailInput.disabled = true;
    DireccionInput.disabled = true;
    EmperesaInput.disabled = true;
    Departamentoinput.disabled = true;
    GuardarBtnM.disabled = true;
    ActualizarBtn.disabled = true;
}
function activarP(){
    RucInput.disabled = false;
    NameInput.disabled = false;
    ApellidoImput.disabled = false;
    TelefonoInput.disabled = false;
    EmailInput.disabled = false;
    DireccionInput.disabled = false;
    EmperesaInput.disabled = false;
    Departamentoinput.disabled = false;
    GuardarBtnM.disabled = false;
    ActualizarBtn.disabled = false;
};
function LimpiarP(){
    RucInput.value = ""
    NameInput.value = "";
    ApellidoImput.value = "";
    TelefonoInput.value = "";
    EmailInput.value = "";
    DireccionInput.value = "";
    EmperesaInput.value = "";
    Departamentoinput.value = "";
};

function Activar(inputs) {
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].disabled = false;
    }
}