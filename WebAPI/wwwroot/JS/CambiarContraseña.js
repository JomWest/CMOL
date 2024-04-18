const txtUsuario = document.getElementById("usernameInput");
const txtAntiguaContra = document.getElementById("oldPasswordInput");
const txtNuevaContra = document.getElementById("newPasswordInput");

const btnActualizar = document.getElementById("UpdatePas");

btnActualizar.addEventListener("click", function(){
    if(txtUsuario.value == "" || txtAntiguaContra.value == "" || txtNuevaContra.value == ""){
        alert("Ninguno de los campos puede quedar vacío");
        return;
    }

    if(txtNuevaContra.value.trim().length < 8){
        alert("La nueva contraseña debe contener como mínimo 8 caracteres");
        return;
    }

    if(txtNuevaContra.value.trim().length > 30){
        alert("La nueva contraseña no puede contener más de 30 caracteres");
        return;
    }

    cambiarContraseña(txtUsuario.value.trim(), txtAntiguaContra.value.trim(), txtNuevaContra.value.trim());
    txtUsuario.value = "";
    txtAntiguaContra.value = "";
    txtNuevaContra.value = "";
})

  function cambiarContraseña( nombreUsuario, contraseñaActual, nuevaContraseña) {
    const data = {
        usuario1: nombreUsuario,
        contraseña: contraseñaActual,
        nuevaContraseña: nuevaContraseña
      };
  
    fetch('/Usuario/CambiarContraseña', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
    .then(response => {
      if (!response.ok) {
        throw new Error(`Error en la solicitud: ${response.status}`);
      }
      txtNuevaContra.value = "";
      txtAntiguaContra.value = "";
      txtUsuario.value = "";
      return response.text();
    })
    .then(data => {
      
      alert(data);
    })
    .catch(error => {
      
      console.error('Error:', error);
      alert('Error al cambiar la contraseña. Verifique los datos proporcionados.');
    });
  }