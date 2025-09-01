# 🚀 TheosLivraria - Ambiente de Desenvolvimento

## 📋 Pré-requisitos

- **.NET 8.0 SDK**
- **Docker Desktop** (Windows/Mac)
- **Git**

## 🐳 Configuração do Banco de Dados

### **Início Rápido (Recomendado)**

#### **Para Mac/Linux:**
```bash
chmod +x start-dev.sh
./start-dev.sh
```

#### **Para Windows:**
```cmd
start-dev.bat
```

### **Configuração Manual**

1. **Iniciar o banco:**
```bash
docker-compose up -d sqlserver
```

2. **Verificar status:**
```bash
docker ps
```

3. **Parar o banco:**
```bash
docker-compose down
```

## 🗄️ Detalhes do Banco

- **Servidor**: `localhost:1433`
- **Banco**: `TheosLivraria`
- **Usuário**: `sa`
- **Senha**: `TheosLivraria@2025-`
- **TrustServerCertificate**: `true`

## 🔧 Configuração da API

### **1. Restaurar dependências:**
```bash
dotnet restore
```

### **2. Executar migrações:**
```bash
cd TheosLivraria.API
dotnet ef database update
```

### **3. Iniciar a API:**
```bash
dotnet run
```

### **4. Acessar:**
- **Swagger**: http://localhost:5181/swagger
- **API**: http://localhost:5181

## 📁 Estrutura do Projeto

```
TheosLivraria/
├── TheosLivraria.API/          # Controllers e configurações
├── TheosLivraria.Domain/        # Entidades e interfaces
├── TheosLivraria.Infra/         # Contexto e repositórios
├── docker-compose.yml     # Configuração Docker
├── docker.env            # Variáveis de ambiente
├── start-dev.sh          # Script Mac/Linux
├── start-dev.bat         # Script Windows
└── README-DEV.md         # Este arquivo
```

## 🐳 Configuração Docker Completa

### **Arquivo docker-compose.yml**
```yaml
version: '3.8'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: TheosLivraria-sqlserver
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=TheosLivraria@2025-
      - MSSQL_PID=Developer
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql
      - ./sql:/docker-entrypoint-initdb.d
    networks:
      - TheosLivraria-network
    restart: unless-stopped

volumes:
  sqlserver_data:
    driver: local

networks:
  TheosLivraria-network:
    driver: bridge
```

### **Arquivo docker.env**
```bash
# Database Configuration
DB_SERVER=localhost,1433
DB_NAME=TheosLivraria
DB_USER=sa
DB_PASSWORD=TheosLivraria@2025-
DB_TRUST_SERVER_CERTIFICATE=true

# JWT Configuration
JWT_VALID_AUDIENCE=http://localhost:7043
JWT_VALID_ISSUER=http://localhost:5043
JWT_SECRET_KEY=Minha@Super#Secreta&Chave*Privada!2022
JWT_TOKEN_VALIDITY_MINUTES=30
JWT_REFRESH_TOKEN_VALIDITY_MINUTES=60

# ImageKit Configuration
IMAGEKIT_URL=https://upload.imagekit.io
IMAGEKIT_PUBLIC_KEY=public_SusJ1E6taqUMMdxP+AhitH7XiMA=
IMAGEKIT_PRIVATE_KEY=private_Fqy1BgWPArWiCcB2K8ExBeLsOTs=
IMAGEKIT_FOLDER=TheosLivraria
```

### **Script start-dev.sh (Mac/Linux)**
```bash
#!/bin/bash

echo "🚀 Iniciando ambiente de desenvolvimento TheosLivraria..."

# Verificar se o Docker está rodando
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker não está rodando. Por favor, inicie o Docker Desktop."
    exit 1
fi

# Parar containers existentes (se houver)
echo "🛑 Parando containers existentes..."
docker-compose down

# Iniciar o banco de dados
echo "🐳 Iniciando SQL Server..."
docker-compose up -d sqlserver

# Aguardar o SQL Server inicializar
echo "⏳ Aguardando SQL Server inicializar..."
sleep 15

# Verificar se o SQL Server está rodando
echo "🔍 Verificando status do SQL Server..."
if docker ps | grep -q "TheosLivraria-sqlserver"; then
    echo "✅ SQL Server está rodando!"
    echo "📊 Porta: localhost:1433"
    echo "🗄️  Banco: TheosLivraria"
    echo "👤 Usuário: sa"
    echo "🔑 Senha: TheosLivraria@2025-"
else
    echo "❌ Erro ao iniciar SQL Server"
    exit 1
fi

echo ""
echo "🎯 Próximos passos:"
echo "1. Execute as migrações: dotnet ef database update"
echo "2. Inicie a API: dotnet run"
echo "3. Acesse: http://localhost:5181/swagger"
echo ""
echo "🛑 Para parar: docker-compose down"
```

