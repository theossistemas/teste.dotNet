var bibliotecaApp;

(function () {

    bibliotecaApp = angular.module('bibliotecaApplication', ['ngRoute']);


    bibliotecaApp.config(function ($routeProvider, $httpProvider) {

        $routeProvider
            .when('/Livro', {
                templateUrl: 'Livro/Livro.html',
                controller: 'livroController'
            })
            .when('/', {
                templateUrl: 'Livro/Livro.html',
                controller: 'livroController'
            });
    });

})();