Funcionalidade: BankAccountOperations
	Sendo eu um correntista do banco
	Para poder saldar as minhas dívidas

Cenário: Sacar valor na Conta Corrente
Dado eu consuma a API GraphQL
Quando eu chamar a mutation sacar informando o número da conta e um valor válido
Então o saldo da minha conta no banco de dados diminuirá de acordo
E a mutation retornará o saldo atualizado.

Cenário: Sacar valor na Conta Corrente com valor maior ao Saldo
Dado eu consuma a API GraphQL
Quando eu chamar a mutation sacar informando o número da conta e um valor maior do que o meu saldo
Então a mutation me retornará um erro do GraphQL informando que eu não tenho saldo suficiente

Cenário: Depositar valor na Conta Corrente
Dado eu consuma a API GraphQL
Quando eu chamar a mutation depositar informando o número da conta e um valor válido
Então a mutation atualizará o saldo da conta no banco de dados
E a mutation retornará o saldo atualizado.

Cenário: Consultar saldo na Conta Corrente
Dado eu consuma a API GraphQL
Quando eu chamar a query saldo informando o número da conta
Então a query retornará o saldo atualizado.