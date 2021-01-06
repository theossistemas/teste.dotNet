// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    var app = angular.module("app", ['ngCookies']);

    app.controller("livroController", ["$scope", "$http", function (scope, http) {

        scope.take = 10;
        scope.skip = 0;
        scope.busca = "";
        scope.tema = "";

        scope.buscarLivros = function () {
            http.get("api/livro/" + scope.take + "/" + scope.skip + "?busca=" + scope.busca + "&tema=" + scope.tema)
                .then(function (r) {
                    scope.livros = r.data.livrosModel;
                    scope.quantidade = r.data.quantidadeLivros;
                })
        };

        scope.buscarLivros();
    }]);

    app.controller("loginController", ["$scope", "$http", "$cookies", function (scope, http, cookies) {
        scope.livro = {};
        scope.livro.autores = [];
        scope.livro.temas = [];

        scope.logar = function () {
            http.post("../api/usuario/logar", scope.usuario)
                .then(function (r) {
                    cookies.put("token", r.data.token);
                    scope.logado = true;
                });
        };

        scope.adicionarAutor = function () {
            scope.livro.autores.push({ nome: scope.autorAdicionar });
            scope.autorAdicionar = "";
        };

        scope.adicionarTema = function () {
            scope.livro.temas.push(scope.temaAdicionar);
            scope.temaAdicionar = "";
        };

        scope.cadastrarLivro = function () {
            http.put("../api/livro", scope.livro, {
                headers: {
                    'Authorization': 'Bearer ' + cookies.get("token")
                }
            });
        }
    }]);
})()