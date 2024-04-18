

  const BtnIniciarSesion= document.getElementById("IniciarSesion");
  
  BtnIniciarSesion.addEventListener("click", function(){

    
    let nombreUsuario = document.getElementById("floatingInput");
    let contraseñaU = document.getElementById("floatingPassword");
    
    if (nombreUsuario && contraseñaU) {
        const usuario = {
            usuario1: nombreUsuario.value,
            contraseña: contraseñaU.value
        };
    
        console.log(nombreUsuario.value, contraseñaU.value);
        fetch('/Usuario/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(usuario)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Nombre de usuario o contraseña incorrectos');
            }
            return response.json();
        })
        .then(data => {
            if (data.message) {
                console.log('Autenticación exitosa:', data.message);
                window.location.href = 'Inicio.html';
            } else {
                throw new Error(data.message);
            }
        })
        .catch(error => {
            console.error('Error de autenticación:', error.message);
            alert('Error de autenticación: Hubo un problema durante la autenticación.');
        });
    } else {
        console.error('Elementos no encontrados en el DOM.');
    }
  });
  document.getElementById("olvidoContrasenaLink").addEventListener("click", function() {
    window.location.href = "RestablecerContras.html";
});
