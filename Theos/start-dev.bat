@echo off
echo 🚀 Iniciando ambiente de desenvolvimento TheosLivraria...

REM Verificar se o Docker está rodando
docker info >nul 2>&1
if %errorlevel% neq 0 (
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
if %errorlevel% equ 0 (
    echo ✅ SQL Server está rodando!
    echo 📊 Porta: localhost:1433
    echo 🗄️  Banco: TheosLivraria
    echo 👤 Usuário: sa
    echo 🔑 Senha: TheosLivraria@2025
) else (
    echo ❌ Erro ao iniciar SQL Server
    pause
    exit /b 1
)

echo.
echo 🎯 Próximos passos:
echo 1. Execute as migrações: dotnet ef database update
echo 2. Inicie a API: dotnet run
echo 3. Acesse: http://localhost:5181/swagger
echo.
echo 🛑 Para parar: docker-compose down
pause
