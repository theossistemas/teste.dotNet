const uri = 'api/livros';
let todos = null;
function getCount(data) {
    const el = $('#counter');
    let nome = 'to-do';
    if (data) {
        if (data > 1) {
            nome = 'to-dos';
        }
        el.text(data + ' ' + nome);
    } else {
        el.html('No ' + nome);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#todos').empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                $('<tr>' +
                    '<td>' + item.nome + '</td>' +
                    '<td>' + item.autor + '</td>' +
                    '<td><button onclick="editItem(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + item.id + ')">Delete</button></td>' +
                   '</tr>').appendTo($('#todos'));
            });

            todos = data;
        }
    });
}

function addItem() {
    const item = {
        'nome': $('#add-nome').val(),
        'autor': $('#add-autor').val(),
        'datafabricacao': $('#add-autor').val(),
        'ano': $('#add-ano').val()
    };
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        headers: {"Authorization": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1MzQ3Mjk4MjcsImV4cCI6MTUzNTMzNDYyNywiaWF0IjoxNTM0NzI5ODI3fQ.UeKh07ZXaam0IYk6FFKigah-4DGmx5E5nrJHfKt9nVI"},        
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-nome').val('');
        }
    });
}

function Registrar() {
    const item = {
        'Nome': $('#RUsername').val(),
        'Senha': $('#RPassword').val(),
        'FirstName': $('#FirstName').val(),
        'LastName': $('#RLastName').val()

    };
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: 'api/Users',
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#Username').val('');
        }
    });
}

function Login() {
    const userDto = {
        'username': $('#Username').val(),
        'password': $('#Password').val(),
        'firstname': '',
        'lastname': ''
    };
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: 'api/Users/authenticate',
        data: userDto,
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#Username').val('');
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $('#edit-nome').val(item.nome);
            $('#edit-id').val(item.id);
            $('#edit-autor').val(item.autor);
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const item = {
        'nome': $('#edit-nome').val(),
        'autor': $('#edit-autor').val(),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}