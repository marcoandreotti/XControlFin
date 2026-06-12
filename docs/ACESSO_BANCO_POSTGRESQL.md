# 📖 Manual de Acesso ao Banco (Postgres + PgAdmin)
## 🚀 Subindo os containers
- Certifique-se de ter o Docker e Docker Compose instalados.
- No diretório do projeto, execute:
```docker-compose up -d```
- Isso irá criar e iniciar os containers:
- xcontrolfin-db → Banco de dados Postgres 15
- xcontrolfin-pgadmin → Interface gráfica PgAdmin4
- Verifique se os containers estão rodando:
```docker ps```



## 🌐 Acessando o PgAdmin
- Abra o navegador e acesse:
```http://localhost:5050```
- Faça login com as credenciais definidas no docker-compose.yml:
- Email: admin@admin.com
- Senha: admin123

## 🔗 Registrando o servidor Postgres no PgAdmin
Após o login, você precisa adicionar o servidor manualmente:
- Clique em Add New Server.
- Preencha os campos:
- General → Name:
xcontrolfin-db (pode ser qualquer nome, apenas para identificação)
- Connection → Host name/address:
postgres
(esse é o nome do serviço definido no docker-compose, usado como hostname dentro da rede Docker)
- Connection → Port:
5432
- Connection → Username:
admin
- Connection → Password:
admin123
- Clique em Save.

## 🗄️ Banco disponível
- Banco criado automaticamente: xcontrolfin
- Você já pode criar tabelas, inserir dados e gerenciar o banco via PgAdmin.

## 🛑 Encerrando os containers
Quando terminar, pode parar os serviços com:
```docker-compose down```



## 👉 Esse manual pode ser colocado em um arquivo docs/ACESSO_BANCO.md e referenciado no README com um link:
[Manual de acesso ao banco](docs/ACESSO_BANCO.md)




