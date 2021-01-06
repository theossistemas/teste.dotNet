// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    var app = angular.module("app", []);

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

    app.controller("loginControler", ["$scope", function (scope) {


    }]);
})()