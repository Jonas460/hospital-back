# Aplicação de Gerenciamento de Fichas de Pacientes para Hospital

Esta é uma aplicação web desenvolvida para um hospital gerenciar as fichas de seus pacientes. A aplicação permite que médicos criem e editem fichas de pacientes, enquanto os pacientes podem visualizar suas próprias fichas.

## Requisitos
- Backend: ASP.NET Core com EF Core
- Frontend: React.js
- Sistema de autenticação com login e cadastro de usuários
- Papéis de usuário: Cada usuário possui um papel que indica se ele é um paciente ou médico.
- Pacientes podem visualizar suas próprias fichas.
- Médicos podem criar, editar e excluir fichas.
- As fichas médicas incluem as seguintes informações:
  - Foto do paciente (*upload de arquivo*)
  - Nome completo
  - CPF válido (*validação*)
  - Número de celular (campo de entrada com formato *E.164 normalizado*)
  - Endereço (campo opcional)

## Como utilizar
1. Clone este repositório para a sua máquina local.
2. Configuração do backend:
   - Navegue até o diretório "backend".
   - Instale as dependências necessárias executando o comando `npm install`.
   - Inicie o servidor backend executando o comando `npm start`.
3. Configuração do frontend:
   - Navegue até o diretório "frontend".
   - Instale as dependências necessárias executando o comando `npm install`.
   - Inicie o servidor de desenvolvimento executando o comando `npm start`.
4. Acesse a aplicação em seu navegador da web através do endereço `http://localhost:3000`.
5. Cadastre-se como paciente ou médico usando o formulário de registro fornecido.
6. Faça login utilizando suas credenciais.
7. Como paciente, você pode visualizar suas próprias fichas médicas.
8. Como médico, você pode criar novas fichas, editar fichas existentes e excluir fichas.

Sinta-se à vontade para explorar a aplicação e fornecer feedback ou sugestões de melhoria.

Observação: Esta aplicação é apenas para fins de demonstração e não contém dados reais de pacientes.

## Tecnologias Utilizadas
- ASP.NET Core
- EF Core
- React.js
- HTML
- CSS
- JavaScript
