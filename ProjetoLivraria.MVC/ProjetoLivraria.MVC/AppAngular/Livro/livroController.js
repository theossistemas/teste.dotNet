
bibliotecaApp.controller('livroController', function ($scope, livroService) {


    carregarlivros();

    
        function carregarlivros() {
            var listarlivros = livroService.getLivrosOrdenados();

            listarlivros.then(function (d) {
                
                $scope.ListaLivros = d.data;
            },
                function (e) {
                    alert("Ocorreu um erro ao tentar listar todos os livros!");
                });
        }


        
        $scope.SalvarLivro = function () {

          
                var livro = {
                    IdLivro: $scope.IdLivro,
                    Nome: $scope.Nome,
                    Editora: $scope.Editora,
                    Autor: $scope.Autor,
                    Ano: $scope.Ano,
                    Categoria: $scope.Categoria
                };

                var adicionarInfos = livroService.SalvarLivro(livro);

                adicionarInfos.then(function (d) {
                    $scope.MostrarForm = false;
                    carregarlivros();
                    $scope.limparDados();
                },
                    function (e) {
                        alert("Ocorreu um erro ao tentar salvar o item!");
                    });
            
        }


        $scope.excluirLivro = function (IdLivro) {

            var excluirInfos = livroService.excluirLivroId(IdLivro);
                excluirInfos.then(function (d) {
                    carregarlivros();
                },
                function (e,status) {
                    alert("Erro ao excluir livro!");
                });
        }

        $scope.ExibirForm = function (exibe)
        {
            $scope.MostrarForm = exibe;
        }

        $scope.PreEditar = function (livro) {
            $scope.PreencheLivro(livro);
            $scope.MostrarForm = true;

        }


        $scope.PreencheLivro = function (livro) {

            $scope.IdLivro = livro.IdLivro;
            $scope.Nome = livro.Nome;
            $scope.Editora = livro.Editora;
            $scope.Autor = livro.Autor;
            $scope.Ano = livro.Ano;
            $scope.Categoria = livro.Categoria;
           
        }


      
    
        $scope.limparDados = function () {
            $scope.IdLivro = "";
            $scope.Nome = "";
            $scope.Editora = "";
            $scope.Autor = "";
            $scope.Ano = "";
            $scope.Categoria = "";
            $scope.MostrarForm = false; 
        }
    
});