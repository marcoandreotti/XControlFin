# 📖 Manual de Acesso ao Banco (SQLite)

Este manual descreve como inicializar e gerenciar o banco de dados SQLite para o projeto **XControlFin**.

## 🛠️ Como criar e executar o script SQLite

O script de inicialização está localizado em [init-auth-sqlite.sql](file:///c:/Users/marco/source/repos/XControlFin/scripts/init-auth-sqlite.sql).

### Opção 1: Via SQLite CLI (Linha de Comando)
1. Certifique-se de ter o `sqlite3` instalado no seu sistema.
2. Abra o terminal (PowerShell ou Bash) na raiz do projeto e execute:
   ```bash
   sqlite3 xcontrolfin.db < scripts/init-auth-sqlite.sql
   ```
3. O comando gerará o arquivo `xcontrolfin.db` na raiz do seu projeto com todas as tabelas criadas e os dados iniciais carregados.

### Opção 2: Via DB Browser for SQLite (Interface Gráfica)
1. Baixe e instale o [DB Browser for SQLite](https://sqlitebrowser.org/).
2. Abra a aplicação e clique em **Open Database** (ou crie um novo banco de dados chamado `xcontrolfin.db`).
3. Vá para a aba **Execute SQL**.
4. Copie o conteúdo do arquivo [init-auth-sqlite.sql](file:///c:/Users/marco/source/repos/XControlFin/scripts/init-auth-sqlite.sql) e cole no editor.
5. Clique no ícone de "Play" para executar todas as instruções.
6. Clique em **Write Changes** na barra de ferramentas para salvar as alterações físicas no banco.

---

## 🔧 Configurando a Aplicação para usar SQLite

No arquivo [appsettings.json](file:///c:/Users/marco/source/repos/XControlFin/src/xControlFin.Api/appsettings.json):
1. Altere a propriedade `DatabaseProvider` para `SQLite`:
   ```json
   "DatabaseProvider": "SQLite"
   ```
2. A aplicação utilizará a string de conexão configurada na chave `SQLiteConnection`.
