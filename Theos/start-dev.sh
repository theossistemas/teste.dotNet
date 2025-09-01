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
    echo "🔑 Senha: TheosLivraria@2025"
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
