const CambioImput = document.getElementById("cambioInput");
const btnacpetarCambio = document.getElementById("BtnSaveCambio");


window.onload = function () {
    ComprobarCambios2();
}

btnacpetarCambio.addEventListener("click", function(){
    if (CambioImput.value == "" ) {
        alert("Rellene todos los campos correspondientes");
        return;
    }
    if (Number.isInteger(parseInt(CambioImput.value))) {} else {
        alert("Ingrese un valor de cambio válido")
        return;
    }

GuardarCambio();
ComprobarCambios2();
});
function ComprobarCambios2() {
    fetch('/TipoCambio/ExisteTipoCambioParaFechaActual')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al comprobar el tipo de cambio para la fecha actual.');
            }
            return response.json();
        })
        .then(data => {
            if (data.existeTipoCambio) {
               
                window.location.href = '/Facturacion.html';
                
            } else {
            }
        })
        .catch(error => {
            // Manejo de errores si es necesario
            console.error(error.message);

            // Muestra un mensaje de error al usuario
            alert('Hubo un error al comprobar el tipo de cambio. Por favor, inténtalo nuevamente más tarde.');
        });
}
//////////////////////////////////////////////////////////////////////
////////////////////Metodo Crear/////////////////////////////////////
////////////////////////////////////////////////////////////////////
//#region Crear Cambio del dia//////////////////////////////
function GuardarCambio() {
    ///////Captura Del Cambio//////
    let CambioImput1 = document.getElementById("cambioInput").value;
    let fechaActual = new Date().toISOString().split('T')[0];
const Nuevocambio={
    precioCambio : Number(CambioImput1),
    fechaC: fechaActual,
};
    ///////////Metod Fetch/////////
    fetch('/TipoCambio/Create/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(Nuevocambio)
        }).then(response => response.json())
        .then(data => {
            if (data.success === true) {
                alert('Cambio  registrado exitosamente');
            } else {
                alert('Error al registrar cambio:', data.message);
            }
        })
        .catch(error => console.error('Error:', error));

};
//#endregion