const btnEnviar = document.getElementById("btnEnviar");

const txtCorreo = document.getElementById("correoImput");

btnEnviar.addEventListener("click", function(){

    if(txtCorreo.value.trim() == ""){
        alert("Ingrese una dirección de correo electrónico");
return;
    }

    if(EmailValido(txtCorreo.value.trim())){
    }else{
        alert("Ingrese una dirección de correo electrónico válida");
        return;
    }

    let nuevaContraseña = generarContraseña();

    enviarCorreoElectronico(txtCorreo.value.trim(), nuevaContraseña);
})

function enviarCorreoElectronico(destinatario, nuevaContraseña) {
    const correoData = {
      destinatario: destinatario,
      nuevaContraseña: nuevaContraseña
    };
  
    fetch('/Email/EnviarContraseña', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(correoData)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Error en la solicitud: ${response.status}');
        }
        return response.text();
      })
      .then(data => {
        alert(data); 
      })
      .catch(error => {
        console.error('Error:', error);
        alert('El correo electrónico ingresado no pertenece a ningún usuario.');
      });
  }

  // Función para la generación de una contraseña alfanumérica de 10 caracteres
  function generarContraseña() {
    const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let contraseña = '';
  
    for (let i = 0; i < 10; i++) {
      const indice = Math.floor(Math.random() * caracteres.length);
      contraseña += caracteres.charAt(indice);
    }
  
    return contraseña;
  }

  // Función para verificar la validez de un correo electrónico
  function EmailValido(email) {
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}