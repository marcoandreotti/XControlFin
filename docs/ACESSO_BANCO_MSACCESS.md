# 📖 Manual de Acesso ao Banco (MS Access / Jet)

Este manual descreve como inicializar e gerenciar o banco de dados MS Access para o projeto **XControlFin**.

## 🛠️ Como criar e executar o script MS Access

O script de inicialização está localizado em [init-auth-msaccess.sql](file:///c:/Users/marco/source/repos/XControlFin/scripts/init-auth-msaccess.sql).

### Opção 1: Via Microsoft Access (Interface Gráfica)
1. Abra o **Microsoft Access**.
2. Crie um novo banco de dados vazio chamado `xcontrolfin.accdb` na raiz do projeto.
3. Clique na guia **Criar (Create)** e depois em **Design da Consulta (Query Design)**.
4. Feche o assistente para mostrar tabelas se ele aparecer.
5. Mude a visualização da consulta para o modo SQL clicando no botão **SQL** (ou clicando com o botão direito na aba da consulta e selecionando **Exibição SQL**).
6. Como o MS Access não suporta a execução de múltiplos comandos em lote com delimitadores padrão via GUI, você deve executar cada comando individualmente:
   - Copie e cole um comando `CREATE TABLE` por vez e clique no botão **Executar (Run)** com o símbolo de exclamação vermelho.
   - Faça o mesmo com as instruções de inserção semente (`INSERT INTO`).

### Opção 2: Execução Automática pelo EF Core
Quando a aplicação inicia, o método `EnsureCreatedAsync()` no arquivo [Program.cs](file:///c:/Users/marco/source/repos/XControlFin/src/xControlFin.Api/Program.cs) cria a estrutura de tabelas automaticamente se o provedor estiver configurado como `MSAccess` e o banco não existir ou estiver vazio.

---

## 🔧 Configurando a Aplicação para usar MS Access

No arquivo [appsettings.json](file:///c:/Users/marco/source/repos/XControlFin/src/xControlFin.Api/appsettings.json):
1. Defina a propriedade `DatabaseProvider` como `MSAccess` (ou `Jet`):
   ```json
   "DatabaseProvider": "MSAccess"
   ```
2. A aplicação utilizará a string de conexão configurada na chave `MSAccessConnection`:
   ```json
   "MSAccessConnection": "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=xcontrolfin.accdb"
   ```
3. **Nota de Dependência**: A execução com MS Access exige que o driver OLE DB correspondente (`Microsoft.ACE.OLEDB.12.0` ou similar) esteja instalado no sistema operacional.
