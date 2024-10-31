
# 📅 CalculadoraDataAPI

A **CalculadoraDataAPI** é uma API em .NET 8 que permite realizar operações de cálculo de datas, incluindo conversão entre calendários Juliano e Gregoriano, além de realizar operações de adição e subtração. Esta API é útil para aplicações que precisam de manipulação de datas complexas.

## ⚙️ Funcionalidades

- 🔄 **Conversão** entre os calendários Juliano e Gregoriano.
- ➕ ➖ **Cálculo** de datas com operações de soma e subtração.
- 📄 **Documentação Swagger** integrada para fácil consulta dos endpoints.

## 🛠️ Tecnologias Utilizadas

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-9.0-blue?style=flat&logo=csharp)
![Swagger](https://img.shields.io/badge/Swagger-UI-green?style=flat&logo=swagger)
![xUnit](https://img.shields.io/badge/Test-xUnit-lightgrey?style=flat&logo=xunit)

---

## 🚀 Como Usar

### `GET /api/v2/calculardata`

Calcula uma nova data com base em uma data inicial, operação (`+` ou `-`), e valor em minutos.

#### 📥 Parâmetros de Query

| Parâmetro   | Tipo   | Descrição                                     |
|-------------|--------|-----------------------------------------------|
| `data`      | string | Data inicial no formato `dd/MM/yyyy HH:mm`.   |
| `operacao`  | string | Operação matemática: `+` (soma) ou `-` (subtração). |
| `valor`     | long   | Valor em minutos a adicionar ou subtrair.     |

#### Exemplo de Requisição

```http
GET /api/v2/calculardata?data=14/03/2021 19:12&operacao=+&valor=3000
```

#### Exemplo de Resposta

```json
{
  "data": "16/03/2021 21:11"
}
```

#### 📋 Códigos de Resposta

- **200 OK**: Retorna a nova data calculada.
- **400 Bad Request**: Parâmetros fornecidos inválidos.
- **500 Internal Server Error**: Erro inesperado no servidor.

---

## 📂 Estrutura do Projeto

```plaintext
CalculadoraDataAPI/
│
├── CalculadoraDataAPI.sln               // Solução do projeto
├── Program.cs                            // Configuração e inicialização do projeto
├── Controllers/                          // Controladores da API
│   └── CalcularDataController.cs         // Controller principal para cálculo de datas
│
├── DTOs/                                 // Objetos de Transferência de Dados
│   └── DateRequestDto.cs                 // DTO para requisição de cálculo de data
│
├── Interfaces/                           // Interfaces para Injeção de Dependência
│   ├── IConversorCalendarioService.cs    // Interface do serviço de conversão de calendário
│   └── IDataCalculationService.cs        // Interface do serviço de cálculo de data
│
├── Services/                             // Lógica de Negócio
│   ├── ConversorCalendarioService.cs     // Implementação da conversão de calendários
│   └── DataCalculationService.cs         // Implementação do cálculo de data
│
├── Utilities/                            // Utilitários auxiliares
│   └── DateHelper.cs                     // Validações auxiliares de data
│
└── Tests/                                // Projeto de Testes Unitários
    ├── CalcularDataUnitTestController.cs // Testes unitários do controller
```

## ⚙️ Configuração do Ambiente

### Pré-requisitos

- **.NET SDK 8**: Certifique-se de que o SDK do .NET 8 está instalado em sua máquina.

### Passos

1. **Clone o Repositório:**
   ```bash
   git clone https://github.com/lucianaregi/CalculadoraDataAPI.git
   cd CalculadoraDataAPI
   ```

2. **Restaure as Dependências:**
   ```bash
   dotnet restore
   ```

3. **Compile e Execute:**
   ```bash
   dotnet run
   ```

4. **Acesse o Swagger para Testar a API:**
   ```
   http://localhost:5000/swagger
   ```

---

## 🧪 Testes

Este projeto utiliza **xUnit** e **Moq** para testes unitários.

### Executar Testes

Para executar os testes, utilize:

```bash
dotnet test
```

---

## 📈 Melhorias Futuras

- **🔒 Autenticação e Autorização**: Adicionar autenticação para segurança da API.
- **🌐 Suporte a Outros Calendários**: Expandir a conversão para outros sistemas de calendário.
- **⚡ Cache de Resultados**: Implementar cache para melhorar o desempenho em cálculos repetidos.

---

## 🤝 Contribuição

Contribuições são bem-vindas! Siga as instruções abaixo:

1. Faça um **fork** do projeto.
2. Crie uma nova **branch** para suas alterações (`git checkout -b feature/minha-feature`).
3. **Commit** suas alterações (`git commit -m 'Adiciona nova feature'`).
4. **Envie** para o repositório (`git push origin feature/minha-feature`).
5. Abra um **Pull Request**.

---


## 📜 Licença

Este projeto está licenciado sob a MIT License. Consulte o arquivo [LICENSE](https://choosealicense.com/licenses/mit/) para mais detalhes.

---

**CalculadoraDataAPI** - Simplificando o cálculo e a conversão de datas!

