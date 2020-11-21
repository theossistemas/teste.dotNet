var apiUrl = "https://localhost:44378/"

function get_books(callback, error_callback){
    var url = apiUrl + 'books';
    GET(url, callback, error_callback);
}

function get_book(id, callback, error_callback){
    var url = apiUrl + 'books/'+id;
    GET(url, callback, error_callback);
}

function GET(url, callback, error_callback){
    var settings = {
        "url": url,
        "method": "GET",
        "timeout": 2000,
        error: error_callback
    };

    $.ajax(settings).done(function (response) { 
        callback(response)
    });
}

function POST(url, type, token, body, callback, error_callback){
    var settings = {
        "url": url,
        "method": type,
        "timeout": 2000,
        "headers": {
            "Authorization": "Bearer "+token,
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(body),
        error: error_callback
    };

    $.ajax(settings).done(function (response) { 
        callback(response)
    });
}

function default_error_callback(response){
    if(response.statusText == 'timeout'){
        alert('Conexão com o servidor perdida, por favor tente novamente')
    }else{//check 401. print 400
        console.log(response)
    }
}

function delete_book(id, token, callback, error_callback){
    var url = apiUrl + 'books/'+id;

    POST(
        url, 
        "DELETE", 
        token, 
        null,
        callback, 
        error_callback
    );
}

function insert_book(token, book, callback, error_callback){
    var url = apiUrl + 'books';

    POST(
        url, 
        "POST", 
        token, 
        book,
        callback, 
        error_callback
    );
}

function update_book(id, token, book, callback, error_callback){
    var url = apiUrl + 'books/' + id;

    POST(
        url, 
        "PUT", 
        token, 
        book,
        callback, 
        error_callback
    );
}

function api_login(usuario, senha, callback, error_callback){
    var url = apiUrl + 'auth';
    
    POST(
        url, 
        "POST", 
        null, 
        {
            login:usuario, 
            password:senha
        }, 
        callback, 
        error_callback
    );
}