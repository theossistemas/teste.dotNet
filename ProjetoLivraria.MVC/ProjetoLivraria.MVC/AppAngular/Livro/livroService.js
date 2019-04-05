
bibliotecaApp.service('livroService', function ($http) {


   
    this.getLivrosOrdenados = function () {

        return $http.get("/api/Livro");
    }

    
    this.SalvarLivro = function (livro) {

        var request = $http({
            method: 'post',
            url: '/api/Livro',
            data: livro
        });

        return request;
    }

    this.excluirLivroId = function (id) {

        return $http.delete('/api/Livro/'+id);
    }
   
});