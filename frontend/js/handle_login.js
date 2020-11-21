/*
    admin = {
        id: number,
        name: string,
        token: string,
        expires: timestamp
    }
*/

function logout(){
    ls_clear();
    location.reload()
}

function setAdmin(id, name, token){
    ls_save('admin', {
        id:id, 
        name:name,
        token:token, 
        expires:Date.now() + 3600000
    });
    location.reload();
}

var admin;

function HandleLogin(){
    if(admin ==  undefined){
        if(!ls_isset('admin')){
            admin = null;
            return;
        }

        admin = ls_retrieve('admin');
        
        if(Date.now() > admin.expires){
            logout()
            admin = null;
            return;
        }
    }
}

function isLogged(){
    return (admin != null && admin != undefined);
}

HandleLogin();

function btnLogin_click(){
    var usuario = document.getElementById('input-usuario').value;
    var senha = document.getElementById('input-senha').value;

    if(senha.trim() == "" || usuario.trim() == ""){
        alert('Preencha os campos Usuario e Senha para continuar')
        return
    }

    api_login(
        usuario, 
        senha,
        function(response){
            setAdmin(response.content.user.id, response.content.user.name, response.content.token);
        },
        function(response){
            if(response.status == 401){
                alert('Login ou Senha incorretos')
            }else{
                alert("Ocorreu um erro desconhecido executando a operação")
            }
        }
    );
    
}