### **Script start-dev.bat (Windows)**
```batch
@echo off
echo 🚀 Iniciando ambiente de desenvolvimento TheosLivraria...

REM Verificar se o Docker está rodando
docker info >nul 2>&1
if errorlevel 1 (
    echo ❌ Docker não está rodando. Por favor, inicie o Docker Desktop.
    pause
    exit /b 1
)

REM Parar containers existentes (se houver)
echo 🛑 Parando containers existentes...
docker-compose down

REM Iniciar o banco de dados
echo 🐳 Iniciando SQL Server...
docker-compose up -d sqlserver

REM Aguardar o SQL Server inicializar
echo ⏳ Aguardando SQL Server inicializar...
timeout /t 15 /nobreak >nul

REM Verificar se o SQL Server está rodando
echo 🔍 Verificando status do SQL Server...
docker ps | findstr "TheosLivraria-sqlserver" >nul
if errorlevel 1 (
    echo ❌ Erro ao iniciar SQL Server
    pause
    exit /b 1
) else (
    echo ✅ SQL Server está rodando!
    echo 📊 Porta: localhost:1433
    echo 🗄️  Banco: TheosLivraria
    echo 👤 Usuário: sa
    echo 🔑 Senha: TheosLivraria@2025-
)

echo.
echo 🎯 Próximos passos:
echo 1. Execute as migrações: dotnet ef database update
echo 2. Inicie a API: dotnet run
echo 3. Acesse: http://localhost:5181/swagger
echo.
echo 🛑 Para parar: docker-compose down
pause
```

## 🚨 Solução de Problemas

### **Docker não está rodando:**
- Verifique se o Docker Desktop está iniciado
- Reinicie o Docker Desktop se necessário

### **Porta 1433 já está em uso:**
```bash
# Verificar o que está usando a porta
lsof -i :1433  # Mac/Linux
netstat -an | findstr 1433  # Windows

# Parar containers
docker-compose down
```

### **Erro de conexão com o banco:**
- Aguarde 15-20 segundos após iniciar o container
- Verifique se o container está rodando: `docker ps`
- Teste a conexão: `docker exec -it TheosLivraria-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TheosLivraria@2025- -C`

### **Erro de certificado SSL:**
```bash
# Use a flag -C para confiar no certificado do servidor
docker exec -it TheosLivraria-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TheosLivraria@2025- -C
```

### **Problemas com .NET:**
```bash
# Verificar versão do .NET
dotnet --version

# Se necessário, configurar variáveis de ambiente
export DOTNET_ROOT="/opt/homebrew/opt/dotnet@8/libexec"
export PATH="/opt/homebrew/opt/dotnet@8/bin:$PATH:/Users/joseguilherme/.dotnet/tools"

# Para Mac com Homebrew
brew install dotnet@8
```

## 🔄 Comandos Úteis

```bash
# Ver logs do SQL Server
docker logs TheosLivraria-sqlserver

# Acessar o SQL Server (com certificado confiável)
docker exec -it TheosLivraria-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TheosLivraria@2025- -C

# Backup do banco (se necessário)
docker exec TheosLivraria-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TheosLivraria@2025- -C -Q "BACKUP DATABASE TheosLivraria TO DISK = '/var/opt/mssql/backup.bak'"

# Restaurar banco (se necessário)
docker exec TheosLivraria-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TheosLivraria@2025- -C -Q "RESTORE DATABASE TheosLivraria FROM DISK = '/var/opt/mssql/backup.bak'"

# Ver containers em execução
docker ps

# Ver todos os containers (incluindo parados)
docker ps -a

# Parar todos os containers
docker stop $(docker ps -q)

# Remover containers parados
docker container prune

# Remover volumes não utilizados
docker volume prune
```

## 👥 Trabalhando em Equipe

### **Para novos desenvolvedores:**
1. Clone o repositório
2. Execute `./start-dev.sh` (Mac/Linux) ou `start-dev.bat` (Windows)
3. Execute as migrações: `dotnet ef database update`
4. Inicie a API: `dotnet run`

### **Compartilhando dados:**
- Os dados ficam persistidos no volume Docker
- Para resetar o banco: `docker-compose down -v` (⚠️ **CUIDADO**: apaga todos os dados)

### **Sincronização de configurações:**
- Todos os desenvolvedores usam o mesmo `docker-compose.yml`
- As variáveis de ambiente estão no `docker.env`
- Os scripts de automação garantem consistência

## 🧪 Testando a API

### **Endpoints disponíveis:**
- **POST** `/api/Auth/register` - Criar um livro
- **UPDATE** `/api/Auth/login` - Editar um livro
- **DELETE** `/api/Auth/refresh` - Excluir um livro


## 📞 Suporte

Se encontrar problemas:
1. Verifique se o Docker está rodando
2. Execute `docker-compose down` e depois `docker-compose up -d sqlserver`
3. Aguarde 15-20 segundos para o SQL Server inicializar
4. Verifique os logs: `docker logs TheosLivraria-sqlserver`
5. Teste a conexão com o banco usando os comandos acima
6. Verifique se as variáveis de ambiente estão configuradas corretamente

### **Logs úteis para debug:**
```bash
# Logs do Docker
docker logs TheosLivraria-sqlserver

# Logs da API .NET
dotnet run --verbosity detailed

# Status dos containers
docker ps -a
```
