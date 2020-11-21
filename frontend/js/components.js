function header(){
    document.write(
`
<header>
    <!-- Fixed navbar -->
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
    
    <a class="navbar-brand" href="https://getbootstrap.com/docs/4.4/examples/sticky-footer-navbar/#">
        <!-- Original nav content height = 40px -->
        <!-- Theos Logo proportion: 105w x 100h -->
        <img src="./imgs/theos-transparente.png" width="52" height="50">
    </a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse"
        aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarCollapse">
        <ul class="navbar-nav mr-auto">
        <li class="nav-item `+(getPageName()=='index.html'?'active':'')+`">
            <a class="nav-link" href="./index.html">Livros</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="https://eclesial.theos.com.br/#/login" target="_blank">Eclesial</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="https://github.com/theossistemas/teste.dotNet" target="_blank">Requisitos</a>
        </li>
        </ul>
        `
        + (isLogged() ? 
        `
        <span class="text-white mr-4">Olá, `+admin.name.split(' ')[0]+`</span>
        <a href="#" class="btn btn-outline-danger my-2 my-sm-0" onclick="logout()">Logout</a>
        `
        : `<a href="./login.html" class="btn btn-outline-success my-2 my-sm-0" data-toggle="modal" data-target="#loginModal">Login</a>`) +
        `
    </div>
    </nav>
</header>
`
    );
}

function loginModal(){
    document.write(
`
<!-- Modal -->
<div class="modal fade" id="loginModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Login</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="input-group mb-3">
                <input type="text" class="form-control" placeholder="Usuario" aria-label="Usuario"
                    aria-describedby="basic-addon1" required id="input-usuario">
                </div>
                <div class="input-group mb-3">
                <input type="password" class="form-control" placeholder="Senha" aria-label="Senha"
                    aria-describedby="basic-addon1" required id="input-senha">
                </div>
            </div>
            <div class="modal-footer" style="justify-content: flex-start !important;">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-success" onclick="btnLogin_click()" id="btnLogin">Login</button>
            </div>
        </div>
    </div>
</div>
`
    );
}

function editBookModal(){
    document.write(
`
<!-- Modal -->
<div class="modal fade" id="editBookModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
  aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 id="editBookModal-titulo" class="modal-title" id="exampleModalLongTitle">### Livro</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="input-group mb-3">
          <input type="text" class="form-control" placeholder="Nome" aria-label="Nome" aria-describedby="basic-addon1"
            required id="input-nome">
        </div>
        <div class="input-group mb-3">
          <input type="text" class="form-control" placeholder="Autor" aria-label="Autor" aria-describedby="basic-addon1"
            required id="input-autor">
        </div>
        <div class="form-row">
          <div class="col">
            <div class="input-group mb-3">
              <input type="number" class="form-control" placeholder="Lançamento" aria-label="Lançamento"
                aria-describedby="basic-addon1" required id="input-lancamento">
            </div>
          </div>
          <div class="col">
            <div class="input-group mb-3">
              <div class="input-group-prepend">
                <span class="input-group-text" id="basic-addon1">R$</span>
              </div>
              <input type="number" class="form-control" placeholder="Preço" aria-label="Preço"
                aria-describedby="basic-addon1" required id="input-preco">
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer" style="justify-content: flex-start !important;">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-success" onclick="alert('error')" id="btnSalvar">Salvar</button>
      </div>
    </div>
  </div>
</div>
`
    );
}