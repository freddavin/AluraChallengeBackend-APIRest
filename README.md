# Aluraflix - Alura Challenge Backend - API Rest
API em desenvolvimento para a **Alura Challenge de BackEnd** com o objetivo de implementar as funcionalidades do backend para uma plataforma de compartilhamento de vídeos. O desafio é divido em 3 semanas e em cada uma é divulgada novas regras de negócio e features.

## Tecnologias Utilizadas
- C#
- ASP .NET Core
- MySQL

### Bibliotecas
- AutoMapper
- FluentResults
- Entity Framework Core

## Desafios
### Semana 1 - Início do projeto:

Após alguns testes com protótipos feitos pelo time de UX de uma empresa, foi requisitada a primeira versão de uma plataforma para compartilhamento de vídeos. A plataforma deve permitir ao usuário montar playlists com links para seus vídeos preferidos, separados por categorias.

Os times de frontend e UI já estão trabalhando no layout e nas telas. Para o backend, as principais funcionalidades a serem implementadas são:
1. API com rotas implementadas segundo o padrão REST;
2. Validações feitas conforme as regras de negócio;
3. Implementação de base de dados para persistência das informações;
4. Serviço de autenticação para acesso às rotas `GET`, `POST`, `PUT` e `DELETE`.

### Semana 2 - Implentação de nova feature:

Depois de alguns testes com usuários, foi definido que a próxima feature a ser desenvolvida nesse projeto é a divisão dos vídeos por categoria, para melhorar a experiência de organização da lista de vídeos pelo usuário.

Dividimos a implementação dessa feat da seguinte forma:
1. Adicionar `categorias` e seus campos na base de dados;
2. Rotas CRUD para `/categorias`;
3. Incluir campo `categoriaId` no modelo `video`; 
4. Escrever os testes necessários.
