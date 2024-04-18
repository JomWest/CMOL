// Obtener referencias a los elementos del DOM
const nuevaUsuarioBtn = document.getElementById("nuevaUsuarioBtn");
const cedulaInput = document.getElementById("cedula");
const nombreUsuarioInput = document.getElementById("nombreUsuario");
const apellidoUsuarioInput = document.getElementById("apellidoUsuario");
const telefonoUsuarioInput = document.getElementById("telefonoUsuario");
const emailUsuarioInput = document.getElementById("emailUsuario");
const rolUsuarioSelect = document.getElementById("rolUsuario");
const estadoUsuarioSelect = document.getElementById("estadoUsuario");
const Usuarioa = document.getElementById("NameUsuarioModal");
const Contraseña = document.getElementById("ContraseñaModal");
const Contraseña2 = document.getElementById("RepitContraeñaModal");
const GuardarBtnM = document.getElementById("GuardarBtnModal");
const actualizarBtn = document.getElementById("ActualizarBtnModal");
const Btnfrom = document.getElementById(".btn btn-primary");
const BtnactF = document.getElementById(".BtnF");
let idUsuario;

///////////////*
desactivar();
//////////////*
nuevaUsuarioBtn.addEventListener("click", function () {
    activar();
});
/////////////////////////////////////////////////////////////////////////////////////
//#region  eventos de los botones crear y actualizar 
actualizarBtn.addEventListener("click", function () {
    if (apellidoUsuarioInput.value == "" || nombreUsuarioInput.value == "" || cedulaInput.value == "" || emailUsuarioInput.value == "" || telefonoUsuarioInput.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }

    if (cedulaInput.value.length != 16) {
        alert("Ingrese un número de cédula válido");
        return;
    }


    if (telefonoUsuarioInput.value.length != 8) {
        alert("El número de teléfono ingresado no contiene 8 caracteres");
        return;
    }

    if (Number.isInteger(parseInt(telefonoUsuarioInput.value))) {} else {
        alert("Ingrese un número de teléfono válido")
        return;
    }

    if (estadoUsuarioSelect.value == "") {
        alert("Seleccione el estado del usuario");
        return;
    }

    if (rolUsuarioSelect.value == "") {
        alert("Seleccione el estado del usuario");
        return;
    }

    if (EmailValido(emailUsuarioInput.value)) {} else {
        alert("Ingrese una dirección de correo electrónico válida");
        return;
    }

    actualizarUsuario();
    Limpiar();
    desactivar();
});
GuardarBtnM.addEventListener("click", function () {
    if (apellidoUsuarioInput.value == "" || nombreUsuarioInput.value == "" || cedulaInput.value == "" || emailUsuarioInput.value == "" || telefonoUsuarioInput.value == "") {
        alert("Rellene todos los campos correspondientes");
        return;
    }

    if (cedulaInput.value.length != 16) {
        alert("Ingrese un número de cédula válido");
        return;
    }


    if (telefonoUsuarioInput.value.length != 8) {
        alert("El número de teléfono ingresado no contiene 8 caracteres");
        return;
    }

    if (Number.isInteger(parseInt(telefonoUsuarioInput.value))) {} else {
        alert("Ingrese un número de teléfono válido")
        return;
    }

    if (estadoUsuarioSelect.value == "") {
        alert("Seleccione el estado del usuario");
        return;
    }

    if (rolUsuarioSelect.value == "") {
        alert("Seleccione el estado del usuario");
        return;
    }

    if (EmailValido(emailUsuarioInput.value)) {} else {
        alert("Ingrese una dirección de correo electrónico válida");
        return;
    }

    CrearUsuario();
    Limpiar();
    desactivar();
});
//#endregion
/////////////////////////////////////////////////////////////////////////////////////

var inputs = document.getElementsByClassName("input");
////////////////////////////////////////////////////////////////////////////////////
//#region Crear usuario
function CrearUsuario(){
    //obtener valores de los campos de entrada
    let cedulaInput1 = document.getElementById("cedula");
    let nombreUsuarioInput1 = document.getElementById("nombreUsuario");
    let apellidoUsuarioInput1 = document.getElementById("apellidoUsuario");
    let telefonoUsuarioInput1 = document.getElementById("telefonoUsuario");
    let emailUsuarioInput1 = document.getElementById("emailUsuario");
    let rolUsuarioSelect1 = document.getElementById("rolUsuario");
    let estadoUsuarioSelect1 = document.getElementById("estadoUsuario");
    let Usuarioa1 = document.getElementById("NameUsuarioModal");
    let Contraseña1 = document.getElementById("ContraseñaModal");



    const nuevoUsuarioForm = {
        cedula: cedulaInput1.value.trim(),
        nombre: nombreUsuarioInput1.value.trim(),
        apellido: apellidoUsuarioInput1.value.trim(),
        telefono: telefonoUsuarioInput1.value.trim(),
        email: emailUsuarioInput1.value.trim(),
        Usuario1: Usuarioa1.value.trim(),
        Contraseña: Contraseña1.value.trim(),
        cargo: rolUsuarioSelect1.value,
        estado: estadoUsuarioSelect1.value,
    };
    fetch('/Usuario/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoUsuarioForm)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Usuario registrado exitosamente');
                actualizarTabla();
            } else {
                alert('Error al registrar Proveedor:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));

    alert("Se guardo el Usuario con exito");
}
//#endregion
///////////////////////////////////////////////////////////////////////////////////
//#region  actualizar tabla
///////////////////////////////////////////////////////////////////////////////////
//#region  Actualizar usuario
function actualizarUsuario() {
    //obtener valores de los campos de entrada
    let cedulaInput1 = document.getElementById("cedula");
    let nombreUsuarioInput1 = document.getElementById("nombreUsuario");
    let apellidoUsuarioInput1 = document.getElementById("apellidoUsuario");
    let telefonoUsuarioInput1 = document.getElementById("telefonoUsuario");
    let emailUsuarioInput1 = document.getElementById("emailUsuario");
    let rolUsuarioSelect1 = document.getElementById("rolUsuario");
    let estadoUsuarioSelect1 = document.getElementById("estadoUsuario");
    let Usuarioa1 = document.getElementById("NameUsuarioModal");
    let Contraseña1 = document.getElementById("ContraseñaModal");



    const nuevoUsuarioForm = {
        cedula: cedulaInput1.value.trim(),
        nombre: nombreUsuarioInput1.value.trim(),
        apellido: apellidoUsuarioInput1.value.trim(),
        telefono: telefonoUsuarioInput1.value.trim(),
        email: emailUsuarioInput1.value.trim(),
        Usuario1: Usuarioa1.value.trim(),
        Contraseña: Contraseña1.value.trim(),
        cargo: rolUsuarioSelect1.value,
        estado: estadoUsuarioSelect1.value,
    };

    fetch(`/Usuario/Update/${idUsuario}/`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoUsuarioForm)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            if (data.success === true) {
                alert('Usuario actualizado exitosamente');
                actualizarTabla();
            } else {
                alert('Error al actualizar el usuario');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            // Muestra detalles del error en la consola
        });

}

window.onload = function () {
    actualizarTabla();
}

function actualizarTabla() {
    fetch('/Usuario/GetAll/')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            const tableBody = document.querySelector('.tbody');
            tableBody.innerHTML = '';

            data.forEach(usuario => {
                const row = document.createElement('tr');
                row.innerHTML = `<td>${usuario.idUsuario}</td>
                          <td>${usuario.idUsuarioNavigation.cedula}</td>
                          <td>${usuario.idUsuarioNavigation.nombre}</td>
                          <td>${usuario.idUsuarioNavigation.apellido}</td>
                          <td>${usuario.idUsuarioNavigation.telefono}</td>
                          <td>${usuario.cargo}</td>
                          <td>${usuario.email}</td>

                          <td>${'<a href="#" class="editarBtn"><i class="fas fa-edit"></i></a>'}</td>`;

                tableBody.appendChild(row);

                const editarBtn = row.querySelector('.editarBtn');
                if (editarBtn) {
                    editarBtn.addEventListener('click', () => {

                        cedulaInput.value = usuario.idUsuarioNavigation.cedula;
                        apellidoUsuarioInput.value = usuario.idUsuarioNavigation.apellido;
                        nombreUsuarioInput.value = usuario.idUsuarioNavigation.nombre;
                        telefonoUsuarioInput.value = usuario.idUsuarioNavigation.telefono;
                        estadoUsuarioSelect.value = usuario.estado;
                        rolUsuarioSelect.value = usuario.cargo;
                        Usuarioa.value = usuario.usuarios;
                        emailUsuarioInput.value = usuario.email;
                        Contraseña.value = usuario.contraseña;
                        Contraseña2.value = usuario.contraseña;
                        idUsuario = parseInt(usuario.idUsuario);
                        activar();
                    });
                } else {
                    console.error('Elemento editarBtn no encontrado en la fila:', row);
                }
            });
        })
};
//#endregion
//////////////////////////////////////////////////////////////////////////////////
//#region Funciones Basicos
function desactivar() {
    Usuarioa.disabled = true;
    Contraseña.disabled = true;
    Contraseña.disabled = true;
    cedulaInput.disabled = true;
    nombreUsuarioInput.disabled = true;
    apellidoUsuarioInput.disabled = true;
    telefonoUsuarioInput.disabled = true;
    estadoUsuarioSelect.disabled = true;
    rolUsuarioSelect.disabled = true;
    emailUsuarioInput.disabled = true;
};

function activar() {
    Usuarioa.disabled = false;
    Contraseña.disabled = false;
    Contraseña.disabled = false;
    cedulaInput.disabled = false;
    nombreUsuarioInput.disabled = false;
    apellidoUsuarioInput.disabled = false;
    telefonoUsuarioInput.disabled = false;
    estadoUsuarioSelect.disabled = false;
    rolUsuarioSelect.disabled = false;
    emailUsuarioInput.disabled = false;

};
function Limpiar() {
    Usuarioa.value = "";
    Contraseña.value = "";
    Contraseña.value = "";
    cedulaInput.value = "";
    nombreUsuarioInput.value = "";
    apellidoUsuarioInput.value = "";
    telefonoUsuarioInput.value = "";
    estadoUsuarioSelect.value = "";
    rolUsuarioSelect.value = "";
    emailUsuarioInput.value = "";

};
// Funcion para verificar la validez de un email
function EmailValido(email) {
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}


//#endregion
/////////////////////////////////////////////////////////////////////////////////